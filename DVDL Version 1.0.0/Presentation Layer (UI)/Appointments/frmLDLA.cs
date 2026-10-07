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
    public partial class frmLDLA : Form
    {
        int UserId = -1;
        public frmLDLA(int userID)
        {
            InitializeComponent();
            UserId = userID;
        }
        private void _RefreshContactList()
        {
            dataGridView1.DataSource = clsApplication_LDLA.GetAllDLApplication();
        }


        private void frmLDLA_Load(object sender, EventArgs e)
        {
            _RefreshContactList();
            lblstutasNote.Visible = false;
            cbFilter.SelectedIndex = 0;
            tbFilter.Visible = false;
            lblRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text == "")
            {
                dataGridView1.DataSource = clsApplication_LDLA.GetAllDLApplication();
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 1 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsApplication_LDLA.FilterByLdlaID(int.Parse(tbFilter.Text));
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 2 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsApplication_LDLA.FilterByNationalNo(tbFilter.Text);
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 3 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsApplication_LDLA.FilterByFullName(tbFilter.Text);
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 4 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsApplication_LDLA.FilterByApplicationStatus(int.Parse(tbFilter.Text));
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                dataGridView1.DataSource = clsApplication_LDLA.GetAllDLApplication();
                lblRecords.Text = dataGridView1.RowCount.ToString();
                tbFilter.Visible = false;
                lblstutasNote.Visible = false;
            }
            else if (cbFilter.SelectedIndex == 4)
            {
                lblstutasNote.Visible = true;
                tbFilter.Visible = true ;
            }
            else
            {
                tbFilter.Visible = true;
                lblstutasNote.Visible = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LDLAID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = clsScheduleTest.CountTestPassed(LDLAID) == 3 && !clsApplication_LDLA.HaveLicense(LDLAID);
            sechduleTestsToolStripMenuItem.Enabled = clsScheduleTest.CountTestPassed(LDLAID) != 3;
            editApplicationToolStripMenuItem.Enabled = !(clsApplication_LDLA.GetApplicationStatus(LDLAID) == 3);
            deleteApplicationToolStripMenuItem.Enabled = !(clsApplication_LDLA.GetApplicationStatus(LDLAID) == 3);
            cancelApplicationToolStripMenuItem.Enabled = !(clsApplication_LDLA.GetApplicationStatus(LDLAID) == 3);
            //sechduleTestsToolStripMenuItem.Enabled = !(clsApplication_LDLA.GetApplicationStatus(LDLAID) == 3);
            //issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = !(clsApplication_LDLA.GetApplicationStatus(LDLAID) == 3);
            showLicenseToolStripMenuItem.Enabled = clsLicenses.ExistLicensesByApplicationID(LDLAID);

            if (clsScheduleTest.CountTestPassed(LDLAID) == 0)
            {
                SechduleVisionTest.Enabled = true;
                SechduleWrittenTest.Enabled = false;
                SechduleStreetTest.Enabled = false;
            }
            else if (clsScheduleTest.CountTestPassed(LDLAID) == 1)
            {
                SechduleVisionTest.Enabled = false;
                SechduleWrittenTest.Enabled = true;
                SechduleStreetTest.Enabled = false;
            }
            else if (clsScheduleTest.CountTestPassed(LDLAID) == 2)
            {
                SechduleVisionTest.Enabled = false;
                SechduleWrittenTest.Enabled = false;
                SechduleStreetTest.Enabled = true;
            }
            else
            {
                SechduleWrittenTest.Enabled = false;
                SechduleVisionTest.Enabled = false;
                SechduleStreetTest.Enabled = false;
            }

        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Application [" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsApplication_LDLA.DeleteLdlaApp((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully.");
                    _RefreshContactList();
                }

                else
                    MessageBox.Show("Person is not deleted.");

            }
            lblRecords.Text = dataGridView1.RowCount.ToString();

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmIssueLicense((int)dataGridView1.CurrentRow.Cells[0].Value, UserId);
            frm.ShowDialog();
            _RefreshContactList();

        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Ldla = (int)dataGridView1.CurrentRow.Cells[0].Value;
            Form frm = new frmPersonInfo(clsApplication_LDLA.Find(Ldla).PersonID);
            frm.ShowDialog();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditLDLAapp((int)dataGridView1.CurrentRow.Cells[0].Value,UserId);
            frm.ShowDialog();
            _RefreshContactList();

        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Cancelle Application [" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {
                if (clsApplication_LDLA.AppIsCompleted((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("This request has been completed; therefore, it cannot be cancelled.");
                }
                else if (clsApplication_LDLA.AppIsCancelled((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("This request has already been cancelled.");
                }
                else if (clsApplication_LDLA.CancelleApp((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Application cancelled Successfully.");
                    _RefreshContactList();
                }

                else
                    MessageBox.Show("Person is not deleted.");

            }

        }

        private void btnAddApp_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditLDLAapp(-1,UserId);
            frm.ShowDialog();
            _RefreshContactList();
            lblRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void sechduleTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowLicenses((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsApplication_LDLA Application_LDLA = clsApplication_LDLA.Find((int)dataGridView1.CurrentRow.Cells[0].Value);
            Form frm = new frmLicenseHistory(Application_LDLA.PersonID);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void SechduleVisionTest_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestApointment(UserId, (int)dataGridView1.CurrentRow.Cells[0].Value, 1);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void SechduleWrittenTest_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestApointment(UserId, (int)dataGridView1.CurrentRow.Cells[0].Value, 2);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void SechduleStreetTest_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestApointment(UserId, (int)dataGridView1.CurrentRow.Cells[0].Value, 3);
            frm.ShowDialog();
            _RefreshContactList();

        }
    }
}
