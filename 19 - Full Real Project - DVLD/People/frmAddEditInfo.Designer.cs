namespace _19___Full_Real_Project___DVLD2
{
    partial class frmAddEditInfo
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
            this.btnClose = new System.Windows.Forms.Button();
            this.cntrlAddEditInfo1 = new _19___Full_Real_Project___DVLD2.cntrlAddEditInfo();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::_19___Full_Real_Project___DVLD2.Properties.Resources.close2;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnClose.Location = new System.Drawing.Point(413, 406);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 54);
            this.btnClose.TabIndex = 55;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // cntrlAddEditInfo1
            // 
            this.cntrlAddEditInfo1.BackColor = System.Drawing.Color.White;
            this.cntrlAddEditInfo1.Location = new System.Drawing.Point(0, 3);
            this.cntrlAddEditInfo1.Name = "cntrlAddEditInfo1";
            this.cntrlAddEditInfo1.Size = new System.Drawing.Size(923, 487);
            this.cntrlAddEditInfo1.TabIndex = 56;
            this.cntrlAddEditInfo1.Load += new System.EventHandler(this.cntrlAddEditInfo1_Load_1);
            // 
            // frmAddEditInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 478);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cntrlAddEditInfo1);
            this.Name = "frmAddEditInfo";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private cntrlAddEditInfo cntrlAddEditInfo1;
    }
}

