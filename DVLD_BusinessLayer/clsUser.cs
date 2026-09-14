using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsUser
    {
        public enum enMode { AddNewUser = 0, UpdateUser = 1 }
        enMode Mode = enMode.AddNewUser;


        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false; 
            Mode = enMode.AddNewUser;
        }
        public clsUser(int UserID,int PersonID,string UserName,string Password, bool isActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = isActive;
            Mode = enMode.UpdateUser;
        }
        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;
            if (clsUserDB.GetUserByUserID(UserID,ref PersonID ,ref UserName,ref Password,ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser Find(string UserName)
        {
            int PersonID = -1;
            int UserID = -1;
            string Password = "";
            bool IsActive = false;
            if (clsUserDB.GetUserByUserName(UserName,ref PersonID ,ref UserID,ref Password,ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserDB.AddNewUser(this.PersonID, this.UserName,this.Password,this.IsActive);
            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUserDB.EditUser(this.UserID ,this.UserName, this.Password, this.IsActive);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNewUser:
                    if (_AddNewUser())
                    {
                        Mode = enMode.UpdateUser;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateUser:
                    return _UpdateUser();
            }
            return false;
        }
        public static bool UserIsExistByUserNameAndPassword(string UserName, string Password)
        {
            return clsUserDB.UserIsExistByUserNameAndPassword(UserName, Password);
        }
        public static bool UserIsActive(string UserName)
        {
            return clsUserDB.UserIsActive(UserName);
        }
        public static DataTable GetAllUsers()
        {
            return clsUserDB.GetAllUsers();
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUserDB.DeleteUser(UserID);
        }
        public static DataTable FilterUsersByUserID(int UserID)
        {
            return clsUserDB.GetAllUsersFilterByUserID(UserID);
        }
        public static DataTable FilterUsersByFullName(string FullName)
        {
            return clsUserDB.GetAllUsersFilterByFullName(FullName);
        }
        public static DataTable FilterUsersByPersonID(int PersonId)
        {
            return clsUserDB.GetAllUsersFilterByPersonID(PersonId);
        }
        public static DataTable FilterUsersByUserName(string UserName)
        {
            return clsUserDB.GetAllUsersFilterByUserName(UserName);
        }
        public static DataTable FilterUsersByIsActive(bool IsActive)
        {
            return clsUserDB.GetAllUsersFilterByIsActive(IsActive);
        }

        public static bool UserIsExistByPersonId(int PersonId)
        {
           return clsUserDB.ExistUserByPersonID(PersonId);
        }
        public static bool UserIsExistByUserId(int UserID)
        {
           return clsUserDB.ExistUserByUserID(UserID);
        }
        public static bool UserIsExistByUserName(string UserName)
        {
           return clsUserDB.ExistUserByUserName(UserName);
        }
        public static bool EditUserPassword(int UserID, string Password)
        {
            return clsUserDB.EditPassword(UserID, Password);
        }
    }
}
