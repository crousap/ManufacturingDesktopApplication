using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApplication.Services;
using DesktopApplication.DbModel;

namespace DesktopApplication.ViewModels
{
    public class UserInfoFieldsViewModel : ViewModelBase
    {
        public User CurrentUser;
        public string FirstName
        {
            get
            {
                return CurrentUser.UserInfo.FirstName ?? "";
            }
            set
            {
                CurrentUser.UserInfo.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get { return CurrentUser.UserInfo.LastName ?? ""; }
            set { CurrentUser.UserInfo.LastName = value; OnPropertyChanged(nameof(LastName)); }
        }
        public string MiddleName
        {
            get { return CurrentUser.UserInfo.MiddleName ?? ""; }
            set { CurrentUser.UserInfo.MiddleName = value; OnPropertyChanged(nameof(MiddleName));}
        }
        public DateTime BirthDate
        {
            get { return CurrentUser.UserInfo.BirthDate; }
            set { CurrentUser.UserInfo.BirthDate = value; OnPropertyChanged(nameof(BirthDate));}
        }
        public string PhoneNumber
        {
            get { return CurrentUser.UserInfo.PhoneNumber ?? ""; }
            set { CurrentUser.UserInfo.PhoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }
        public string Email
        {
            get { return CurrentUser.UserInfo.Email ?? ""; }
            set { CurrentUser.UserInfo.Email = value; OnPropertyChanged(nameof(Email)); }
        }
        public string ResidantialAddress
        {
            get => CurrentUser.UserInfo.ResidentialAddress ?? "";
            set { CurrentUser.UserInfo.ResidentialAddress = value; OnPropertyChanged(nameof(ResidantialAddress)); }
        }
        public UserInfoFieldsViewModel()
        {
            //CurrentUser = Navigator.EditUser;
        }

    }
}
