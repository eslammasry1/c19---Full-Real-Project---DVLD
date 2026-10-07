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
    public partial class frmPersonInfo : Form
    {
        private int _ID = 0;
        public frmPersonInfo(int iD)
        {
            InitializeComponent();
            _ID = iD;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (clsPeople.Find(_ID) == null)
            {
                return;
            }
            cntrlPersonInfo1.LoadPersonByPersonID(_ID);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
