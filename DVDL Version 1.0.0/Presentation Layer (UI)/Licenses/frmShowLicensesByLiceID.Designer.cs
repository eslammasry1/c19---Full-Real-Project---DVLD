namespace _19___Full_Real_Project___DVLD2
{
    partial class frmShowLicensesByLiceID
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.cntrlLicenses1 = new _19___Full_Real_Project___DVLD2.cntrlLicenses();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(316, 153);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(347, 42);
            this.lblTitle.TabIndex = 71;
            this.lblTitle.Text = "Driver License Info";
            // 
            // cntrlLicenses1
            // 
            this.cntrlLicenses1.BackColor = System.Drawing.Color.White;
            this.cntrlLicenses1.Location = new System.Drawing.Point(12, 188);
            this.cntrlLicenses1.Name = "cntrlLicenses1";
            this.cntrlLicenses1.Size = new System.Drawing.Size(1006, 449);
            this.cntrlLicenses1.TabIndex = 68;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::_19___Full_Real_Project___DVLD2.Properties.Resources.licenses_Driver;
            this.pictureBox8.Location = new System.Drawing.Point(410, 11);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(160, 152);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 70;
            this.pictureBox8.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::_19___Full_Real_Project___DVLD2.Properties.Resources.icons8_close_24;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(909, 526);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 43);
            this.btnClose.TabIndex = 69;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowLicensesByLiceID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1028, 579);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cntrlLicenses1);
            this.Name = "frmShowLicensesByLiceID";
            this.Text = "frmShowLicensesByLiceID";
            this.Load += new System.EventHandler(this.frmShowLicensesByLiceID_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Button btnClose;
        private cntrlLicenses cntrlLicenses1;
    }
}