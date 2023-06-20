using SuziVAT.Data;
using System;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Windows.Forms;
using static SuziVAT.UUID;

namespace SuziVAT
{
    static class Program
    {
        public static string strVersion = "v1.5";

        private static StreamWriter swLogFile = null;
        private static string strLogFolder = "";
        private static string strProgName = "SuziVAT";
        public static bool bDebug = false;

        private static Settings _settings;

        public static Double SalesVAT = 0;
        public static Double PurchasesVAT = 0;
        public static Double SalesTurnover = 0;
        public static Double PurchasesTurnover = 0;
        public static Double VATDue = 0;

        public static string FileSettings = AppDomain.CurrentDomain.BaseDirectory + "settings.json";

        public static string ClientID = "";
        public static string ClientSecret = "";
        public static string BaseURL = "";

        public static string GovClientConnectionMethod = "";    // Gov-Client-Connection-Method
        public static string GovClientDeviceID = "";            // Gov-Client-Device-ID
        public static string GovClientUserIDs = "";             // Gov-Client-User-IDs

        public static string GovClientTimezone = "";            // Gov-Client-Timezone
        public static string GovClientLocalIPs = "";            // Gov-Client-Local-IPs
        public static string GovClientMACAddresses = "";        // Gov-Client-MAC-Addresses

        public static string GovClientScreens = "";             // Gov-Client-Screens
        public static string GovClientWindowSize = "";          // Gov-Client-Window-Size
        public static string GovClientUserAgent = "";           // Gov-Client-User-Agent
        public static string GovClientMultiFactor = "";         // Gov-Client-Multi-Factor

        public static string GovVendorProductName = "";         // Gov-Vendor-Product-Name
        public static string GovVendorVersion = "";             // Gov-Vendor-Version
        public static string GovVendorLicenseIDs = "";          // Gov-Vendor-License-IDs

        public static string PrivacyValues = "";
        public static int LeftColSize = 20;


        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                _settings = Settings.Load(FileSettings);
                _settings.Save(FileSettings);
                strLogFolder = _settings.LogFilePath + "\\" + strProgName + "\\";

                //if (_settings.Test)
                //{
                //    // Test Settings
                //    ClientID = "<Your Test ClientID>";
                //    ClientSecret = "<Your Test Client Secret>";
                //    BaseURL = "https://test-api.service.hmrc.gov.uk";
                //}
                //else
                //{
                //    // Production Settings
                //    ClientID = "<Your Production ClientID>";
                //    ClientSecret = "<Your Production Client Secret>";
                //    BaseURL = "https://api.service.hmrc.gov.uk";
                //}

                if (_settings.Test)
                {
                    // Test Settings
                    ClientID = "R8xluuYDeD7KDZ23h91oGHkWNtMa";
                    ClientSecret = "3bb5011a-b279-4a0d-8a57-33e16c53a53c";
                    BaseURL = "https://test-api.service.hmrc.gov.uk";
                }
                else
                {
                    // Production Settings
                    ClientID = "a1oKZ7vM3y3t7wVM48xflZZZZ6Ea";
                    ClientSecret = "6457dd1f-3d61-414a-a35d-0a0a4f4a1a4b";
                    BaseURL = "https://api.service.hmrc.gov.uk";
                }

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            catch (System.Exception e)
            {
                Console.Write(e.ToString());
                return;
            }


            try
            {
                StartLog();
            }
            catch (System.Exception e)
            {
                Console.Write(e.ToString());
                return;
            }


            try
            {
                switch (args.Length)
                {
                    case 0:
                        break;

                    case 1:
                        string strURL = args[0];
                        Uri uri = new Uri(args[0]);
                        var queryParams = HttpUtility.ParseQueryString(uri.Query);

                        _settings.AuthCode = queryParams["code"];
                        _settings.Save(FileSettings);

                        if (_settings.State != queryParams["state"])
                        {
                            MessageBox.Show("State is not valid, please run again");

                            WriteLog("State is not Valid");
                            WriteLog("  Should be: " + _settings.State);
                            WriteLog("  Is: " + queryParams["state"]);
                            WriteLog("");

                            DeleteOldLogs();
                            StopLog();
                            return;
                        }

                        break;

                    default:
                        break;
                }

                if (_settings.VATNumber.Length == 0)
                {
                    MessageBox.Show("No VAT Number.\r\nPlease edit settings.json file and add VAT Number");
                    WriteLog("No VAT Number.");
                    StopLog();
                    return;
                }
            }
            catch (System.Exception e)
            {
                Console.Write(e.ToString());
                WriteLog("");
                WriteLog("Exception Raised Getting Args");
                WriteLog(e.ToString());
                StopLog();
                return;
            }

