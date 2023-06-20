using System;
using System.IO;
using System.Net;
using System.Text;

namespace SuziVAT
{
    class Http
    {
        // UserAgent to be used on the requests 
        private static string UserAgent = @"curl/7.24.0 (i386-pc-win32) libcurl/7.24.0 zlib/1.2.5";

        // Cookie Container that will handle all the cookies. 
        private static CookieContainer cJar = new CookieContainer();


        // Performs a basic HTTP GET request. 
        // <param name="url">The URL of the request.</param> 
        // <param name="StatusCode">HttpStatusCode returned</param> 
        // <returns>HTML Content of the response.</returns> 
        public static string HttpGet(string strURL, string serverToken, ref HttpStatusCode StatusCode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            HttpWebResponse response = (HttpWebResponse)null;
            request.CookieContainer = cJar;
            request.UserAgent = UserAgent;
            request.KeepAlive = false;
            request.Method = "GET";
            request.Accept = "application/vnd.hmrc.1.0+json";
            request.PreAuthenticate = true;

            // Refresh IP and MAC addresses
            string IPAddresses = Program.GetIPandMACAddresses();

            // Add Fraud Prevention Headers
            request.Headers.Add("Gov-Client-Connection-Method: " + Program.GovClientConnectionMethod);
            request.Headers.Add("Gov-Client-Device-ID: " + Program.GovClientDeviceID);
            request.Headers.Add("Gov-Client-User-IDs: " + Program.GovClientUserIDs);
            request.Headers.Add("Gov-Client-Timezone: " + Program.GovClientTimezone);
            request.Headers.Add("Gov-Client-Local-IPs: " + Program.GovClientLocalIPs);
            request.Headers.Add("Gov-Client-MAC-Addresses: " + Program.GovClientMACAddresses);
            request.Headers.Add("Gov-Client-Screens: " + Program.GovClientScreens);
            request.Headers.Add("Gov-Client-Window-Size: " + Program.GovClientWindowSize);
            request.Headers.Add("Gov-Client-User-Agent: " + Program.GovClientUserAgent);
            request.Headers.Add("Gov-Client-Multi-Factor: " + Program.GovClientMultiFactor);
            request.Headers.Add("Gov-Vendor-Product-Name: " + Program.GovVendorProductName);
            request.Headers.Add("Gov-Vendor-Version: " + Program.GovVendorVersion);
            request.Headers.Add("Gov-Vendor-License-IDs: " + Program.GovVendorLicenseIDs);

            // Date/Time accurate to milliseconds
            string DateTimeNow = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            request.Headers.Add("Gov-Client-Local-IPs-Timestamp: " + DateTimeNow);

            Program.WriteLog("Making HMRC API Call with the following information");
            Program.WriteLog("  IP Addresses " + IPAddresses);
            Program.WriteLog("  Date/Time " + DateTimeNow);

            if (serverToken.Length != 0)
                request.Headers.Add("Authorization", "Bearer " + serverToken);

            if (Program.bDebug)
            {
                Program.WriteLog("HTTP GET to: " + strURL);

                Program.WriteLog("HTTP Headers:");

                for (int i = 0; i < request.Headers.Count; ++i)
                    Program.WriteLog("  " + request.Headers.Keys[i] + ": " + request.Headers[i]);
            }

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }

            catch (WebException e)
            {
                Program.WriteLog("Exception Raised");
                Program.WriteLog(e.ToString());

                response = (HttpWebResponse)(e.Response);
            }

            StreamReader sr = new StreamReader(response.GetResponseStream());

            StatusCode = response.StatusCode;

            if (Program.bDebug || !((int)StatusCode).ToString().StartsWith("2"))
                Program.WriteLog("HTTP Status Code: " + ((int)StatusCode).ToString() + " (" + StatusCode + ")");

            string strReply = sr.ReadToEnd();
            response.Close();

            Program.WriteLog("HttpGet Response: " + strReply);
            Program.WriteLog("");

            return strReply;
        }


        // Performs a basic HTTP POST request 
        // <param name="url">The URL of the request.</param> 
        // <param name="post">POST Data to be passed.</param> 
        // <param name="StatusCode">HttpStatusCode returned</param> 
        // <returns>HTML Content of the response.</returns> 
        public static string HttpPost(string strURL, string strPost, ref HttpStatusCode StatusCode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            HttpWebResponse response = (HttpWebResponse)null;
            request.CookieContainer = cJar;
            request.UserAgent = UserAgent;
            request.KeepAlive = false;
            request.Method = "POST";

            // Refresh IP and MAC addresses
            string IPAddresses = Program.GetIPandMACAddresses();

            // Add Fraud Prevention Headers
            request.Headers.Add("Gov-Client-Connection-Method: " + Program.GovClientConnectionMethod);
            request.Headers.Add("Gov-Client-Device-ID: " + Program.GovClientDeviceID);
            request.Headers.Add("Gov-Client-User-IDs: " + Program.GovClientUserIDs);
            request.Headers.Add("Gov-Client-Timezone: " + Program.GovClientTimezone);
            request.Headers.Add("Gov-Client-Local-IPs: " + Program.GovClientLocalIPs);
            request.Headers.Add("Gov-Client-MAC-Addresses: " + Program.GovClientMACAddresses);
            request.Headers.Add("Gov-Client-Screens: " + Program.GovClientScreens);
            request.Headers.Add("Gov-Client-Window-Size: " + Program.GovClientWindowSize);
            request.Headers.Add("Gov-Client-User-Agent: " + Program.GovClientUserAgent);
            request.Headers.Add("Gov-Client-Multi-Factor: " + Program.GovClientMultiFactor);
            request.Headers.Add("Gov-Vendor-Product-Name: " + Program.GovVendorProductName);
            request.Headers.Add("Gov-Vendor-Version: " + Program.GovVendorVersion);
            request.Headers.Add("Gov-Vendor-License-IDs: " + Program.GovVendorLicenseIDs);

            // Date/Time accurate to milliseconds
            string DateTimeNow = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            request.Headers.Add("Gov-Client-Local-IPs-Timestamp: " + DateTimeNow);

            Program.WriteLog("Making HMRC API Call with the following information");
            Program.WriteLog("  IP Addresses " + IPAddresses);
            Program.WriteLog("  Date/Time " + DateTimeNow);

            byte[] postBytes = Encoding.ASCII.GetBytes(strPost);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            if (Program.bDebug)
            {
                Program.WriteLog("HTTP POST to: " + strURL);

                Program.WriteLog("HTTP Headers:");

                for (int i = 0; i < request.Headers.Count; ++i)
                    Program.WriteLog("  " + request.Headers.Keys[i] + ": " + request.Headers[i]);

                Program.WriteLog("HTTP POST Data: " + strPost.Replace(Program.ClientSecret, "<ClientSecret>").Replace(Program.ClientID, "<ClientID>"));
            }

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }

