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
    public partial class fmEditTestType : Form
    {
        private clsTestType _TestType;
        private int _ID;
        public fmEditTestType(int id)
        {
            InitializeComponent();
            _ID = id;
        }

        private void fmTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_ID);
            if (_TestType == null)
                return;
            lblId.Text = _TestType.ID.ToString();
            tbtitl.Text = _TestType.Title.ToString();
            tbDescription.Text = _TestType.Description.ToString();
            tbFees.Text = _TestType.Fees.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _TestType.Title = tbtitl.Text;
            _TestType.Description = tbDescription.Text;
            _TestType.Fees = Convert.ToDecimal(tbFees.Text);
            if (_TestType.UpdateTestType())
            {
                MessageBox.Show("Data saved successfully.");
            }
            else
            {
                MessageBox.Show("Error saving data");

            }

        }
    }
}