            try
            {
                GetHMRCFraudPreventionValues();
            }
            catch (System.Exception e)
            {
                Console.Write(e.ToString());
                WriteLog("");
                WriteLog("Exception Raised Getting HMRC Fraud Prevention Values");
                WriteLog(e.ToString());
                StopLog();
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (System.Exception e)
            {
                Console.Write(e.ToString());
                WriteLog("");
                WriteLog("Exception Raised Running Application");
                WriteLog(e.ToString());
                StopLog();
                return;
            }

            try
            {
                DeleteOldLogs();
                StopLog();
            }
            catch (System.Exception e)
            {
                WriteLog("");
                WriteLog("Exception Raised Deleting Old Logs");
                WriteLog(e.ToString());
                return;
            }
        }


        private static void StartLog()
        {
            if (!Directory.Exists(strLogFolder))
                Directory.CreateDirectory(strLogFolder);

            // Write data out to a file
            swLogFile = File.CreateText(strLogFolder + strProgName + " " + DateTime.Now.ToString("yyy-MM-dd HH-mm-ss") + ".log");

            WriteLog("");
            WriteLog(strProgName + " Started");
            WriteLog("");

            swLogFile.Flush();
        }

        public static void WriteLog(string strLine)
        {
            // Write data out to a file
            if (strLine.Length == 0)
            {
                swLogFile.WriteLine("");
            }
            else
            {
                swLogFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + strLine);
            }

            swLogFile.Flush();
        }

        private static void StopLog()
        {
            // Write data out to a file
            WriteLog(strProgName + " Finished");

            swLogFile.Close();
        }

        private static void DeleteOldLogs()
        {
            string strFileDateTime = "";
            DirectoryInfo objDirectory = new DirectoryInfo(strLogFolder);

            foreach (FileInfo objFile in objDirectory.GetFiles("*.log"))
            {
                strFileDateTime = objFile.Name.Substring(0, objFile.Name.Length - objFile.Extension.Length);

                if (strFileDateTime.Length >= 19)
                {
                    strFileDateTime = strFileDateTime.Substring(strFileDateTime.Length - 19, 10);

                    if (DateTime.ParseExact(strFileDateTime, "yyyy-MM-dd", null).CompareTo(DateTime.Now.Date.AddDays(-30)) < 0)
                    {
                        objFile.Delete();
                    }
                }
            }
        }

        public static string MakeRandomString(int iLength, string strCharacterSet)
        {
            if (iLength < 1)
                throw new ArgumentException("length must not be less than 1", "iLength");

            if (iLength > 100)
                throw new ArgumentException("length must not be more than 100", "iLength");

            if (strCharacterSet.Length < 1)
                throw new ArgumentException("character set must not be empty", "strCharacterSet");

            if (iLength < 1)
                throw new ArgumentException("character set size must not be more than 256", "strCharacterSet");


            char[] chrCharacterSet = new char[strCharacterSet.Length];
            chrCharacterSet = strCharacterSet.ToCharArray();

            // So that the Modulus fits exactly into the length of the character set, work out the largest integer multiple that will fit
            int limit = (int)(256 / chrCharacterSet.Length) * chrCharacterSet.Length;

            RNGCryptoServiceProvider Crypto = new RNGCryptoServiceProvider();

            byte[] byteRandom = new byte[1];
            char[] chrResult = new char[iLength];
            int iIndex = 0;

            while (iIndex < iLength)
            {
                Crypto.GetBytes(byteRandom);

                if (byteRandom[0] < limit)
                    chrResult[iIndex++] = chrCharacterSet[byteRandom[0] % chrCharacterSet.Length];
            }

            return new string(chrResult);
        }


