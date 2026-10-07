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
    public partial class frmChangePassword : Form
    {
        private int _UserID = -1;
        public frmChangePassword(int id)
        {
            InitializeComponent();
            _UserID = id;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            cntrlEditPassword1.LoadEditUserInfo(_UserID);
        }
    }
}
