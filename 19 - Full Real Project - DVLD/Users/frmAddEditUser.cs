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
    public partial class frmAddEditUser : Form
    {
        private clsUser _user;
        private int _UserId = -1;
        private int PersonID = -1;
        enum enMode { AddUserMode = 0 , UpdateUserMode = 1 }
        enMode _Mode = enMode.AddUserMode;
        public frmAddEditUser(int UserId)
        {
            InitializeComponent();
            _UserId = UserId;
            if (_UserId == -1 ) 
                _Mode = enMode.AddUserMode;
            else 
                _Mode = enMode.UpdateUserMode;
        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.UpdateUserMode)
            {
                _user = clsUser.Find(_UserId);
                cntrlFindPerson1.LoadUserInfo(_user.PersonID);
                lblUserId .Text = _UserId.ToString();
                tbUserName.Text = _user.UserName;
                tbPassword.Text = _user.Password;
                tbConfirmPassword .Text = _user.Password;
                cbActive.Checked = _user.IsActive;
                lblTitle.Text = "Update User";

            }
            if (_Mode == enMode.AddUserMode)
            {
                _user = new clsUser();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddUserMode)
            {
                PersonID = cntrlFindPerson1.PersonId;
                if (PersonID == -1)
                {
                    MessageBox.Show("The person has not been selected", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (clsUser.UserIsExistByPersonId(PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    tabUserInfo.SelectedIndex = 1;
                }
            }
            else
            {
                tabUserInfo.SelectedIndex = 1;
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbConfirmPassword.Text == ""||tbPassword.Text == "" || tbUserName.Text == "")
            {
                MessageBox.Show("The necessary data must be entered!.", "eror 405", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _user.UserName = tbUserName.Text;
            _user.Password = tbPassword.Text;
            _user.IsActive = cbActive.Checked;
            _user.PersonID = PersonID;
            if (_user.Save())
            {
                MessageBox.Show("Data Saved Successfully");
            }
            else
            {
                MessageBox.Show("ERORR : Data  IS NOT Saved Successfully");
            }
            _Mode = enMode.UpdateUserMode;
            lblTitle.Text = "Update User";
            lblUserId.Text = _user.UserID.ToString();
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUserName.Text))
            {
                e.Cancel = true;
                tbUserName.Focus();
                errorProvider1.SetError(tbUserName, "NationalNo should have a value!");
            }
            else if (clsUser.UserIsExistByUserName(tbUserName.Text))
            {
                e.Cancel = true;
                tbUserName.Focus();
                errorProvider1.SetError(tbUserName, "Please enter a different username!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbUserName, "");
            }

        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                e.Cancel = true;
                tbPassword.Focus();
                errorProvider1.SetError(tbPassword, "You must enter your password.!");
            }

        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbConfirmPassword.Text)&&!string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                e.Cancel = true;
                tbConfirmPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword, "You need to Confirm your password!");
            }
            else if (tbConfirmPassword.Text !=  tbPassword.Text)
            {
                e.Cancel = true;
                tbConfirmPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword, "The password does not match!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbConfirmPassword, "");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