        private static void GetHMRCFraudPreventionValues()
        {
            PrivacyValues = "";

            PrivacyValues += "The IP Address information is refreshed on each call\r\nto HMRC and will be accompanied by the Date/Time\r\naccurate to milliseconds\r\n\r\n";

            GovClientConnectionMethod = "DESKTOP_APP_DIRECT";
            PrivacyValues += "Connection Method".PadRight(LeftColSize) + "DESKTOP APP DIRECT\r\n\r\n";

            _settings = Settings.Load(Program.FileSettings);
            GovClientDeviceID = _settings.DeviceID;

            if (GovClientDeviceID.Replace("-", "").Length != 32)
            {
                Uuid newUUID = new Uuid(Encoding.ASCII.GetBytes(Program.MakeRandomString(16, "abcdefghijklmnopqrstuvwxyz01234567892")));
                GovClientDeviceID = newUUID.ToString();

                _settings.DeviceID = GovClientDeviceID;
                _settings.Save(Program.FileSettings);
            }

            PrivacyValues += "Device ID".PadRight(LeftColSize) + GovClientDeviceID + "\r\n";

            string UserID = Environment.UserName;
            GovClientUserIDs = "os=" + Uri.EscapeDataString(UserID);
            PrivacyValues += "User ID".PadRight(LeftColSize) + UserID + "\r\n";


            TimeZoneInfo LocalZone = TimeZoneInfo.Local;
            string TimeZone = LocalZone.DisplayName.Substring(1, 9);
            GovClientTimezone = TimeZone;
            PrivacyValues += "TimeZone".PadRight(LeftColSize) + TimeZone + "\r\n\r\n";

            GovClientLocalIPs = "";
            GovClientMACAddresses = "";

            GetIPandMACAddresses();

            foreach (Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                string strTemp = "";
                string Width = screen.Bounds.Width.ToString();
                string Height = screen.Bounds.Height.ToString();
                string Scaling = (screen.Bounds.Height / System.Windows.SystemParameters.PrimaryScreenHeight).ToString();
                string Depth = screen.BitsPerPixel.ToString();

                strTemp = ",width=" + Width;
                strTemp += "&height=" + Height;
                strTemp += "&scaling-factor=" + Scaling;
                strTemp += "&colour-depth=" + Depth;

                GovClientScreens += strTemp;

                PrivacyValues += "Screen".PadRight(LeftColSize) + "width=" + Width + "\r\n";
                PrivacyValues += "".PadRight(LeftColSize) + "height=" + Height + "\r\n";
                PrivacyValues += "".PadRight(LeftColSize) + "scaling-factor=" + Scaling + "\r\n";
                PrivacyValues += "".PadRight(LeftColSize) + "colour-depth=" + Depth + "\r\n\r\n";
            }

            if (GovClientScreens.Length != 0)
                GovClientScreens = GovClientScreens.Substring(1);


            string PrimaryWidth = System.Windows.SystemParameters.PrimaryScreenWidth.ToString();
            string PrimaryHeight = System.Windows.SystemParameters.PrimaryScreenHeight.ToString();

            GovClientWindowSize = "width=" + PrimaryWidth;
            GovClientWindowSize += "&height=" + PrimaryHeight;

            PrivacyValues += "Primary Screen".PadRight(LeftColSize) + "width=" + PrimaryWidth + "\r\n";
            PrivacyValues += "".PadRight(LeftColSize) + "height=" + PrimaryHeight + "\r\n\r\n";

            string OSFamily = "Windows";
            string OSVersion = Environment.OSVersion.ToString();

            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();

            string DeviceManufacturer = "";
            string DeviceModel = "";

            foreach (ManagementObject mo in moc)
            {
                DeviceManufacturer = mo["Manufacturer"].ToString();
                DeviceModel = mo["Model"].ToString();
            }

            GovClientUserAgent = "os-family=" + Uri.EscapeDataString(OSFamily) + "&os-version=" + Uri.EscapeDataString(OSVersion) + "&device-manufacturer=" + Uri.EscapeDataString(DeviceManufacturer) + "&device-model=" + Uri.EscapeDataString(DeviceModel);

            PrivacyValues += "Operating System".PadRight(LeftColSize) + OSFamily + "\r\n";
            PrivacyValues += "OS Version".PadRight(LeftColSize) + OSVersion + "\r\n";
            PrivacyValues += "Computer Maker".PadRight(LeftColSize) + DeviceManufacturer + "\r\n";
            PrivacyValues += "Computer Model".PadRight(LeftColSize) + DeviceModel + "\r\n\r\n";

            string strType = "OTHER";
            string strTimeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mmZ");
            string strUniqueRef = GovClientDeviceID;

            GovClientMultiFactor = "type=" + strType + "&timestamp=" + Uri.EscapeDataString(strTimeStamp) + "&unique-reference=" + strUniqueRef;
            PrivacyValues += "MultiFactor".PadRight(LeftColSize) + "type " + strType + "\r\n";
            PrivacyValues += "".PadRight(LeftColSize) + "timestamp " + strTimeStamp + "\r\n";
            PrivacyValues += "".PadRight(LeftColSize) + "unique-reference " + strUniqueRef + "\r\n\r\n";

            GovVendorLicenseIDs = "SuziVAT=GNU%20General%20Public%20License%20v3.0";
            PrivacyValues += "LicenceID".PadRight(LeftColSize) + "GNU General Public License v3.0" + "\r\n\r\n";

            GovVendorProductName = "SuziVAT";
            PrivacyValues += "Product Name".PadRight(LeftColSize) + GovVendorProductName + "\r\n";

            GovVendorVersion = "SuziVAT=" + Uri.EscapeDataString(Program.strVersion);
            PrivacyValues += "Product Version".PadRight(LeftColSize) + "SuziVAT=" + Program.strVersion + "\r\n\r\n";
        }

