using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuziVAT.Data;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SuziVAT
{
    public partial class Form1 : Form
    {
        string strPeriodKey = "";
        string strPeriodDate = "";
        string CompanyYear = "";
        bool bCalculationDone = false;

        private Settings _settings;

        public Form1()
        {
            InitializeComponent();

            _settings = Settings.Load(Program.FileSettings);
            _settings.Save(Program.FileSettings);

            txtAccessToken.Text = "";
            txtRefreshToken.Text = "";
            txtAuthCode.Text = _settings.AuthCode;

            txtAccessToken.Text = _settings.AccessToken;
            txtRefreshToken.Text = _settings.RefreshToken;
            txtScope.Text = _settings.Scope;
            txtExpires.Text = _settings.Expiry;

            this.Text = "SuziVAT - Submit MTD VAT " + Program.strVersion;

            //dlgPrivacy Privacy = new dlgPrivacy();

            //DialogResult result = Privacy.ShowDialog();

            //if (result != DialogResult.OK)
            //    Application.Exit();

            // Possibly just check the Fraud Prevention Headers
            if (_settings.Test && false)
            {
                string strURL = "";
                string strReply = "";

                if (txtAccessToken.Text.Length == 0)
                {
                    MessageBox.Show("Must get Access Token first", "Missing Access Token Error");
                    return;
                }

                if (DateTime.Parse(txtExpires.Text) < DateTime.Now)
                {
                    MessageBox.Show("Access Token has Expired\r\nPlease click \"Refresh Access Token\"", "Expired Access Token Error");
                    return;
                }

                HttpStatusCode StatusCode = 0;

                strURL = Program.BaseURL + "/test/fraud-prevention-headers/validate";
                strReply = Http.HttpGet(strURL, txtAccessToken.Text, ref StatusCode);
                string strFormattedReply = "";


                // Format Reply if we can
                try
                {
                    strFormattedReply = JToken.Parse(strReply).ToString(Formatting.Indented);
                }
                catch
                {
                    strFormattedReply = strReply;
                }



                if (StatusCode.ToString() != "OK")
                {
                    MessageBox.Show("Error: " + StatusCode.ToString() + "\r\n\r\nURL: " + strURL + "\r\n\r\nPostData:\r\n" + strFormattedReply, "Error Checking Headers");
                }
                else
                {
                    Program.WriteLog(strFormattedReply.Replace("\\\"","\"").Replace("\\r\\n","\r\n"));
                    MessageBox.Show(strFormattedReply, "Checking Headers");
                }

                Environment.Exit(0);
            }
        }


        private void btnGetAuthCode_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1 - Request an OAuth authorisation code with the required scope
                string strURL = Program.BaseURL + "/oauth/authorize";
                _settings.State = Program.MakeRandomString(36, "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567892");
                _settings.Save(Program.FileSettings);

                strURL += "?client_id=" + Program.ClientID;
                strURL += "&response_type=code";
                strURL += "&state=" + _settings.State;
                strURL += "&scope=read:vat%20write:vat";
                strURL += "&redirect_uri=" + _settings.RedirectURL;

                Program.WriteLog("Starting Browser with URL " + strURL.Replace(Program.ClientID, "<ClientID>"));
                Process.Start(strURL);

                Program.WriteLog("");

                txtAuthCode.Text = "";
                txtAuthCode.Focus();
            }
            catch (System.Exception e2)
            {
                Program.WriteLog("");
                Program.WriteLog("Exception Raised");
                Program.WriteLog(e2.ToString());
            }
        }


        private void txtAuthCode_TextChanged(object sender, EventArgs e)
        {
            _settings.AuthCode = txtAuthCode.Text;
            _settings.Save(Program.FileSettings);
        }


        private void btnGetAccessToken_Click(object sender, EventArgs e)
        {
            if (txtAuthCode.Text.Length == 0)
            {
                MessageBox.Show("Must get AuthCode first", "No AuthCode Error");
                return;
            }

            _settings.AuthCode = txtAuthCode.Text;
            _settings.Save(Program.FileSettings);

            HttpStatusCode StatusCode = 0;

            string strURL = Program.BaseURL + "/oauth/token";
            string strPost = "grant_type=authorization_code";
            strPost += "&client_id=" + Program.ClientID;
            strPost += "&client_secret=" + Program.ClientSecret;
            strPost += "&redirect_uri=" + _settings.RedirectURL;
            strPost += "&code=" + txtAuthCode.Text;

            string strReply = Http.HttpPost(strURL, strPost, ref StatusCode);

            if (StatusCode.ToString() != "OK")
            {
                MessageBox.Show(StatusCode.ToString() + "\r\n\r\nURL: " + strURL + "\r\n\r\nPostData: " + strPost.Replace(Program.ClientSecret, "<ClientSecret>").Replace(Program.ClientID, "<ClientID>") + "\r\n\r\nReply: " + strReply, "Error Getting Access Token");
                return;
            }

            AccessToken AccessToken = new AccessToken();
            AccessToken = JsonConvert.DeserializeObject<AccessToken>(strReply);
            txtAccessToken.Text = AccessToken.access_token;
            txtRefreshToken.Text = AccessToken.refresh_token;
            txtScope.Text = AccessToken.scope;
            txtExpires.Text = DateTime.Now.AddSeconds(double.Parse(AccessToken.expires_in)).ToString("dd-MMM-yyyy HH:mm:ss");
            txtAuthCode.Text = "";

            _settings.AccessToken = txtAccessToken.Text;
            _settings.RefreshToken = txtRefreshToken.Text;
            _settings.Scope = txtScope.Text;
            _settings.Expiry = txtExpires.Text;
            _settings.AuthCode = "";

            _settings.Save(Program.FileSettings);
        }


        private void btnRefreshAccessToken_Click(object sender, EventArgs e)
        {
            if (txtRefreshToken.Text.Length == 0)
            {
                MessageBox.Show("Must get Access and Refresh Tokens first", "No Refresh Token Error");
                return;
            }

            _settings.AuthCode = txtAuthCode.Text;
            _settings.Save(Program.FileSettings);

            HttpStatusCode StatusCode = 0;

            string strURL = Program.BaseURL + "/oauth/token";
            string strPost = "grant_type=refresh_token";
            strPost += "&client_id=" + Program.ClientID;
            strPost += "&client_secret=" + Program.ClientSecret;
            strPost += "&refresh_token=" + txtRefreshToken.Text;

            string strReply = Http.HttpPost(strURL, strPost, ref StatusCode);

            if (StatusCode.ToString() != "OK")
            {
                MessageBox.Show(StatusCode.ToString() + "\r\n\r\nURL: " + strURL + "\r\n\r\nPostData: " + strPost.Replace(Program.ClientSecret, "<ClientSecret>").Replace(Program.ClientID, "<ClientID>") + "\r\n\r\nReply: " + strReply, "Error Getting Access Token");
                return;
            }

            AccessToken AccessToken = new AccessToken();
            AccessToken = JsonConvert.DeserializeObject<AccessToken>(strReply);
            txtAccessToken.Text = AccessToken.access_token;
            txtRefreshToken.Text = AccessToken.refresh_token;
            txtScope.Text = AccessToken.scope;
            txtExpires.Text = DateTime.Now.AddSeconds(double.Parse(AccessToken.expires_in)).ToString("dd-MMM-yyyy HH:mm:ss");

            _settings.AccessToken = txtAccessToken.Text;
            _settings.RefreshToken = txtRefreshToken.Text;
            _settings.Scope = txtScope.Text;
            _settings.Expiry = txtExpires.Text;

            _settings.Save(Program.FileSettings);
        }


        private void txtGetObligations_Click(object sender, EventArgs e)
        {
            string strURL = "";
            string strReply = "";
            Obligations obligations = null;

            if (txtAccessToken.Text.Length == 0)
            {
                MessageBox.Show("Must get Access Token first", "Missing Access Token Error");
                return;
            }

            if (DateTime.Parse(txtExpires.Text) < DateTime.Now)
            {
                MessageBox.Show("Access Token has Expired\r\nPlease click \"Refresh Access Token\"", "Expired Access Token Error");
                return;
            }

            HttpStatusCode StatusCode = 0;

            strURL = Program.BaseURL + "/organisations/vat/" + _settings.VATNumber + "/obligations?status=O";
            strReply = Http.HttpGet(strURL, txtAccessToken.Text, ref StatusCode);
            string strFormattedReply = "";


            // Format Reply if we can
            try
            {
                strFormattedReply = JToken.Parse(strReply).ToString(Formatting.Indented);
            }
            catch
            {
                strFormattedReply = strReply;
            }



            if (StatusCode.ToString() != "OK")
            {
                string strSuggestion = "";

                if (StatusCode.ToString() == "Forbidden")
                    strSuggestion = "\r\n\r\nSuggestion: Is the VAT Number Correct? " + _settings.VATNumber;

                MessageBox.Show("Error: " + StatusCode.ToString() + "\r\n\r\nURL: " + strURL + "\r\n\r\nPostData:\r\n" + strFormattedReply + strSuggestion, "Error getting Obligations");
                return;
            }

            obligations = JsonConvert.DeserializeObject<Obligations>(strReply);

            lbObligations.Items.Clear();

            for (int i = obligations.obligations.Length - 1; i >= 0; i--)
            {
                Obligation temp = obligations.obligations[i];
                string strDate = DateTime.Parse(temp.end).ToString("MMM-yyyy");

                string strStatus = "";

                switch (temp.status)
                {
                    case "O":
                        strStatus = "Outstanding";
                        break;

                    case "F":
                        strStatus = "Fulfilled  ";
                        break;
                }

                lbObligations.Items.Add(strDate + "    " + strStatus + "          " + temp.periodKey);
            }

            lblSelectedObligation.Text = "";
            lblObligations.Visible = true;

            strPeriodKey = "";
            strPeriodDate = "";
        }


        private void lbObligations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbObligations.SelectedIndex < 0)
                return;

            lblSelectedObligation.Text = "";

            string strSelection = lbObligations.SelectedItem.ToString();
            string strStatus = strSelection.Substring(12, 11).Trim();
            CompanyYear = "";

            if (strStatus != "Outstanding")
            {
                MessageBox.Show("Please Select an Outstanding Obligation", "Selection Error");
                lbObligations.SelectedIndex = -1;
                strPeriodKey = "";
            }
            else
            {
                strPeriodDate = strSelection.Substring(0, 8).Trim();
                strPeriodKey = strSelection.Substring(29).Trim();

                lblSelectedObligation.Text = "Selected Obligation:   " + strPeriodDate;

                // Calculate Current Company Year
                if (_settings.UseCompanyYearFolder)
                {
                    DateTime YearEndDate = DateTime.Parse(_settings.CompanyYearEndDate + "-" + DateTime.Parse(strPeriodDate).Year.ToString());

                    if (YearEndDate < DateTime.Parse(strPeriodDate))
                        YearEndDate = YearEndDate.AddYears(1);

                    int YearEndYear = int.Parse(YearEndDate.Year.ToString());
                    CompanyYear = (YearEndYear - 1).ToString() + "-" + YearEndYear.ToString();
                }
            }
        }


        private void btnGetVATFigures_Click(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string ExcelFolderPath = "";


            // Clear Values in Global Variables
            Program.SalesVAT = 0;
            Program.PurchasesVAT = 0;
            Program.SalesTurnover = 0;
            Program.PurchasesTurnover = 0;
            Program.VATDue = 0;

            // Clear on Form
            txtSalesVAT.Text = "";
            txtPurchasesVAT.Text = "";
            txtSalesTurnover.Text = "";
            txtPurchasesTurnover.Text = "";
            txtNetVAT.Text = "";

            bCalculationDone = false;


            if (txtAccessToken.Text.Length == 0)
            {
                MessageBox.Show("Must get Access Token first", "Missing Access Token Error");
                return;
            }

            if (lbObligations.Items.Count == 0)
            {
                MessageBox.Show("Must get Obligations first", "Missing Obligations Error");
                return;
            }

            if (strPeriodKey.Length == 0)
            {
                MessageBox.Show("Must Select an Obligation first", "No Obligation Selected");
                return;
            }

            if (CompanyYear.Length == 0)
                ExcelFolderPath = _settings.WorkBookPath;
            else
                ExcelFolderPath = _settings.WorkBookPath + "\\" + CompanyYear;

            if (!Directory.Exists(ExcelFolderPath))
            {
                MessageBox.Show("Folder " + ExcelFolderPath + " does not exist, please create", "Missing Folder Error");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ExcelFolderPath;
                openFileDialog.Filter = "Excel files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExcelFileName = openFileDialog.FileName;
                }
                else return;

            }

            //MessageBox.Show("Excel File Name: " + ExcelFileName);

            // Create Excel Objects
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ExcelFileName);
            Excel._Worksheet xlWorkSheet = xlWorkbook.Sheets[_settings.WorkSheetName];
            Excel.Range xlRange = xlWorkSheet.UsedRange;

            DateTime VATReturnDate = DateTime.MinValue;

            if (_settings.VATReturnDateCell.Length != 0)
            {

                VATReturnDate = DateTime.FromOADate(xlWorkSheet.Range[_settings.VATReturnDateCell].Value2);
                //MessageBox.Show(VATReturnDate.ToString());

                DateTime PeriodEndDate = DateTime.Parse("01-" + strPeriodDate).AddMonths(1).AddDays(-1);

                if (VATReturnDate != PeriodEndDate)
                {
                    MessageBox.Show("VAT Period End Date on Excel File \"" + ExcelFileName + "\" in cell \"" + _settings.VATReturnDateCell + "\" of " + VATReturnDate.ToString("dd-MMM-yyyy") + " does not match the end Date of the chosen Obligation of " + PeriodEndDate.ToString("dd-MMM-yyyy"), "Date MisMatch Error");
                    return;
                }
            }

            // Get Values from SpreadSheet
            Program.SalesVAT = xlWorkSheet.Range[_settings.SalesVATCell].Value;
            Program.PurchasesVAT = xlWorkSheet.Range[_settings.PurchaseVATCell].Value;
            Program.SalesTurnover = xlWorkSheet.Range[_settings.SalesTurnoverCell].Value;
            Program.PurchasesTurnover = xlWorkSheet.Range[_settings.PurchaseTurnoverCell].Value;

            // Set Values into Form
            txtSalesVAT.Text = Program.SalesVAT.ToString("0,0.00");
            txtPurchasesVAT.Text = Program.PurchasesVAT.ToString("0,0.00");
            txtSalesTurnover.Text = Program.SalesTurnover.ToString("0,0");
            txtPurchasesTurnover.Text = Program.PurchasesTurnover.ToString("0,0");

            // Cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Release com objects to fully kill excel;process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorkSheet);

            // Close and Release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            // Quit and Release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }


        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Program.VATDue = Double.Parse("0" + txtSalesVAT.Text) - Double.Parse("0" + txtPurchasesVAT.Text);

            txtSalesVAT.Text = Program.SalesVAT.ToString("0,0.00");
            txtPurchasesVAT.Text = Program.PurchasesVAT.ToString("0,0.00");
            txtSalesTurnover.Text = Program.SalesTurnover.ToString("0,0");
            txtPurchasesTurnover.Text = Program.PurchasesTurnover.ToString("0,0");
            txtNetVAT.Text = Math.Abs(Program.VATDue).ToString("0,0.00");

            if (Program.VATDue < 0)
            {
                lblNetVAT.Text = "VAT Refund from HMRC";
                lblNetVAT.ForeColor = Color.Red;
                lblPound5.ForeColor = Color.Red;
                txtNetVAT.BackColor = txtNetVAT.BackColor;
                txtNetVAT.ForeColor = Color.Red;
            }
            else
            {
                lblNetVAT.Text = "     VAT Due to HMRC";
                lblNetVAT.ForeColor = Color.Black;
                lblPound5.ForeColor = Color.Black;
                txtNetVAT.ForeColor = Color.Black;
            }

            bCalculationDone = true;
        }


        private void btnSubmitVATReturn_Click(object sender, EventArgs e)
        {
            string SubmissionReportFolderPath = "";


            if (lbObligations.Items.Count == 0)
            {
                MessageBox.Show("Must get Obligations first", "No Obligations Error");
                return;
            }

            if (!bCalculationDone)
            {
                MessageBox.Show("You must do the Calculation", "Calculation Not Done Error");
                return;
            }


            if (_settings.SubmissionReportPath.Length != 0)
            {
                if (CompanyYear.Length == 0)
                    SubmissionReportFolderPath = _settings.WorkBookPath;
                else
                    SubmissionReportFolderPath = _settings.WorkBookPath + "\\" + CompanyYear;

                if (!Directory.Exists(SubmissionReportFolderPath))
                {
                    MessageBox.Show("Folder " + SubmissionReportFolderPath + " does not exist", "Missing Folder Error");
                    return;
                }
            }


            string strBody = "";
            strBody += "Please check these figures carefully.\r\n\r\n";
            strBody += "            Sales VAT: " + txtSalesVAT.Text + "\r\n";
            strBody += "        Purchases VAT: " + txtPurchasesVAT.Text + "\r\n";

            if (txtNetVAT.ForeColor == Color.Red)
                strBody += "  VAT Refund from HMRC: " + txtNetVAT.Text + "\r\n\r\n";
            else
                strBody += "       VAT due to HMRC: " + txtNetVAT.Text + "\r\n\r\n";

            strBody += "       Sales Turnover: " + txtSalesTurnover.Text + "\r\n";
            strBody += "   Purchases Turnover: " + txtPurchasesTurnover.Text + "\r\n\r\n";
            strBody += "When you submit this VAT information you are making a legal\r\n";
            strBody += "declaration that the information is true and complete.\r\n";
            strBody += "A false declaration can result in prosecution.\r\n";

            //DialogResult Result = MessageBox.Show(strBody, "Confirm VAT Amounts for " + strPeriodDate, MessageBoxButtons.OKCancel);

            //if (Result != DialogResult.OK)
            //    return;


            dlgConfirm Confirm = new dlgConfirm();

            DialogResult Result = Confirm.ShowDialog();

            if (Result != DialogResult.OK)
                return;


            string strPost = "{";
            strPost += "  \"periodKey\": \"" + strPeriodKey + "\",";
            strPost += "  \"vatDueSales\": " + Program.SalesVAT.ToString("0.00") + ",";
            strPost += "  \"vatDueAcquisitions\": 0.00,";
            strPost += "  \"totalVatDue\": " + Program.SalesVAT.ToString("0.00") + ",";
            strPost += "  \"vatReclaimedCurrPeriod\": " + Program.PurchasesVAT.ToString("0.00") + ",";
            strPost += "  \"netVatDue\": " + Math.Abs(Program.VATDue).ToString("0.00") + ",";
            strPost += "  \"totalValueSalesExVAT\": " + Program.SalesTurnover.ToString("0") + ",";
            strPost += "  \"totalValuePurchasesExVAT\": " + Program.PurchasesTurnover.ToString("0") + ",";
            strPost += "  \"totalValueGoodsSuppliedExVAT\": 0,";
            strPost += "  \"totalAcquisitionsExVAT\": 0,";
            strPost += "  \"finalised\": true";
            strPost += "}";

            string strURL = Program.BaseURL + "/organisations/vat/" + _settings.VATNumber + "/returns";

            HttpStatusCode StatusCode = 0;
            string strReply = Http.HttpPost2(strURL, txtAccessToken.Text, strPost, ref StatusCode);
            string strFormattedReply = "";


            // Format Reply if we can
            try
            {
                strFormattedReply = JToken.Parse(strReply).ToString(Formatting.Indented);
            }
            catch
            {
                strFormattedReply = strReply;
            }



            if (StatusCode.ToString() != "Created")
            {
                // Format Post if we can
                string strFormattedPost = "";

                try
                {
                    strFormattedPost = JToken.Parse(strPost).ToString(Formatting.Indented);
                }
                catch
                {
                    strFormattedPost = strPost;
                }

                MessageBox.Show(StatusCode.ToString() + "\r\n\r\nURL: " + strURL + "\r\n\r\nPostData:\r\n" + strFormattedPost + "\r\n\r\nReply:\r\n" + strFormattedReply, "VAT Return Submission Error");
                return;
            }

            if (_settings.SubmissionReportPath.Length != 0)
            {
                string SubmissionReportFile = SubmissionReportFolderPath + "\\Submission Report " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".json";
                File.WriteAllText(SubmissionReportFile, strFormattedReply);
            }


            MessageBox.Show("VAT Return Submitted", "VAT Return Success");
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dlgAbout popup = new dlgAbout();
            popup.ShowDialog();
            popup.Dispose();
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            dlgPrivacy Privacy = new dlgPrivacy();

            DialogResult result = Privacy.ShowDialog();

            if (result != DialogResult.OK)
                Application.Exit();
        }
    }
}
