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
    public partial class cntrlFindPerson : UserControl
    {
        public int PersonId = -1;
        public cntrlFindPerson()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbFiltter.SelectedIndex == 0)
            {
                if (string.IsNullOrWhiteSpace(tbFilter.Text))
                {
                    MessageBox.Show("Please enter a National Number.");
                    return;
                }

                //if (clsPeople.Find(tbFilter.Text) == null)
                //{
                //    MessageBox.Show("This National No does not exist.");
                //    return;
                //}

                cntrlPersonInfo1.LoadPersonByNationalNo(tbFilter.Text);
                PersonId = cntrlPersonInfo1.PersonId;

            }
            if (cbFiltter.SelectedIndex == 1) //البحث باستخدام person ID
            {
                if (!int.TryParse(tbFilter.Text, out int PersonID))
                {
                    MessageBox.Show("Please enter a valid Person ID.");
                    return;
                }

                //if (clsPeople.Find(PersonID) == null)
                //{
                //    MessageBox.Show("This Person ID does not exist.");
                //}
                cntrlPersonInfo1.LoadPersonByPersonID(PersonID);
                PersonId = PersonID;
            }
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFiltter.SelectedIndex == 1)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void cntrlFindPerson_Load(object sender, EventArgs e)
        {
            cbFiltter.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditInfo frm = new frmAddEditInfo(-1);
            frm.AddEditInfo += frmAddEditInfo_Databack;
            frm.ShowDialog();
            if (clsPeople.Find(frm.PersonId) == null)
                return;
            PersonId = frm.PersonId;

        }
        private void frmAddEditInfo_Databack(object sender, int PersonID)
        {
            if (clsPeople.Find(PersonID) == null)
                return;
            cntrlPersonInfo1.LoadPersonByPersonID(PersonID);
        }
        public void LoadUserInfo(int Id)
        {
            cbFiltter.SelectedIndex = 1;
            tbFilter.Text = Id.ToString();
            cntrlPersonInfo1.LoadPersonByPersonID(Id);
            groupBox1.Enabled = false;
            PersonId = Id;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cntrlPersonInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
