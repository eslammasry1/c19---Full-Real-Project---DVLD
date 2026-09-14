namespace _19___Full_Real_Project___DVLD2
{
    partial class frmTestApointment
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblApointmentTitle = new System.Windows.Forms.Label();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labl3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.cntrlAppAndLDLAInfo1 = new _19___Full_Real_Project___DVLD2.cntrlAppAndLDLAInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddApointments = new System.Windows.Forms.Button();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbExamIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExamIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblApointmentTitle
            // 
            this.lblApointmentTitle.AutoSize = true;
            this.lblApointmentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApointmentTitle.ForeColor = System.Drawing.Color.Maroon;
            this.lblApointmentTitle.Location = new System.Drawing.Point(313, 127);
            this.lblApointmentTitle.Name = "lblApointmentTitle";
            this.lblApointmentTitle.Size = new System.Drawing.Size(411, 38);
            this.lblApointmentTitle.TabIndex = 58;
            this.lblApointmentTitle.Text = "Vision Test Appointmants";
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.AllowUserToOrderColumns = true;
            this.dgvAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointments.BackgroundColor = System.Drawing.Color.White;
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAppointments.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAppointments.Location = new System.Drawing.Point(12, 619);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.RowHeadersWidth = 51;
            this.dgvAppointments.RowTemplate.Height = 24;
            this.dgvAppointments.Size = new System.Drawing.Size(1024, 172);
            this.dgvAppointments.TabIndex = 59;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTastToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(225, 104);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // labl3
            // 
            this.labl3.AutoSize = true;
            this.labl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labl3.Location = new System.Drawing.Point(12, 577);
            this.labl3.Name = "labl3";
            this.labl3.Size = new System.Drawing.Size(137, 22);
            this.labl3.TabIndex = 61;
            this.labl3.Text = "Appointments:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 806);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 22);
            this.label1.TabIndex = 62;
            this.label1.Text = "# Records:";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(117, 806);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(32, 22);
            this.lblRecords.TabIndex = 63;
            this.lblRecords.Text = "00";
            // 
            // cntrlAppAndLDLAInfo1
            // 
            this.cntrlAppAndLDLAInfo1.BackColor = System.Drawing.Color.White;
            this.cntrlAppAndLDLAInfo1.Location = new System.Drawing.Point(12, 156);
            this.cntrlAppAndLDLAInfo1.Name = "cntrlAppAndLDLAInfo1";
            this.cntrlAppAndLDLAInfo1.Size = new System.Drawing.Size(1025, 418);
            this.cntrlAppAndLDLAInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::_19___Full_Real_Project___DVLD2.Properties.Resources.icons8_close_24;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(927, 798);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 43);
            this.btnClose.TabIndex = 64;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddApointments
            // 
            this.btnAddApointments.BackColor = System.Drawing.Color.White;
            this.btnAddApointments.BackgroundImage = global::_19___Full_Real_Project___DVLD2.Properties.Resources.add_Appoimant_icon;
            this.btnAddApointments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddApointments.Location = new System.Drawing.Point(982, 564);
            this.btnAddApointments.Name = "btnAddApointments";
            this.btnAddApointments.Size = new System.Drawing.Size(54, 50);
            this.btnAddApointments.TabIndex = 60;
            this.btnAddApointments.UseVisualStyleBackColor = false;
            this.btnAddApointments.Click += new System.EventHandler(this.btnAddApointments_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editToolStripMenuItem.Image = global::_19___Full_Real_Project___DVLD2.Properties.Resources.icons8_edit_301;
            this.editToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(224, 36);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTastToolStripMenuItem
            // 
            this.takeTastToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.takeTastToolStripMenuItem.Image = global::_19___Full_Real_Project___DVLD2.Properties.Resources.icons8_paper_241;
            this.takeTastToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.takeTastToolStripMenuItem.Name = "takeTastToolStripMenuItem";
            this.takeTastToolStripMenuItem.Size = new System.Drawing.Size(224, 36);
            this.takeTastToolStripMenuItem.Text = "Take Test";
            this.takeTastToolStripMenuItem.Click += new System.EventHandler(this.takeTastToolStripMenuItem_Click);
            // 
            // pbExamIcon
            // 
            this.pbExamIcon.Image = global::_19___Full_Real_Project___DVLD2.Properties.Resources.street_exam_icon_21;
            this.pbExamIcon.Location = new System.Drawing.Point(436, 0);
            this.pbExamIcon.Name = "pbExamIcon";
            this.pbExamIcon.Size = new System.Drawing.Size(141, 125);
            this.pbExamIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbExamIcon.TabIndex = 9;
            this.pbExamIcon.TabStop = false;
            // 
            // frmTestApointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1050, 849);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labl3);
            this.Controls.Add(this.btnAddApointments);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.lblApointmentTitle);
            this.Controls.Add(this.pbExamIcon);
            this.Controls.Add(this.cntrlAppAndLDLAInfo1);
            this.Name = "frmTestApointment";
            this.Text = "TestAppointment";
            this.Load += new System.EventHandler(this.frmScheduleTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbExamIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private cntrlAppAndLDLAInfo cntrlAppAndLDLAInfo1;
        private System.Windows.Forms.PictureBox pbExamIcon;
        private System.Windows.Forms.Label lblApointmentTitle;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.Button btnAddApointments;
        private System.Windows.Forms.Label labl3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTastToolStripMenuItem;
    }
}