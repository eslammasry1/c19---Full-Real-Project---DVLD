using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace _19___Full_Real_Project___DVLD2
{
    public partial class cntrlPersonInfo : UserControl
    {
        private clsPeople _Person;
        public int PersonId = -1;
        public cntrlPersonInfo()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
        public void LoadPersonByPersonID(int ID)
        {

            _Person = clsPeople.Find(ID);

            if (_Person == null)
            {
                MessageBox.Show("Person Not Found");
                return;
            }

            lblPersonId.Text = _Person.ID.ToString();
            LblName.Text = _Person.FirstName + " " + _Person.SecondName + " " +
                           _Person.ThirdName + " " + _Person.LastName;

            lblNationalNo.Text = _Person.NationalNo;
            lblGendor.Text = (_Person.Gendor == 0) ? "Male" : "Female";
            if (_Person.Gendor == 0)
            {
                pbGendorIcon.Image = Properties.Resources.person_man;
            }
            else
            {
                pbGendorIcon.Image = Properties.Resources.person_woman;
            }
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDate.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = clsGetCountry.FindCountry(_Person.NationalityCountryID).CountryName;

            if (!string.IsNullOrEmpty(_Person.ImagePath)
                && File.Exists(_Person.ImagePath))
            {
                pbImagePath.ImageLocation =
                    _Person.ImagePath;

            }
            else
            {

                if (_Person.Gendor == 0)
                {
                    pbImagePath.Image =
                        Properties.Resources.icons8_person_94;
                }
                else
                {
                    pbImagePath.Image =
                        Properties.Resources.icons8_person_96;
                }

            }
            PersonId = _Person.ID;
        }
        public void LoadPersonByNationalNo(string NationalNO)
        {
            

            _Person = clsPeople.Find(NationalNO);

            if (_Person == null)
            {
                MessageBox.Show("Person Not Found");
                return;
            }

            lblPersonId.Text = _Person.ID.ToString();
            LblName.Text = _Person.FirstName + " " + _Person.SecondName + " " +
                           _Person.ThirdName + " " + _Person.LastName;

            lblNationalNo.Text = _Person.NationalNo;
            lblGendor.Text = (_Person.Gendor == 0) ? "Male" : "Female";
            if (_Person.Gendor == 0)
            {
                pbGendorIcon.Image = Properties.Resources.person_man;
            }
            else
            {
                pbGendorIcon.Image = Properties.Resources.person_woman;
            }
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDate.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = clsGetCountry.FindCountry(_Person.NationalityCountryID).CountryName;

            if (!string.IsNullOrEmpty(_Person.ImagePath)
                && File.Exists(_Person.ImagePath))
            {
                pbImagePath.ImageLocation =
                    _Person.ImagePath;

            }
            else
            {

                if (_Person.Gendor == 0)
                {
                    pbImagePath.Image =
                        Properties.Resources.icons8_person_94;
                }
                else
                {
                    pbImagePath.Image =
                        Properties.Resources.icons8_person_96;
                }

            }
            PersonId = _Person.ID;

        }
        private void cntrlPersonInfo_Load(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_Person == null)
            {
                MessageBox.Show("No data to edit.","eror",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmAddEditInfo frm = new frmAddEditInfo(_Person.ID);
            frm.AddEditInfo += frmAddEditInfo_Databack;
            frm.ShowDialog();
            
        
        }
        private void frmAddEditInfo_Databack(object sender, int PersonID)
        {
            LoadPersonByPersonID(PersonID);
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }
    }
}
