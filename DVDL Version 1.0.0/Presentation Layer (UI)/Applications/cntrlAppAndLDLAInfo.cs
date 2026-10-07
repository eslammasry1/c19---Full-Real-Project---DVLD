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
namespace _19___Full_Real_Project___DVLD2
{
    public partial class cntrlAppAndLDLAInfo : UserControl
    {
        private clsAppAndDLInfo _AppAndDLInfo;
        public cntrlAppAndLDLAInfo()
        {
            InitializeComponent();
        }
        public void Load(int DLid)
        {
            _AppAndDLInfo = clsAppAndDLInfo.Find(DLid);
            if (_AppAndDLInfo == null)
            {
                return;
            }
            lblDLid.Text = _AppAndDLInfo.DLid.ToString();
            lblClass.Text = _AppAndDLInfo.ClassName.ToString();
            lblPassdTests.Text = _AppAndDLInfo.PassTests.ToString() + "\\3";
            lblAppId.Text = _AppAndDLInfo.AppID.ToString();
            lblStatus.Text = (_AppAndDLInfo.AppStatus) == 1 ? "new" : ((_AppAndDLInfo.AppStatus) == 2) ? "Cancelled" : "Completed";
            lblFees.Text = _AppAndDLInfo.fees.ToString();
            lblType.Text = _AppAndDLInfo.AppTitle.ToString();
            lblFullName.Text = _AppAndDLInfo.FullName.ToString();
            lblDate.Text = _AppAndDLInfo.AppDate.ToString();
            lblStatusDate.Text = _AppAndDLInfo.StatusDate.ToString();
            lblUserName.Text = _AppAndDLInfo.UserName.ToString();
        }

        private void LlPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmPersonInfo(_AppAndDLInfo.ApplicantPersonID);  
            frm.ShowDialog();
        }
    }
}
