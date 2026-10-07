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
    public partial class frmAddEditInfo : Form
    {
        public delegate void cntrlAddEditInfoEventHandler(object sender, int PersonId);
        public event cntrlAddEditInfoEventHandler AddEditInfo;

        public int PersonId;
        private int _Id = 0;
        public frmAddEditInfo(int id)
        {
            InitializeComponent();
            _Id = id;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cntrlAddEditInfo1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            PersonId = cntrlAddEditInfo1.IDback;
            AddEditInfo.Invoke(this, PersonId);
            this.Close();
        }

        private void cntrlAddEditInfo1_Load_1(object sender, EventArgs e)
        {
            cntrlAddEditInfo1.LoadcntrlAddEdit(_Id);
        }
    }
}
