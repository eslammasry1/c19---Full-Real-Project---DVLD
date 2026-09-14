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
    public partial class frmShowInternationlInfo : Form
    {
        int _ID = -1;
        public frmShowInternationlInfo(int iD)
        {
            InitializeComponent();
            _ID = iD;
        }

        private void frmShowInternationlInfo_Load(object sender, EventArgs e)
        {
            cntrlInternationalInfo1.Load(_ID);
        }

        private void cntrlInternationalInfo1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
