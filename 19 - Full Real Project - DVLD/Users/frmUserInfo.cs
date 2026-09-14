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
    public partial class frmUserInfo : Form
    {
        private int _UserID = -1;
        public frmUserInfo(int id)
        {
            InitializeComponent();
            _UserID = id;
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            cntrlUserInfo1.LoadUserInfo(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
