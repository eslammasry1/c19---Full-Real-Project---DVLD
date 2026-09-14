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
    public partial class cntrlEditPassword : UserControl
    {
        private int _UserID = -1;
        clsUser _user;
        public cntrlEditPassword()
        {
            InitializeComponent();
        }
        public void LoadEditUserInfo(int userID)
        {
            _UserID = userID;         
            if(clsUser.Find(_UserID) != null)
            {
                cntrlUserInfo1.LoadUserInfo(_UserID);
            }
            _user = clsUser.Find(_UserID);
        }
        private void cntrlEditPassword_Load(object sender, EventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbNewPass.Text == "" || tbCurrentPass.Text == "" || tbConfirmPass.Text == "")
            {
                MessageBox.Show("All data must be entered.", "patchy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsUser.EditUserPassword(_UserID,tbNewPass.Text))
            {
                MessageBox.Show("The password has been successfully changed.");
            }
            else
            {
                MessageBox.Show("Error updating password.");
            }
        }

        private void tbCurrentPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbCurrentPass_Validating(object sender, CancelEventArgs e)
        {
            if (tbCurrentPass.Text != _user.Password)
            {
                e.Cancel = true;
                tbCurrentPass.Focus();
                errorProvider1.SetError(tbCurrentPass, "Incorrect password entered..!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbCurrentPass, "");
            }

        }

        private void tbNewPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNewPass.Text ))
            {
                e.Cancel = true;
                tbNewPass.Focus();
                errorProvider1.SetError(tbNewPass, "Enter your new password!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbNewPass, "");
            }

        }

        private void tbConfirmPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbConfirmPass.Text) && !string.IsNullOrWhiteSpace(tbNewPass.Text))
            {
                e.Cancel = true;
                tbConfirmPass.Focus();
                errorProvider1.SetError(tbConfirmPass, "You must verify the password!");
            }
            else if (tbConfirmPass.Text != tbNewPass.Text && !string.IsNullOrWhiteSpace(tbNewPass.Text))
            {
                e.Cancel = true;
                tbConfirmPass.Focus();
                errorProvider1.SetError(tbConfirmPass, "The password does not match!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbConfirmPass, "");
            }

        }

        //private void btnSave_Validating(object sender, CancelEventArgs e)
        //{
        //    //if (tbNewPass.Text == "" || tbCurrentPass.Text == "" || tbConfirmPass.Text == "")
        //    //{
        //    //    e.Cancel = true;
        //    //    tbConfirmPass.Focus();
        //    //    tbCurrentPass.Focus();
        //    //    tbNewPass.Focus();
        //    //    errorProvider1.SetError(tbCurrentPass, "Please enter the password.!");
        //    //    errorProvider1.SetError(tbNewPass, "Please enter the new password.!");

        //    //}

        //}
    }
}
