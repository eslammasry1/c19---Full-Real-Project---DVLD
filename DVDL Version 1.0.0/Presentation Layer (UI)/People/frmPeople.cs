using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmPeople : Form
    {
        public frmPeople()
        {
            InitializeComponent();
        }
        private static DataTable _dtAllPeople = clsPeople.GetAllPeople();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                 "FirstName", "SecondName", "ThirdName", "LastName",
                                                 "Gendor", "DateOfBirth", "Nationality",
                                                 "Phone", "Email");

        private void _RefreshContactList()
        {
            dataGridView1.DataSource = clsPeople.GetAllPeople();
        }
        private void _RefreshContactList(object sender, int PersonID)
        {
            dataGridView1.DataSource = clsPeople.GetAllPeople();
        }
        private void _Filter ()
        {
            string FilterBy;
            switch(cbFilter.Text)
            {
                case "Person ID":
                    FilterBy = "PersonID";
                    break;
                case "National NO":
                    FilterBy = "NationalNO";
                    break;
                case "First Name":
                    FilterBy = "FirstName";
                    break;
                case "Second Name":
                    FilterBy = "SecondName";
                    break;
                case "Third Name":
                    FilterBy = "ThirdName";
                    break;
                case "Last Name":
                    FilterBy = "LastName";
                    break;
                case "Gendor":
                    FilterBy = "Gendor";
                    break;
                case "Nationality":
                    FilterBy = "Nationality";
                    break;
                case "Phone":
                    FilterBy = "Phone";
                    break;
                case "Email":
                    FilterBy = "Email";
                    break;
                default:
                    FilterBy = "None";
                    break;
            }
            if (tbFilter.Text == ""||FilterBy == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                return;
            }
            if (FilterBy == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterBy,tbFilter.Text.Trim());
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", FilterBy, tbFilter.Text.Trim());
            }
        }
        private void frmPeople_Load(object sender, EventArgs e)
        {
            //_RefreshContactList();
            dataGridView1.DataSource = _dtPeople;
            cbFilter.SelectedIndex = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Person ID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText = "National NO.";
                dataGridView1.Columns[1].Width = 60;

                dataGridView1.Columns[2].HeaderText = "First Name";
                dataGridView1.Columns[2].Width = 70;

                dataGridView1.Columns[3].HeaderText = "Second Name";
                dataGridView1.Columns[3].Width = 70;

                dataGridView1.Columns[4].HeaderText = "Third Name";
                dataGridView1.Columns[4].Width = 70;

                dataGridView1.Columns[5].HeaderText = "Last Name";
                dataGridView1.Columns[5].Width = 120;

                dataGridView1.Columns[6].HeaderText = "Gendor";
                dataGridView1.Columns[6].Width = 60;

                dataGridView1.Columns[7].HeaderText = "Date Of Birth";
                dataGridView1.Columns[7].Width = 140;

                dataGridView1.Columns[8].HeaderText = "Nationality";
                dataGridView1.Columns[8].Width = 120;

                dataGridView1.Columns[9].HeaderText = "Phone";
                dataGridView1.Columns[9].Width = 120;

                dataGridView1.Columns[10].HeaderText = "Email";
                dataGridView1.Columns[10].Width = 150;

                
            }

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
            _Filter ();
            //if (tbFilter.Text == "")
            //{
            //    dataGridView1.DataSource = clsPeople.GetAllPeople();
            //}
            //if (cbFilter.SelectedIndex == 1 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterByID(int.Parse(tbFilter.Text));
            //}
            //if (cbFilter.SelectedIndex == 2 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterByNationalNo(tbFilter.Text);
            //}
            //if (cbFilter.SelectedIndex == 3 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterByFirstName(tbFilter.Text);
            //}
            //if (cbFilter.SelectedIndex == 4 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterBySecondName(tbFilter.Text);
            //}
            //if (cbFilter.SelectedIndex == 5 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterByThirdName(tbFilter.Text);
            //}
            //if (cbFilter.SelectedIndex == 6 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterByLastName(tbFilter.Text);
            //}
            //if (cbFilter.SelectedIndex == 7 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterByGendor(int.Parse(tbFilter.Text));
            //}
            //if (cbFilter.SelectedIndex == 8 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterByNationalit(tbFilter.Text);
            //}
            //if (cbFilter.SelectedIndex == 9 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterByPhone(tbFilter.Text);
            //}
            //if (cbFilter.SelectedIndex == 10 && tbFilter.Text != "")
            //{
            //    dataGridView1.DataSource = clsPeople.FilterByEmail(tbFilter.Text);
            //}


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
