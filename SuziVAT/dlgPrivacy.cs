using System.Windows.Forms;

namespace SuziVAT
{
    public partial class dlgPrivacy : Form
    {
        public dlgPrivacy()
        {
            InitializeComponent();

            txtPrivacy.Text = Program.PrivacyValues;
            this.Text = "SuziVAT - Submit MTD VAT " + Program.strVersion + " - Privacy Information";
        }
    }
}