        public static string GetIPandMACAddresses()
        {
            GovClientLocalIPs = "";
            GovClientMACAddresses = "";

            string PrivacyLocalIPs = "";
            string PrivacyMACAddresses = "";

            string LogIPAddresses = "";

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties Properties = adapter.GetIPProperties();
                bool IncludeAdapter = false;

                // First scan the Adapter to see if it has IPV4 Addresses that are not APIPA or LocalHost
                foreach (UnicastIPAddressInformation UnicastAddress in Properties.UnicastAddresses)
                {
                    IPAddress IPAddress = UnicastAddress.Address;
                    AddressFamily addressFamily = IPAddress.AddressFamily;

                    if (addressFamily == AddressFamily.InterNetwork && addressFamily != AddressFamily.InterNetworkV6)
                    {
                        if (!IPAddress.ToString().StartsWith("169.") && IPAddress.ToString() != "127.0.0.1" && adapter.OperationalStatus == OperationalStatus.Up)
                            IncludeAdapter = true;
                    }
                }

                // Next scan the Adapter again and hoover up any IPV4 or IPV6 Addresses
                if (IncludeAdapter)
                {
                    foreach (UnicastIPAddressInformation UnicastAddress in Properties.UnicastAddresses)
                    {
                        IPAddress IPAddress = UnicastAddress.Address;
                        AddressFamily addressFamily = IPAddress.AddressFamily;

                        if (addressFamily == AddressFamily.InterNetwork || addressFamily == AddressFamily.InterNetworkV6)
                        {
                            PrivacyLocalIPs += IPAddress.ToString() + "\r\n".PadRight(LeftColSize + 2);
                            GovClientLocalIPs += "," + Uri.EscapeDataString(IPAddress.ToString());
                            LogIPAddresses += "," + IPAddress.ToString();
                        }
                    }

                    string strMACAddress = adapter.GetPhysicalAddress().ToString();
                    string strDisplayMACAddress = "";

                    for (int i = 0; i < strMACAddress.Length; i += 2)
                        strDisplayMACAddress += strMACAddress.Substring(i, 2) + ":";

                    strDisplayMACAddress = strDisplayMACAddress.Substring(0, strDisplayMACAddress.Length - 1);
                    PrivacyMACAddresses += strDisplayMACAddress + "\r\n".PadRight(LeftColSize + 2);

                    GovClientMACAddresses += "," + Uri.EscapeDataString(strDisplayMACAddress);
                }
            }

            if (GovClientLocalIPs.Length != 0)
                GovClientLocalIPs = GovClientLocalIPs.Substring(1);

            if (GovClientMACAddresses.Length != 0)
                GovClientMACAddresses = GovClientMACAddresses.Substring(1);

            PrivacyValues += "Local IPs".PadRight(LeftColSize) + PrivacyLocalIPs.Trim() + "\r\n\r\n";
            PrivacyValues += "MACAddresses".PadRight(LeftColSize) + PrivacyMACAddresses.Trim() + "\r\n\r\n";

            if (LogIPAddresses.Length != 0)
                LogIPAddresses = LogIPAddresses.Substring(1);

            return LogIPAddresses;
        }
    }
}
