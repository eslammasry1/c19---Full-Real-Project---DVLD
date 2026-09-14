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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmPeople : Form
    {
        public frmPeople()
        {
            InitializeComponent();
        }
        private void _RefreshContactList()
        {
            dataGridView1.DataSource = clsPeople.GetAllPeople();
        }
        private void _RefreshContactList(object sender, int PersonID)
        {
            dataGridView1.DataSource = clsPeople.GetAllPeople();
        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
            _RefreshContactList();
            cbFilter.SelectedIndex = 0;

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                tbFilter.Visible = false;
            }
            else
            {
                tbFilter.Visible = true;
                tbFilter.Text = "";
            }

        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text == "")
            {
                dataGridView1.DataSource = clsPeople.GetAllPeople();
            }
            if (cbFilter.SelectedIndex == 1 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterByID(int.Parse(tbFilter.Text));
            }
            if (cbFilter.SelectedIndex == 2 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterByNationalNo(tbFilter.Text);
            }
            if (cbFilter.SelectedIndex == 3 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterByFirstName(tbFilter.Text);
            }
            if (cbFilter.SelectedIndex == 4 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterBySecondName(tbFilter.Text);
            }
            if (cbFilter.SelectedIndex == 5 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterByThirdName(tbFilter.Text);
            }
            if (cbFilter.SelectedIndex == 6 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterByLastName(tbFilter.Text);
            }
            if (cbFilter.SelectedIndex == 7 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterByGendor(int.Parse(tbFilter.Text));
            }
            if (cbFilter.SelectedIndex == 8 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterByNationalit(tbFilter.Text);
            }
            if (cbFilter.SelectedIndex == 9 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterByPhone(tbFilter.Text);
            }
            if (cbFilter.SelectedIndex == 10 && tbFilter.Text != "")
            {
                dataGridView1.DataSource = clsPeople.FilterByEmail(tbFilter.Text);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEditInfo frm = new frmAddEditInfo(-1);
            frm.AddEditInfo += _RefreshContactList;
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersonInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshContactList();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditInfo(-1);
            frm.ShowDialog();
            _RefreshContactList();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditInfo frm = new frmAddEditInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.AddEditInfo += _RefreshContactList;
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUser.UserIsExistByPersonId((int)dataGridView1.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("This person cannot be deleted because they are a user.");
                return;

            }
            else if (MessageBox.Show("Are you sure you want to delete Person [" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPeople.DeletePeople((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.");
                    _RefreshContactList();
                }

                else
                    MessageBox.Show("Person is not deleted.");

            }

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Email sending service will be provided later.");

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The calling service will be made available later.");

        }
    }
}
