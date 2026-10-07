using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Windows.Forms;
using System.Net.Mail;
namespace _19___Full_Real_Project___DVLD2
{
    public partial class cntrlAddEditInfo : UserControl
    {
        public int IDback = 0;
        private clsPeople _Person;
        private int _ID;

        // مسار الصورة الحالية
        private string _ImagePath = null;

        // هل المستخدم اختار صورة جديدة؟
        private bool _IsNewImage = false;

        enum enMode
        {
            AddMode = 0,
            UpdataMode = 1
        }

        enMode _Mode = enMode.AddMode;

        public cntrlAddEditInfo()
        {
            InitializeComponent();
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dt = clsGetCountry.CountryList();

            foreach (DataRow DT in dt.Rows)
            {
                cbCountry.Items.Add(DT["CountryName"]);
            }
        }

        public void LoadcntrlAddEdit(int id)
        {
            _ID = id;

            if (id == -1)
            {
                _Mode = enMode.AddMode;
            }
            else
            {
                _Mode = enMode.UpdataMode;
            }

            _FillCountriesInComboBox();

            if (cbCountry.Items.Count > 0)
                cbCountry.SelectedIndex = 0;


            // =========================
            // ADD MODE
            // =========================
            if (id == -1)
            {
                lblTitle.Text = "Add New Person";
                lblID.Text = "???";
                pBTitle.Image =
                    Properties.Resources.icons8_add_user_male_96;
                dtpBirth.MaxDate = DateTime.Today.AddYears(-18);
                _Person = new clsPeople();

                _ImagePath = null;
                _IsNewImage = false;
                rbMale.Checked = true;
                if (rbMale.Checked)
                {
                    pBPerson.Image =
                        Properties.Resources.icons8_person_94;
                }
                else
                {
                    pBPerson.Image =
                        Properties.Resources.icons8_person_96;
                }

                lLRemove.Visible = false;

                return;
            }


            // =========================
            // UPDATE MODE
            // =========================

            _Person = clsPeople.Find(_ID);

            if (_Person == null)
            {
                MessageBox.Show(
                    "This form will be closed because No Contact with ID = "
                    + _ID);

                return;
            }
            IDback = _Person.ID;
            lblTitle.Text = "Edit Contact ID = " + _Person.ID;

            pBTitle.Image =
                Properties.Resources.icons8_Edit_96;

            lblID.Text = _Person.ID.ToString();
            tbFirstName.Text = _Person.FirstName;
            tbSecondName.Text = _Person.SecondName;
            tbThirdName.Text = _Person.ThirdName;
            tbLastName.Text = _Person.LastName;

            tbNationalNo.Text = _Person.NationalNo;
            tbEmail.Text = _Person.Email;
            tbAddress.Text = _Person.Address;
            tbPhone.Text = _Person.Phone;


            cbCountry.SelectedIndex =
                cbCountry.FindString(
                    clsGetCountry.FindCountry(
                        _Person.NationalityCountryID).CountryName);



            dtpBirth.Value = _Person.DateOfBirth;


            if (_Person.Gendor == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }


            // =========================
            // OLD IMAGE
            // =========================

            _ImagePath = _Person.ImagePath;
            _IsNewImage = false;


            if (!string.IsNullOrEmpty(_Person.ImagePath)
                && File.Exists(_Person.ImagePath))
            {
                pBPerson.ImageLocation =
                    _Person.ImagePath;

                lLRemove.Visible = true;
                _ImagePath = _Person.ImagePath;
                _IsNewImage = true;


            }
            else
            {
                _ImagePath = "";

                if (_Person.Gendor == 0)
                {
                    pBPerson.Image =
                        Properties.Resources.icons8_person_94;
                }
                else
                {
                    pBPerson.Image =
                        Properties.Resources.icons8_person_96;
                }

                lLRemove.Visible = false;
            }
        }


        private void cntrlAddEditInfo_Load(object sender,EventArgs e)
        {
        }


        // ==========================================
        // SELECT NEW IMAGE
        // ==========================================

