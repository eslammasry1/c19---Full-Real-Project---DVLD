using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmTestApointment : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestTypeID = -1;
        private int _CreatedByUserID = -1;
        public frmTestApointment(int CreatedByUserID,int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _CreatedByUserID = CreatedByUserID;
        }
        private void _IconAppointMent(int TestTypeID)
        {
            if (TestTypeID ==1)
            {
                pbExamIcon.Image =Properties.Resources.eye_icon;
            }
            else if(TestTypeID ==2)
            {
                pbExamIcon.Image = Properties.Resources.Writing_exam_icon;
            }
            else
            {
                pbExamIcon.Image = Properties.Resources.street_exam_icon_2;
            }
        }
        private void _AppointMentTitale(int TestTypeID)
        {
            if (TestTypeID ==1)
            {
                lblApointmentTitle.Text = "Vision Test Appointmants";
                this.Text = "Vision Test Appointmants";
            }
            else if(TestTypeID ==2)
            {
                lblApointmentTitle.Text = "Writing Test Appointmants";
                this.Text = "Writing Test Appointmants";
            }
            else
            {
                lblApointmentTitle.Text = "Street Test Appointmants";
                this.Text = "Street Test Appointmants";
            }
        }
        private void _RefreshContactList()
        {
            dgvAppointments.DataSource = clsScheduleTest.AppointmentInfo(_LocalDrivingLicenseApplicationID,_TestTypeID);
            lblRecords.Text = dgvAppointments.RowCount.ToString();

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            _IconAppointMent(_TestTypeID);
            _AppointMentTitale(_TestTypeID);
            _RefreshContactList();
            cntrlAppAndLDLAInfo1.Load(_LocalDrivingLicenseApplicationID);
        }

        private void cntrlAppAndLDLAInfo1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddApointments_Click(object sender, EventArgs e)
        {
            if (clsScheduleTest.IsPassThisTest(_TestTypeID, _LocalDrivingLicenseApplicationID))
            {
                MessageBox.Show("This test has already been passed; another appointment cannot be booked.", "appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (clsScheduleTest.ISExistAppointments(_TestTypeID, _LocalDrivingLicenseApplicationID))
            {
                MessageBox.Show("You already have a booked appointment.", "appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                Form frm = new frmScheduleTest(_CreatedByUserID, -1, _LocalDrivingLicenseApplicationID, _TestTypeID);
                frm.ShowDialog();
                _RefreshContactList();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmScheduleTest(_CreatedByUserID, (int)dgvAppointments.CurrentRow.Cells[0].Value, _LocalDrivingLicenseApplicationID, _TestTypeID);
            frm.ShowDialog();
            _RefreshContactList();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            editToolStripMenuItem.Enabled = !clsScheduleTest.ISLocked((int)dgvAppointments.CurrentRow.Cells[0].Value);
            takeTastToolStripMenuItem.Enabled = !clsScheduleTest.ISLocked((int)dgvAppointments.CurrentRow.Cells[0].Value);
        }

        private void takeTastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestType(_CreatedByUserID, (int)dgvAppointments.CurrentRow.Cells[0].Value, _LocalDrivingLicenseApplicationID, _TestTypeID);
            frm.ShowDialog();
            _RefreshContactList();
            cntrlAppAndLDLAInfo1.Load(_LocalDrivingLicenseApplicationID);


        }
    }
}
