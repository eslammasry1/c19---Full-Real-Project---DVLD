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
using System.IO;

namespace _19___Full_Real_Project___DVLD2
{
    public partial class frmLogin : Form
    {
        private clsUser _user;
        public int UserId = -1;

        public frmLogin()
        {
            InitializeComponent();
        }
        private string RememberMeFilePath()
        {
            return Path.Combine(Application.StartupPath, "RememberMe.txt");
        }

        private void SaveRememberMe()
        {
            string filePath = RememberMeFilePath();

            if (cbRemember.Checked)
            {
                File.WriteAllLines(filePath, new string[]
                {
            tbUserName.Text,
            tbPassword.Text
                });
            }
            else
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        private void LoadRememberMe()
        {
            string filePath = RememberMeFilePath();

            if (File.Exists(filePath))
            {
                string[] data = File.ReadAllLines(filePath);

                if (data.Length >= 2)
                {
                    tbUserName.Text = data[0];
                    tbPassword.Text = data[1];

                    cbRemember.Checked = true;
                }
            }
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (clsUser.UserIsExistByUserNameAndPassword(tbUserName.Text,tbPassword.Text))
            {
                if (clsUser.UserIsActive(tbUserName.Text))
                {
                    SaveRememberMe();
                    this.Hide();
                    _user = clsUser.Find(tbUserName.Text);
                    UserId = _user.UserID;
                    Form frm = new frmHome(UserId);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("User Is Not Active", "Activity",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Invalid Username/Password.","Wrong Credentials",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadRememberMe();
        }
    }
}