        private void llSetImage_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog ofd =
                   new OpenFileDialog())
            {
                ofd.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                ofd.Title = "Select Person Image";


                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // عرض الصورة
                    pBPerson.ImageLocation =
                        ofd.FileName;

                    // حفظ المسار
                    _ImagePath =
                        ofd.FileName;

                    // دي صورة جديدة
                    _IsNewImage = true;

                    lLRemove.Visible = true;
                }
            }
        }


        // ==========================================
        // REMOVE IMAGE
        // ==========================================

        private void lLRemove_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            // امسح مسار الصورة
            _ImagePath = null;

            _IsNewImage = true;


            // امسح ImageLocation
            pBPerson.ImageLocation = null;


            // حط الصورة الافتراضية
            if (rbMale.Checked)
            {
                pBPerson.Image =
                    Properties.Resources.icons8_person_94;
            }
            else
            {
                pBPerson.Image =
                    Properties.Resources.icons8_person_96;
            }


            lLRemove.Visible = false;
        }


        // ==========================================
        // SAVE
        // ==========================================

        private void btnSave_Click(object sender,EventArgs e)
        {
            if (tbAddress.Text == "" || tbNationalNo.Text == "" || tbFirstName.Text == "" || tbThirdName.Text == "" || tbSecondName.Text == "" ||
                tbLastName.Text == "" )
            {
                MessageBox.Show("The necessary data must be entered!.","eror 405",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Person.FirstName =
                tbFirstName.Text;

            _Person.SecondName =
                tbSecondName.Text;

            _Person.ThirdName =
                tbThirdName.Text;

            _Person.LastName =
                tbLastName.Text;

            _Person.NationalNo =
                tbNationalNo.Text;

            _Person.Email =
                tbEmail.Text;

            _Person.Address =
                tbAddress.Text;

            _Person.Phone =
                tbPhone.Text;
            _Person.NationalityCountryID = cbCountry.SelectedIndex + 1;

            if (rbMale.Checked)
            {
                _Person.Gendor = 0;
            }
            else if (rbFemale.Checked)
            {
                _Person.Gendor = 1;
            }


            // ======================================
            // IMAGE
            // ======================================

            if (_IsNewImage)
            {
                // لو اختار صورة جديدة
                // _ImagePath فيه مسار الصورة

                // ولو عمل Remove
                // _ImagePath هيكون ""

                _Person.ImagePath =
                    _ImagePath;
            }
            else
            {
                _Person.ImagePath = null;

            }

            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully");
            }
            else
            {
                MessageBox.Show("ERORR : Data  IS NOT Saved Successfully");
            }
            _Mode = enMode.UpdataMode;
            lblTitle.Text = "Edit Contact ID = " + _Person.ID;
            lblID.Text = _Person.ID.ToString();
            IDback = _Person.ID;

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            pBPerson.Image = Properties.Resources.icons8_person_94;

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            pBPerson.Image = Properties.Resources.icons8_person_96;

        }

        private void tbNationalNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNationalNo.Text))
            {
                e.Cancel = true;
                tbNationalNo.Focus();
                errorProvider1.SetError(tbNationalNo, "NationalNo should have a value!");
            }
            else if (clsPeople.PersonIsExist(tbNationalNo.Text))
            {
                e.Cancel = true;
                tbNationalNo.Focus();
                errorProvider1.SetError(tbNationalNo, "NationalNo should be Unique!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbNationalNo, "");
            }
        }

        private void pBTitle_Click(object sender, EventArgs e)
        {

        }

        private void tbEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                MailAddress mail = new MailAddress(tbEmail.Text);
                errorProvider1.SetError(tbEmail, "");
                e.Cancel = false;
            }
            catch
            {
                errorProvider1.SetError(tbEmail, "Invalid Email");
                e.Cancel = true;
            }
        }

        private void tbAddress_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (clsPeople.PersonIsExist(tbAddress.Text))
            {
                e.Cancel = true;
                tbAddress.Focus();
                errorProvider1.SetError(tbAddress, "address is required.!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbAddress, "");
            }

        }

        private void tbFirstName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}