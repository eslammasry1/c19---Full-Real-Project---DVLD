namespace _19___Full_Real_Project___DVLD2
{
    partial class frmChangePassword
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
            this.cntrlEditPassword1 = new _19___Full_Real_Project___DVLD2.cntrlEditPassword();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cntrlEditPassword1
            // 
            this.cntrlEditPassword1.BackColor = System.Drawing.Color.White;
            this.cntrlEditPassword1.Location = new System.Drawing.Point(-3, 1);
            this.cntrlEditPassword1.Name = "cntrlEditPassword1";
            this.cntrlEditPassword1.Size = new System.Drawing.Size(866, 571);
            this.cntrlEditPassword1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::_19___Full_Real_Project___DVLD2.Properties.Resources.icons8_close_24;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(633, 526);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 43);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 574);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cntrlEditPassword1);
            this.Name = "frmChangePassword";
            this.Text = "frmChangePassword";
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private cntrlEditPassword cntrlEditPassword1;
        private System.Windows.Forms.Button btnClose;
    }
}