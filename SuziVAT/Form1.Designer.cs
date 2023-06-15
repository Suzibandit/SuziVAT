namespace SuziVAT
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetAccessToken = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtAccessToken = new System.Windows.Forms.TextBox();
            this.lblAccessToken = new System.Windows.Forms.Label();
            this.txtRefreshToken = new System.Windows.Forms.TextBox();
            this.lblRefreshToken = new System.Windows.Forms.Label();
            this.txtScope = new System.Windows.Forms.TextBox();
            this.txtExpires = new System.Windows.Forms.TextBox();
            this.lblScope = new System.Windows.Forms.Label();
            this.lblExpiresIn = new System.Windows.Forms.Label();
            this.txtAuthCode = new System.Windows.Forms.TextBox();
            this.lblAuthCode = new System.Windows.Forms.Label();
            this.btnGetAuthCode = new System.Windows.Forms.Button();
            this.txtGetObligations = new System.Windows.Forms.Button();
            this.lbObligations = new System.Windows.Forms.ListBox();
            this.lblSelectedObligation = new System.Windows.Forms.Label();
            this.btnGetVATFigures = new System.Windows.Forms.Button();
            this.lblSalesVat = new System.Windows.Forms.Label();
            this.lblPurchasesVAT = new System.Windows.Forms.Label();
            this.lblSalesTurnover = new System.Windows.Forms.Label();
            this.lblPurchasesTurnover = new System.Windows.Forms.Label();
            this.txtSalesVAT = new System.Windows.Forms.TextBox();
            this.txtSalesTurnover = new System.Windows.Forms.TextBox();
            this.txtPurchasesVAT = new System.Windows.Forms.TextBox();
            this.txtPurchasesTurnover = new System.Windows.Forms.TextBox();
            this.btnSubmitVATReturn = new System.Windows.Forms.Button();
            this.lblPound1 = new System.Windows.Forms.Label();
            this.lblPound2 = new System.Windows.Forms.Label();
            this.lblPound3 = new System.Windows.Forms.Label();
            this.lblPound4 = new System.Windows.Forms.Label();
            this.txtNetVAT = new System.Windows.Forms.TextBox();
            this.lblNetVAT = new System.Windows.Forms.Label();
            this.lblPound5 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblObligations = new System.Windows.Forms.Label();
            this.btnRefreshAccessToken = new System.Windows.Forms.Button();
            this.lnkAbout = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnGetAccessToken
            // 
            this.btnGetAccessToken.Location = new System.Drawing.Point(84, 107);
            this.btnGetAccessToken.Name = "btnGetAccessToken";
            this.btnGetAccessToken.Size = new System.Drawing.Size(104, 23);
            this.btnGetAccessToken.TabIndex = 3;
            this.btnGetAccessToken.Text = "Get Access Token";
            this.btnGetAccessToken.UseVisualStyleBackColor = true;
            this.btnGetAccessToken.Click += new System.EventHandler(this.btnGetAccessToken_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(472, 365);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(101, 37);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtAccessToken
            // 
            this.txtAccessToken.Font = new System.Drawing.Font("Courier New", 8.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccessToken.Location = new System.Drawing.Point(84, 136);
            this.txtAccessToken.Name = "txtAccessToken";
            this.txtAccessToken.ReadOnly = true;
            this.txtAccessToken.Size = new System.Drawing.Size(242, 20);
            this.txtAccessToken.TabIndex = 6;
            // 
            // lblAccessToken
            // 
            this.lblAccessToken.AutoSize = true;
            this.lblAccessToken.Location = new System.Drawing.Point(5, 139);
            this.lblAccessToken.Name = "lblAccessToken";
            this.lblAccessToken.Size = new System.Drawing.Size(73, 13);
            this.lblAccessToken.TabIndex = 7;
            this.lblAccessToken.Text = "AccessToken";
            // 
            // txtRefreshToken
            // 
            this.txtRefreshToken.Font = new System.Drawing.Font("Courier New", 8.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefreshToken.Location = new System.Drawing.Point(84, 158);
            this.txtRefreshToken.Name = "txtRefreshToken";
            this.txtRefreshToken.ReadOnly = true;
            this.txtRefreshToken.Size = new System.Drawing.Size(242, 20);
            this.txtRefreshToken.TabIndex = 8;
            // 
            // lblRefreshToken
            // 
            this.lblRefreshToken.AutoSize = true;
            this.lblRefreshToken.Location = new System.Drawing.Point(5, 160);
            this.lblRefreshToken.Name = "lblRefreshToken";
            this.lblRefreshToken.Size = new System.Drawing.Size(75, 13);
            this.lblRefreshToken.TabIndex = 9;
            this.lblRefreshToken.Text = "RefreshToken";
            // 
            // txtScope
            // 
            this.txtScope.Font = new System.Drawing.Font("Courier New", 8.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScope.Location = new System.Drawing.Point(84, 180);
            this.txtScope.Name = "txtScope";
            this.txtScope.ReadOnly = true;
            this.txtScope.Size = new System.Drawing.Size(138, 20);
            this.txtScope.TabIndex = 10;
            // 
            // txtExpires
            // 
            this.txtExpires.Font = new System.Drawing.Font("Courier New", 8.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpires.Location = new System.Drawing.Point(84, 202);
            this.txtExpires.Name = "txtExpires";
            this.txtExpires.ReadOnly = true;
            this.txtExpires.Size = new System.Drawing.Size(242, 20);
            this.txtExpires.TabIndex = 11;
            // 
            // lblScope
            // 
            this.lblScope.AutoSize = true;
            this.lblScope.Location = new System.Drawing.Point(5, 181);
            this.lblScope.Name = "lblScope";
            this.lblScope.Size = new System.Drawing.Size(38, 13);
            this.lblScope.TabIndex = 12;
            this.lblScope.Text = "Scope";
            // 
            // lblExpiresIn
            // 
            this.lblExpiresIn.AutoSize = true;
            this.lblExpiresIn.Location = new System.Drawing.Point(5, 202);
            this.lblExpiresIn.Name = "lblExpiresIn";
            this.lblExpiresIn.Size = new System.Drawing.Size(41, 13);
            this.lblExpiresIn.TabIndex = 13;
            this.lblExpiresIn.Text = "Expires";
            // 
            // txtAuthCode
            // 
            this.txtAuthCode.Font = new System.Drawing.Font("Courier New", 8.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAuthCode.Location = new System.Drawing.Point(84, 50);
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(242, 20);
            this.txtAuthCode.TabIndex = 0;
            this.txtAuthCode.Leave += new System.EventHandler(this.txtAuthCode_TextChanged);
            // 
            // lblAuthCode
            // 
            this.lblAuthCode.AutoSize = true;
            this.lblAuthCode.Location = new System.Drawing.Point(10, 51);
            this.lblAuthCode.Name = "lblAuthCode";
            this.lblAuthCode.Size = new System.Drawing.Size(57, 13);
            this.lblAuthCode.TabIndex = 1;
            this.lblAuthCode.Text = "Auth Code";
            // 
            // btnGetAuthCode
            // 
            this.btnGetAuthCode.Location = new System.Drawing.Point(84, 21);
            this.btnGetAuthCode.Name = "btnGetAuthCode";
            this.btnGetAuthCode.Size = new System.Drawing.Size(145, 23);
            this.btnGetAuthCode.TabIndex = 2;
            this.btnGetAuthCode.Text = "Get Authorisation Code";
            this.btnGetAuthCode.UseVisualStyleBackColor = true;
            this.btnGetAuthCode.Click += new System.EventHandler(this.btnGetAuthCode_Click);
            // 
            // txtGetObligations
            // 
            this.txtGetObligations.Location = new System.Drawing.Point(84, 261);
            this.txtGetObligations.Name = "txtGetObligations";
            this.txtGetObligations.Size = new System.Drawing.Size(118, 23);
            this.txtGetObligations.TabIndex = 18;
            this.txtGetObligations.Text = "Get Obligations";
            this.txtGetObligations.UseVisualStyleBackColor = true;
            this.txtGetObligations.Click += new System.EventHandler(this.txtGetObligations_Click);
            // 
            // lbObligations
            // 
            this.lbObligations.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbObligations.FormattingEnabled = true;
            this.lbObligations.ItemHeight = 14;
            this.lbObligations.Location = new System.Drawing.Point(84, 294);
            this.lbObligations.Name = "lbObligations";
            this.lbObligations.Size = new System.Drawing.Size(171, 60);
            this.lbObligations.TabIndex = 20;
            this.lbObligations.SelectedIndexChanged += new System.EventHandler(this.lbObligations_SelectedIndexChanged);
            // 
            // lblSelectedObligation
            // 
            this.lblSelectedObligation.AutoSize = true;
            this.lblSelectedObligation.Location = new System.Drawing.Point(81, 400);
            this.lblSelectedObligation.Name = "lblSelectedObligation";
            this.lblSelectedObligation.Size = new System.Drawing.Size(0, 13);
            this.lblSelectedObligation.TabIndex = 22;
            // 
            // btnGetVATFigures
            // 
            this.btnGetVATFigures.Location = new System.Drawing.Point(370, 21);
            this.btnGetVATFigures.Name = "btnGetVATFigures";
            this.btnGetVATFigures.Size = new System.Drawing.Size(118, 23);
            this.btnGetVATFigures.TabIndex = 23;
            this.btnGetVATFigures.Text = "Get VAT Figures";
            this.btnGetVATFigures.UseVisualStyleBackColor = true;
            this.btnGetVATFigures.Click += new System.EventHandler(this.btnGetVATFigures_Click);
            // 
            // lblSalesVat
            // 
            this.lblSalesVat.AutoSize = true;
            this.lblSalesVat.Location = new System.Drawing.Point(367, 66);
            this.lblSalesVat.Name = "lblSalesVat";
            this.lblSalesVat.Size = new System.Drawing.Size(57, 13);
            this.lblSalesVat.TabIndex = 24;
            this.lblSalesVat.Text = "Sales VAT";
            // 
            // lblPurchasesVAT
            // 
            this.lblPurchasesVAT.AutoSize = true;
            this.lblPurchasesVAT.Location = new System.Drawing.Point(367, 92);
            this.lblPurchasesVAT.Name = "lblPurchasesVAT";
            this.lblPurchasesVAT.Size = new System.Drawing.Size(81, 13);
            this.lblPurchasesVAT.TabIndex = 25;
            this.lblPurchasesVAT.Text = "Purchases VAT";
            // 
            // lblSalesTurnover
            // 
            this.lblSalesTurnover.AutoSize = true;
            this.lblSalesTurnover.Location = new System.Drawing.Point(367, 128);
            this.lblSalesTurnover.Name = "lblSalesTurnover";
            this.lblSalesTurnover.Size = new System.Drawing.Size(79, 13);
            this.lblSalesTurnover.TabIndex = 26;
            this.lblSalesTurnover.Text = "Sales Turnover";
            // 
            // lblPurchasesTurnover
            // 
            this.lblPurchasesTurnover.AutoSize = true;
            this.lblPurchasesTurnover.Location = new System.Drawing.Point(367, 154);
            this.lblPurchasesTurnover.Name = "lblPurchasesTurnover";
            this.lblPurchasesTurnover.Size = new System.Drawing.Size(103, 13);
            this.lblPurchasesTurnover.TabIndex = 27;
            this.lblPurchasesTurnover.Text = "Purchases Turnover";
            // 
            // txtSalesVAT
            // 
            this.txtSalesVAT.Location = new System.Drawing.Point(502, 63);
            this.txtSalesVAT.Name = "txtSalesVAT";
            this.txtSalesVAT.ReadOnly = true;
            this.txtSalesVAT.Size = new System.Drawing.Size(96, 20);
            this.txtSalesVAT.TabIndex = 28;
            // 
            // txtSalesTurnover
            // 
            this.txtSalesTurnover.Location = new System.Drawing.Point(502, 125);
            this.txtSalesTurnover.Name = "txtSalesTurnover";
            this.txtSalesTurnover.ReadOnly = true;
            this.txtSalesTurnover.Size = new System.Drawing.Size(96, 20);
            this.txtSalesTurnover.TabIndex = 29;
            // 
            // txtPurchasesVAT
            // 
            this.txtPurchasesVAT.Location = new System.Drawing.Point(502, 89);
            this.txtPurchasesVAT.Name = "txtPurchasesVAT";
            this.txtPurchasesVAT.ReadOnly = true;
            this.txtPurchasesVAT.Size = new System.Drawing.Size(96, 20);
            this.txtPurchasesVAT.TabIndex = 30;
            // 
            // txtPurchasesTurnover
            // 
            this.txtPurchasesTurnover.Location = new System.Drawing.Point(502, 151);
            this.txtPurchasesTurnover.Name = "txtPurchasesTurnover";
            this.txtPurchasesTurnover.ReadOnly = true;
            this.txtPurchasesTurnover.Size = new System.Drawing.Size(96, 20);
            this.txtPurchasesTurnover.TabIndex = 31;
            // 
            // btnSubmitVATReturn
            // 
            this.btnSubmitVATReturn.Location = new System.Drawing.Point(384, 287);
            this.btnSubmitVATReturn.Name = "btnSubmitVATReturn";
            this.btnSubmitVATReturn.Size = new System.Drawing.Size(201, 50);
            this.btnSubmitVATReturn.TabIndex = 32;
            this.btnSubmitVATReturn.Text = "Submit VAT Return";
            this.btnSubmitVATReturn.UseVisualStyleBackColor = true;
            this.btnSubmitVATReturn.Click += new System.EventHandler(this.btnSubmitVATReturn_Click);
            // 
            // lblPound1
            // 
            this.lblPound1.AutoSize = true;
            this.lblPound1.Location = new System.Drawing.Point(483, 66);
            this.lblPound1.Name = "lblPound1";
            this.lblPound1.Size = new System.Drawing.Size(13, 13);
            this.lblPound1.TabIndex = 33;
            this.lblPound1.Text = "£";
            // 
            // lblPound2
            // 
            this.lblPound2.AutoSize = true;
            this.lblPound2.Location = new System.Drawing.Point(483, 92);
            this.lblPound2.Name = "lblPound2";
            this.lblPound2.Size = new System.Drawing.Size(13, 13);
            this.lblPound2.TabIndex = 34;
            this.lblPound2.Text = "£";
            // 
            // lblPound3
            // 
            this.lblPound3.AutoSize = true;
            this.lblPound3.Location = new System.Drawing.Point(483, 128);
            this.lblPound3.Name = "lblPound3";
            this.lblPound3.Size = new System.Drawing.Size(13, 13);
            this.lblPound3.TabIndex = 35;
            this.lblPound3.Text = "£";
            // 
            // lblPound4
            // 
            this.lblPound4.AutoSize = true;
            this.lblPound4.Location = new System.Drawing.Point(483, 154);
            this.lblPound4.Name = "lblPound4";
            this.lblPound4.Size = new System.Drawing.Size(13, 13);
            this.lblPound4.TabIndex = 36;
            this.lblPound4.Text = "£";
            // 
            // txtNetVAT
            // 
            this.txtNetVAT.Location = new System.Drawing.Point(502, 234);
            this.txtNetVAT.Name = "txtNetVAT";
            this.txtNetVAT.ReadOnly = true;
            this.txtNetVAT.Size = new System.Drawing.Size(96, 20);
            this.txtNetVAT.TabIndex = 37;
            // 
            // lblNetVAT
            // 
            this.lblNetVAT.AutoSize = true;
            this.lblNetVAT.Location = new System.Drawing.Point(352, 237);
            this.lblNetVAT.Name = "lblNetVAT";
            this.lblNetVAT.Size = new System.Drawing.Size(113, 13);
            this.lblNetVAT.TabIndex = 38;
            this.lblNetVAT.Text = "     VAT Due to HMRC";
            // 
            // lblPound5
            // 
            this.lblPound5.AutoSize = true;
            this.lblPound5.Location = new System.Drawing.Point(483, 237);
            this.lblPound5.Name = "lblPound5";
            this.lblPound5.Size = new System.Drawing.Size(13, 13);
            this.lblPound5.TabIndex = 39;
            this.lblPound5.Text = "£";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(370, 186);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(118, 23);
            this.btnCalculate.TabIndex = 40;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblObligations
            // 
            this.lblObligations.AutoSize = true;
            this.lblObligations.Location = new System.Drawing.Point(69, 360);
            this.lblObligations.Name = "lblObligations";
            this.lblObligations.Size = new System.Drawing.Size(207, 26);
            this.lblObligations.TabIndex = 41;
            this.lblObligations.Text = "Please click on the Outstanding Obligation\r\nthat you want to Submit a VAT Return " +
    "for.";
            this.lblObligations.Visible = false;
            // 
            // btnRefreshAccessToken
            // 
            this.btnRefreshAccessToken.Location = new System.Drawing.Point(197, 107);
            this.btnRefreshAccessToken.Name = "btnRefreshAccessToken";
            this.btnRefreshAccessToken.Size = new System.Drawing.Size(129, 23);
            this.btnRefreshAccessToken.TabIndex = 42;
            this.btnRefreshAccessToken.Text = "Refresh Access Token";
            this.btnRefreshAccessToken.UseVisualStyleBackColor = true;
            this.btnRefreshAccessToken.Click += new System.EventHandler(this.btnRefreshAccessToken_Click);
            // 
            // lnkAbout
            // 
            this.lnkAbout.AutoSize = true;
            this.lnkAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAbout.Location = new System.Drawing.Point(402, 374);
            this.lnkAbout.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lnkAbout.Name = "lnkAbout";
            this.lnkAbout.Size = new System.Drawing.Size(42, 16);
            this.lnkAbout.TabIndex = 43;
            this.lnkAbout.TabStop = true;
            this.lnkAbout.Text = "about";
            this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAbout_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 433);
            this.Controls.Add(this.lnkAbout);
            this.Controls.Add(this.btnRefreshAccessToken);
            this.Controls.Add(this.lblObligations);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.lblPound5);
            this.Controls.Add(this.lblNetVAT);
            this.Controls.Add(this.txtNetVAT);
            this.Controls.Add(this.lblPound4);
            this.Controls.Add(this.lblPound3);
            this.Controls.Add(this.lblPound2);
            this.Controls.Add(this.lblPound1);
            this.Controls.Add(this.btnSubmitVATReturn);
            this.Controls.Add(this.txtPurchasesTurnover);
            this.Controls.Add(this.txtPurchasesVAT);
            this.Controls.Add(this.txtSalesTurnover);
            this.Controls.Add(this.txtSalesVAT);
            this.Controls.Add(this.lblPurchasesTurnover);
            this.Controls.Add(this.lblSalesTurnover);
            this.Controls.Add(this.lblPurchasesVAT);
            this.Controls.Add(this.lblSalesVat);
            this.Controls.Add(this.btnGetVATFigures);
            this.Controls.Add(this.lblSelectedObligation);
            this.Controls.Add(this.lbObligations);
            this.Controls.Add(this.txtGetObligations);
            this.Controls.Add(this.lblExpiresIn);
            this.Controls.Add(this.lblScope);
            this.Controls.Add(this.txtExpires);
            this.Controls.Add(this.txtScope);
            this.Controls.Add(this.lblRefreshToken);
            this.Controls.Add(this.txtRefreshToken);
            this.Controls.Add(this.lblAccessToken);
            this.Controls.Add(this.txtAccessToken);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGetAccessToken);
            this.Controls.Add(this.btnGetAuthCode);
            this.Controls.Add(this.lblAuthCode);
            this.Controls.Add(this.txtAuthCode);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Suzi Bandit Ltd. - Submit MTD VAT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGetAccessToken;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtAccessToken;
        private System.Windows.Forms.Label lblAccessToken;
        private System.Windows.Forms.TextBox txtRefreshToken;
        private System.Windows.Forms.Label lblRefreshToken;
        private System.Windows.Forms.TextBox txtScope;
        private System.Windows.Forms.TextBox txtExpires;
        private System.Windows.Forms.Label lblScope;
        private System.Windows.Forms.Label lblExpiresIn;
        private System.Windows.Forms.TextBox txtAuthCode;
        private System.Windows.Forms.Label lblAuthCode;
        private System.Windows.Forms.Button btnGetAuthCode;
        private System.Windows.Forms.Button txtGetObligations;
        private System.Windows.Forms.ListBox lbObligations;
        private System.Windows.Forms.Label lblSelectedObligation;
        private System.Windows.Forms.Button btnGetVATFigures;
        private System.Windows.Forms.Label lblSalesVat;
        private System.Windows.Forms.Label lblPurchasesVAT;
        private System.Windows.Forms.Label lblSalesTurnover;
        private System.Windows.Forms.Label lblPurchasesTurnover;
        private System.Windows.Forms.TextBox txtSalesVAT;
        private System.Windows.Forms.TextBox txtSalesTurnover;
        private System.Windows.Forms.TextBox txtPurchasesVAT;
        private System.Windows.Forms.TextBox txtPurchasesTurnover;
        private System.Windows.Forms.Button btnSubmitVATReturn;
        private System.Windows.Forms.Label lblPound1;
        private System.Windows.Forms.Label lblPound2;
        private System.Windows.Forms.Label lblPound3;
        private System.Windows.Forms.Label lblPound4;
        private System.Windows.Forms.TextBox txtNetVAT;
        private System.Windows.Forms.Label lblNetVAT;
        private System.Windows.Forms.Label lblPound5;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblObligations;
        private System.Windows.Forms.Button btnRefreshAccessToken;
        private System.Windows.Forms.LinkLabel lnkAbout;
    }
}

