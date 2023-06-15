using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuziVAT
{
    public partial class dlgAbout : Form
    {
        public dlgAbout()
        {
            InitializeComponent();

            this.Label2.Text = "SuziVAT " + Program.strVersion;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.\r\n\"" + ex.Message + "\"");
            }
        }

        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited   
            // to true.  
            linkLabel1.LinkVisited = true;

            //Call the Process.Start method to open the default browser   
            //with a URL:  
            Process.Start("https://SuziBandit.ltd.uk/SuziVAT");
        }
    }
}
