using BusinessLayer;
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
    public partial class frmListDetain : Form
    {
        int _UserId = -1;
        public frmListDetain(int UserID)
        {
            InitializeComponent();
            _UserId = UserID;
        }

        private void releaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseLicense((int)dgvListDetained.CurrentRow.Cells[0].Value, _UserId);
            frm.ShowDialog();
            _RefreshContactList();

        }
        private void _RefreshContactList()
        {
            dgvListDetained.DataSource = clsDetainedLicenses.GetDetainedLicenses();
            lblRecords.Text = dgvListDetained.RowCount.ToString();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text == "")
            {
                dgvListDetained.DataSource = clsDetainedLicenses.GetDetainedLicenses();
                lblRecords.Text = dgvListDetained.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 1 && tbFilter.Text != "")
            {
                dgvListDetained.DataSource = clsDetainedLicenses.GetDetainedLicensesByDetainID(int.Parse(tbFilter.Text));
                lblRecords.Text = dgvListDetained.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 2 && tbFilter.Text != "")
            {
                dgvListDetained.DataSource = clsDetainedLicenses.GetDetainedLicensesByIsReleased(int.Parse(tbFilter.Text)==0? false : true);
                lblRecords.Text = dgvListDetained.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 3 && tbFilter.Text != "")
            {
                dgvListDetained.DataSource = clsDetainedLicenses.GetDetainedLicensesByNationalNo(tbFilter.Text);
                lblRecords.Text = dgvListDetained.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 4 && tbFilter.Text != "")
            {
                dgvListDetained.DataSource = clsDetainedLicenses.GetDetainedLicensesByFullName(tbFilter.Text);
                lblRecords.Text = dgvListDetained.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 5 && tbFilter.Text != "")
            {
                dgvListDetained.DataSource = clsDetainedLicenses.GetDetainedLicensesByReleaseApplicationID(int.Parse(tbFilter.Text));
                lblRecords.Text = dgvListDetained.RowCount.ToString();
            }

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0 )
            {
                dgvListDetained.DataSource = clsDetainedLicenses.GetDetainedLicenses();
                lblRecords.Text = dgvListDetained.RowCount.ToString();
                tbFilter.Visible = false;
                tbFilter.Text = "";
            }
            else
            {
                tbFilter.Visible = true;
                tbFilter.Focus();
            }
        }

        private void frmListDetain_Load(object sender, EventArgs e)
        {

            _RefreshContactList();
            cbFilter.SelectedIndex = 0;
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Licenseid = (int)dgvListDetained.CurrentRow.Cells[1].Value;
            int DriverId = clsLicensesInfo.FindByLicenseID(Licenseid).DriverID;
            Form frm = new frmPersonInfo(clsDriver.FindByDriverID(DriverId).PersonID);
            frm.ShowDialog();

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowLicensesByLiceID((int)dgvListDetained.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Licenseid = (int)dgvListDetained.CurrentRow.Cells[1].Value;
            int DriverId = clsLicensesInfo.FindByLicenseID(Licenseid).DriverID;
            Form frm = new frmLicenseHistory(clsDriver.FindByDriverID(DriverId).PersonID);
            frm.ShowDialog();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseToolStripMenuItem.Enabled = clsDetainedLicenses.IsLicenseDetained((int)dgvListDetained.CurrentRow.Cells[1].Value);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseLicense(-1, _UserId);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void btnDetainID_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicense( _UserId);
            frm.ShowDialog();
            _RefreshContactList();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
