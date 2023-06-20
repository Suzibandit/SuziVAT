using System;
using System.Drawing;
using System.Windows.Forms;

namespace SuziVAT
{
    public partial class dlgConfirm : Form
    {
        public dlgConfirm()
        {
            InitializeComponent();

            this.Text = "SuziVAT - Submit MTD VAT " + Program.strVersion + " - Confirm VAT Figures";

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
        }
    }
}
