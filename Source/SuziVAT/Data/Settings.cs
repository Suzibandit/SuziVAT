using System.IO;
using Newtonsoft.Json;

namespace SuziVAT.Data
{
    public class Settings
    {
        private Settings()
        {
        }

        public string VATNumber { get; set; }

        public string WorkBookPath { get; set; }
        public string WorkSheetName { get; set; }

        public string SalesVATCell { get; set; }
        public string PurchaseVATCell { get; set; }
        public string SalesTurnoverCell { get; set; }
        public string PurchaseTurnoverCell { get; set; }
        public string VATReturnDateCell { get; set; }
        public string CompanyYearEndDate { get; set; }
        public bool UseCompanyYearFolder { get; set; }

        public string SubmissionReportPath { get; set; }
        public string LogFilePath { get; set; }
        
        public string AuthCode { get; set; }
        public string State { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public string Expiry { get; set; }
        public string DeviceID { get; set; }
        public string RedirectURL { get; set; }
        public bool Test { get; set; }


        public static Settings Load(string Filename)
        {
            if (!File.Exists(Filename))
                return new Settings();

            return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Filename)) ?? new Settings();
        }

        public void Save(string Filename)
        {
            File.WriteAllText(Filename, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
