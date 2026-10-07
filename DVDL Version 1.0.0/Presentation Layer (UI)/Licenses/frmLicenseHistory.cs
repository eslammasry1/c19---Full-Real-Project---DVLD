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
    public partial class frmLicenseHistory : Form
    {
        int _PersonID = -1;
        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            cntrlFindPerson1.LoadUserInfo(_PersonID);
            dgvLocal.DataSource = clsLicenses.GetLicensesOfPerson(_PersonID);
            dgvInternational.DataSource = clsInternationalLicense.GetInternationalLicensesOfPerson(_PersonID);
            lblLocal.Text = dgvLocal.RowCount.ToString();
            lblInternationalLicense.Text = dgvInternational.RowCount.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowLicensesByLiceID((int)dgvLocal.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showInterLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowInternationlInfo((int)dgvInternational.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }
    }
}
