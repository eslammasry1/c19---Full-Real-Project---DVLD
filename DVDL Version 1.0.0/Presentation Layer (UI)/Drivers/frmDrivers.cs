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
    public partial class frmDrivers : Form
    {
        public frmDrivers()
        {
            InitializeComponent();
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clsDriver.GetDrivers();
            lblRecords.Text = dataGridView1.RowCount.ToString();
            cbFilter.SelectedIndex = 0;
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text == "")
            {
                dataGridView1.DataSource = clsDriver.GetDrivers();
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 1 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsDriver.GetDriversFilterByDriverID(int.Parse(tbFilter.Text));
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 2 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsDriver.GetDriversFilterByPersonID(int.Parse(tbFilter.Text));
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 3 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsDriver.GetAllDriversFilterByNationalNo(tbFilter.Text);
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }
            if (cbFilter.SelectedIndex == 4 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsDriver.GetAllUsersFilterByFullName(tbFilter.Text);
                lblRecords.Text = dataGridView1.RowCount.ToString();
            }

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                dataGridView1.DataSource = clsDriver.GetDrivers();
                lblRecords.Text = dataGridView1.RowCount.ToString();
                tbFilter.Visible = false;
            }
            else
            {
                tbFilter.Visible = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
