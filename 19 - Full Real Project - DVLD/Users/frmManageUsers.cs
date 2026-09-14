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
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
        }
        private void _RefreshContactList()
        {
            dataGridView1.DataSource = clsUser.GetAllUsers(); ;
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text == "")
            {
                dataGridView1.DataSource = clsUser.GetAllUsers();
            }
            if (cbFilter.SelectedIndex == 1 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsUser.FilterUsersByUserID(int.Parse(tbFilter.Text));
            }
            if (cbFilter.SelectedIndex == 2 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsUser.FilterUsersByUserName(tbFilter.Text);
            }
            if (cbFilter.SelectedIndex == 3 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsUser.FilterUsersByPersonID(int.Parse(tbFilter.Text));
            }
            if (cbFilter.SelectedIndex == 4 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsUser.FilterUsersByFullName(tbFilter.Text);
            }
            
        }


        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                tbFilter.Visible = false;
                cbActive.Visible = false;
                dataGridView1.DataSource = clsUser.GetAllUsers();

            }
            else if (cbFilter.SelectedIndex == 1 || cbFilter.SelectedIndex == 2 || cbFilter.SelectedIndex == 3 || cbFilter.SelectedIndex == 4)
            {
                tbFilter.Visible = true;
                cbActive.Visible = false;
                cbActive.SelectedIndex = 0;
                tbFilter.Focus();
            }
            else
            {
                cbActive.Visible = true;
                cbActive.SelectedIndex = 0;
                tbFilter.Text = "";
                tbFilter.Visible = false;
                cbActive.Focus();
            }
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshContactList();
            cbFilter.SelectedIndex = 0;
        }

        private void cbActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbActive.SelectedIndex == 0)
            {
                dataGridView1.DataSource = clsUser.GetAllUsers();
            }
            if (cbActive.SelectedIndex == 1)
            {
                dataGridView1.DataSource = clsUser.FilterUsersByIsActive(true);
            }
            if (cbActive.SelectedIndex == 2)
            {
                dataGridView1.DataSource = clsUser.FilterUsersByIsActive(false);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser(-1);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser(-1);
            frm.ShowDialog();
            _RefreshContactList();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshContactList();


        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete User [" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsUser.DeleteUser((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully.");
                    _RefreshContactList();
                }

                else
                    MessageBox.Show("Person is not deleted.");

            }
        }

        private void chanagePassword_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }
    }
}
