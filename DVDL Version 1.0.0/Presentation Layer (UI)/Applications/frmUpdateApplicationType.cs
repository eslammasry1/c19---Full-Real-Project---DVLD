using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _Id = -1;
        private clsApplicationType _ApplicationType;
        public frmUpdateApplicationType(int id)
        {
            InitializeComponent();
            _Id = id;
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(_Id);
            if (_ApplicationType == null )
            {
                return;
            }
            lblID.Text = _Id.ToString();
            tbTitle.Text = _ApplicationType.ApplicationTypeTitle;
            tbFees.Text = _ApplicationType.ApplicationFees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ApplicationType.ApplicationTypeTitle = tbTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToDecimal(tbFees.Text);
            if (_ApplicationType.UpdateApplicationType())
            {
                MessageBox.Show("Data saved successfully.");
            }
            else
            {
                MessageBox.Show("Error saving data");

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
