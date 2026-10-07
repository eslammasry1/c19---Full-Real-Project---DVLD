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
    public partial class cntrlUserInfo : UserControl
    {
        public cntrlUserInfo()
        {
            InitializeComponent();
        }
        public void LoadUserInfo(int UserID)
        {
            clsUser _user = clsUser.Find(UserID);
            if (_user == null)
            {
                return;
            }
            cntrlPersonInfo1.LoadPersonByPersonID(_user.PersonID);
            lblUserId.Text= _user.UserID.ToString();
            lblUserName.Text= _user.UserName.ToString();
            lblIsActive.Text = _user.IsActive ? "yes" : "No";

        }
    }
}
