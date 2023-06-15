namespace SuziVAT
{
    partial class dlgPrivacy
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblExplain = new System.Windows.Forms.Label();
            this.txtPrivacy = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(127, 592);
            this.btnOK.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(84, 32);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(252, 592);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 32);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(129, 21);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(214, 26);
            this.lblHeading.TabIndex = 2;
            this.lblHeading.Text = "Privacy Confirmation";
            // 
            // lblExplain
            // 
            this.lblExplain.AutoSize = true;
            this.lblExplain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExplain.Location = new System.Drawing.Point(38, 496);
            this.lblExplain.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblExplain.Name = "lblExplain";
            this.lblExplain.Size = new System.Drawing.Size(460, 68);
            this.lblExplain.TabIndex = 27;
            this.lblExplain.Text = "HMRC mandates that the above information must be collected and sent \r\nto them whe" +
    "n going through the steps to submit your VAT return.\r\n\r\nPlease press OK to conti" +
    "nue or Cancel to exit the program\r\n";
            // 
            // txtPrivacy
            // 
            this.txtPrivacy.Font = new System.Drawing.Font("Courier New", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrivacy.Location = new System.Drawing.Point(15, 47);
            this.txtPrivacy.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.txtPrivacy.Multiline = true;
            this.txtPrivacy.Name = "txtPrivacy";
            this.txtPrivacy.ReadOnly = true;
            this.txtPrivacy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPrivacy.Size = new System.Drawing.Size(473, 420);
            this.txtPrivacy.TabIndex = 29;
            this.txtPrivacy.WordWrap = false;
            // 
            // dlgPrivacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 644);
            this.Controls.Add(this.txtPrivacy);
            this.Controls.Add(this.lblExplain);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "dlgPrivacy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dlgPrivacy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblExplain;
        private System.Windows.Forms.TextBox txtPrivacy;
    }
}