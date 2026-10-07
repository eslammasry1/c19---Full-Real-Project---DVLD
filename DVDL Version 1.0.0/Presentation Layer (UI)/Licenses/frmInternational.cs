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
    public partial class frmInternational : Form
    {
        int _UserID = -1;
        public frmInternational(int Userid)
        {
            InitializeComponent();
            _UserID = Userid;
        }
        private void _RefreshContactList()
        {
            dgvInternational.DataSource = clsInternationalLicense.GetInternationalInfo();
            lblRecords.Text = dgvInternational.RowCount.ToString();

        }
        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text == "")
            {
                dgvInternational.DataSource = clsInternationalLicense.GetInternationalInfo();
                lblRecords.Text = dgvInternational.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 1 && tbFilter.Text != "")
            {
                dgvInternational.DataSource = clsInternationalLicense.GetInternationalInfoFilterByInterID(int.Parse(tbFilter.Text));
                lblRecords.Text = dgvInternational.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 2 && tbFilter.Text != "")
            {
                dgvInternational.DataSource = clsInternationalLicense.GetInternationalInfoFilterByApplicationID(int.Parse(tbFilter.Text));
                lblRecords.Text = dgvInternational.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 3 && tbFilter.Text != "")
            {
                dgvInternational.DataSource = clsInternationalLicense.GetInternationalInfoFilterByDriverID(int.Parse(tbFilter.Text));
                lblRecords.Text = dgvInternational.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 4 && tbFilter.Text != "")
            {
                dgvInternational.DataSource = clsInternationalLicense.GetInternationalInfoFilterByLLicenseID(int.Parse(tbFilter.Text));
                lblRecords.Text = dgvInternational.RowCount.ToString();
            }

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0 )
            {
                dgvInternational.DataSource = clsInternationalLicense.GetInternationalInfo();
                tbFilter.Text = "";
                tbFilter.Visible = false;
            }
            else
                tbFilter.Visible = true;
        }

        private void frmInternational_Load(object sender, EventArgs e)
        {
            _RefreshContactList();
            cbFilter.SelectedIndex = 0;
        }

        private void showPersonDitailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternational.CurrentRow.Cells[2].Value;
            Form frm = new frmPersonInfo(clsDriver.FindByDriverID(DriverID).PersonID);
            frm.ShowDialog();
        }


        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowInternationlInfo((int)dgvInternational.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternational.CurrentRow.Cells[2].Value;
            Form frm = new frmLicenseHistory(clsDriver.FindByDriverID(DriverID).PersonID);
            frm.ShowDialog();

        }

        private void btnAddNewInter_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddNewInternationalLicense(_UserID);
            frm.ShowDialog();
            _RefreshContactList();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