            catch (WebException e)
            {
                Program.WriteLog("Exception Raised");
                Program.WriteLog(e.ToString());

                response = (HttpWebResponse)(e.Response);
            }

            StreamReader sr = new StreamReader(response.GetResponseStream());

            StatusCode = response.StatusCode;

            if (Program.bDebug || !((int)StatusCode).ToString().StartsWith("2"))
                Program.WriteLog("HTTP Status Code: " + ((int)StatusCode).ToString() + " (" + StatusCode + ")");

            string strReply = sr.ReadToEnd();
            response.Close();

            Program.WriteLog("HttpGet Response: " + strReply);
            Program.WriteLog("");

            return strReply;
        }


        public static string HttpPost2(string strURL, string serverToken, string strPost, ref HttpStatusCode StatusCode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            HttpWebResponse response = (HttpWebResponse)null;
            request.CookieContainer = cJar;
            request.UserAgent = UserAgent;
            request.KeepAlive = false;
            request.Accept = "application/vnd.hmrc.1.0+json";
            request.Method = "POST";

            byte[] postBytes = Encoding.ASCII.GetBytes(strPost);
            request.ContentType = "application/json";
            request.ContentLength = postBytes.Length;

            // Refresh IP and MAC addresses
            string IPAddresses = Program.GetIPandMACAddresses();

            // Add Fraud Prevention Headers
            request.Headers.Add("Gov-Client-Connection-Method: " + Program.GovClientConnectionMethod);
            request.Headers.Add("Gov-Client-Device-ID: " + Program.GovClientDeviceID);
            request.Headers.Add("Gov-Client-User-IDs: " + Program.GovClientUserIDs);
            request.Headers.Add("Gov-Client-Timezone: " + Program.GovClientTimezone);
            request.Headers.Add("Gov-Client-Local-IPs: " + Program.GovClientLocalIPs);
            request.Headers.Add("Gov-Client-MAC-Addresses: " + Program.GovClientMACAddresses);
            request.Headers.Add("Gov-Client-Screens: " + Program.GovClientScreens);
            request.Headers.Add("Gov-Client-Window-Size: " + Program.GovClientWindowSize);
            request.Headers.Add("Gov-Client-User-Agent: " + Program.GovClientUserAgent);
            request.Headers.Add("Gov-Client-Multi-Factor: " + Program.GovClientMultiFactor);
            request.Headers.Add("Gov-Vendor-Product-Name: " + Program.GovVendorProductName);
            request.Headers.Add("Gov-Vendor-Version: " + Program.GovVendorVersion);
            request.Headers.Add("Gov-Vendor-License-IDs: " + Program.GovVendorLicenseIDs);

            // Date/Time accurate to milliseconds
            string DateTimeNow = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            request.Headers.Add("Gov-Client-Local-IPs-Timestamp: " + DateTimeNow);

            Program.WriteLog("Making HMRC API Call with the following information");
            Program.WriteLog("  IP Addresses " + IPAddresses);
            Program.WriteLog("  Date/Time " + DateTimeNow);

            if (serverToken.Length != 0)
                request.Headers.Add("Authorization", "Bearer " + serverToken);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            if (Program.bDebug)
            {
                Program.WriteLog("HTTP POST to: " + strURL);

                Program.WriteLog("HTTP Headers:");

                for (int i = 0; i < request.Headers.Count; ++i)
                    Program.WriteLog("  " + request.Headers.Keys[i] + ": " + request.Headers[i]);

                Program.WriteLog("HTTP POST Data: " + strPost.Replace(Program.ClientSecret, "<ClientSecret>").Replace(Program.ClientID, "<ClientID>"));
            }

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }

            catch (WebException e)
            {
                Program.WriteLog("Exception Raised");
                Program.WriteLog(e.ToString());

                response = (HttpWebResponse)(e.Response);
            }

            StreamReader sr = new StreamReader(response.GetResponseStream());

            StatusCode = response.StatusCode;

            if (Program.bDebug || !((int)StatusCode).ToString().StartsWith("2"))
                Program.WriteLog("HTTP Status Code: " + ((int)StatusCode).ToString() + " (" + StatusCode + ")");

            string strReply = sr.ReadToEnd();
            response.Close();

            Program.WriteLog("HttpGet Response: " + strReply);
            Program.WriteLog("");

            return strReply;
        }
    }
}