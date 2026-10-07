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
    public partial class frmManageTestType : Form
    {
        public frmManageTestType()
        {
            InitializeComponent();
        }
        private void _RefreshContactList()
        {
            dataGridView1.DataSource = clsTestType.GetAllTestType(); ;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageTestType_Load(object sender, EventArgs e)
        {
            _RefreshContactList();

            dataGridView1.Columns["TestTypeDescription"].Width = 300;

            dataGridView1.Columns["TestTypeDescription"]
                .DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridView1.RowTemplate.Height = 40;
            lblRecords.Text = clsTestType.GetRowsNumber().ToString();

        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new fmEditTestType((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshContactList();

        }
    }
}
