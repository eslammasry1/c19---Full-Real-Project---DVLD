using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsPeople
    {
        public enum enMode { AddNewPeople = 0, UpdatePeople = 1 }
        enMode Mode = enMode.AddNewPeople;
        public int ID                   { get; set; }
        public string NationalNo        { get; set; }
        public string FirstName         { get; set; }
        public string SecondName        { get; set; }
        public string ThirdName         { get; set; }
        public string LastName          { get; set; }
        public DateTime DateOfBirth     { get; set; }
        public int Gendor               { get; set; }
        public string Address           { get; set; }
        public string Phone             { get; set; }
        public string Email             { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath         { get; set; }

        public clsPeople()
        {
            ID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = new DateTime(2005, 1, 1);
            Gendor = 0;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";
            Mode = enMode.AddNewPeople;
        }
        public clsPeople(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName,
                                       string LastName, DateTime DateOfBirth, int Gendor, string Address, string Phone,
                                       string Email, int NationalityCountryID, string ImagePath)
        {
            this.ID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            Mode = enMode.UpdatePeople;

        }
        private  bool _AddNewPeople()
        {
            this.ID = clsPeopleDB.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
                                              this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return (this.ID != -1);
        }

        private bool _UpdatePeople()
        {
            return clsPeopleDB.UpdatePerson(this.ID,this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
                                  this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNewPeople:
                    if (_AddNewPeople())
                    {
                        Mode = enMode.UpdatePeople;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdatePeople:
                    return _UpdatePeople();

            }
            return false;
        }
        public static bool DeletePeople(int Id)
        {
            return clsPeopleDB.DeletePersons(Id);
        }
        public static clsPeople Find(int ID)
        {
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth =new DateTime(2005, 1, 1);
            int Gendor = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";
            if (clsPeopleDB.GetPersonInfoByID( ID, ref  NationalNo, ref  FirstName, ref  SecondName, ref  ThirdName,
                                            ref  LastName, ref  DateOfBirth, ref  Gendor, ref  Address, ref  Phone
                                            , ref  Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(ID, NationalNo, FirstName, SecondName, ThirdName,
                                             LastName, DateOfBirth, Gendor, Address, Phone
                                            , Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }

        }
        public static clsPeople Find(string NationalNo)
        {
            int ID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth =new DateTime(2005, 1, 1);
            int Gendor = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";
            if (clsPeopleDB.GetPersonInfoByNationalNo(NationalNo, ref ID, ref  FirstName, ref  SecondName, ref  ThirdName,
                                            ref  LastName, ref  DateOfBirth, ref  Gendor, ref  Address, ref  Phone
                                            , ref  Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(ID, NationalNo, FirstName, SecondName, ThirdName,
                                             LastName, DateOfBirth, Gendor, Address, Phone
                                            , Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }

        }
        public static DataTable GetAllPeople()
        {
            return clsPeopleDB.GetAllPersons();
        }
        public static DataTable FilterByID(int ID)
        {
            return clsPeopleDB.GetAllPersonsFilterByID(ID);
        }
        public static DataTable FilterByNationalNo(string NationalNo)
        {
            return clsPeopleDB.GetAllPersonsFilterByNationalNo(NationalNo);
        }
        public static DataTable FilterByFirstName(string FirstName)
        {
            return clsPeopleDB.GetAllPersonsFilterByFirstName(FirstName);
        }
        public static DataTable FilterBySecondName(string SecondName)
        {
            return clsPeopleDB.GetAllPersonsFilterBySecondName(SecondName);
        }
        public static DataTable FilterByThirdName(string ThirdName)
        {
            return clsPeopleDB.GetAllPersonsFilterByThirdName(ThirdName);
        }  
        public static DataTable FilterByLastName(string LastName)
        {
            return clsPeopleDB.GetAllPersonsFilterByLastName(LastName);
        }

        public static DataTable FilterByGendor(int G)
        {
            return clsPeopleDB.GetAllPersonsFilterByGendor(G);
        }
        public static DataTable FilterByNationalit(string Nationalit)
        {
            return clsPeopleDB.GetAllPersonsFilterByNationality(Nationalit);
        }
        public static DataTable FilterByPhone(string Phone)
        {
            return clsPeopleDB.GetAllPersonsFilterByPhone(Phone);
        }
        public static DataTable FilterByEmail(string Email)
        {
            return clsPeopleDB.GetAllPersonsFilterByEmail(Email);
        }
        public static bool PersonIsExist(int ID)
        {
            return clsPeopleDB.ExistPerson(ID);
        }

        public static bool PersonIsExist(string NationalNo)
        {
            return clsPeopleDB.ExistPerson(NationalNo);
        }

    }
}