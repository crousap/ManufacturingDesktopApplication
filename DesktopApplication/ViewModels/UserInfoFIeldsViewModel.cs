using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApplication.Classes;
using DesktopApplication.DbModel;

namespace DesktopApplication.ViewModels
{
    public class UserInfoFIeldsViewModel : ViewModelBase
    {
        public User CurrentUser;
        public string FirstName
        {
            get
            {
                return CurrentUser.UserInfo.FirstName;
            }
            set
            {
                CurrentUser.UserInfo.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get { return CurrentUser.UserInfo.LastName; }
            set { CurrentUser.UserInfo.LastName = value; OnPropertyChanged(nameof(LastName)); }
        }
        public string MiddleName
        {
            get { return CurrentUser.UserInfo.MiddleName; }
            set { CurrentUser.UserInfo.MiddleName = value; OnPropertyChanged(nameof(MiddleName));}
        }
        public DateTime BirthDate
        {
            get { return CurrentUser.UserInfo.BirthDate; }
            set { CurrentUser.UserInfo.BirthDate = value; OnPropertyChanged(nameof(BirthDate));}
        }
        public string PhoneNumber
        {
            get { return CurrentUser.UserInfo.PhoneNumber; }
            set { CurrentUser.UserInfo.PhoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }
        public string Email
        {
            get { return CurrentUser.UserInfo.Email; }
            set { CurrentUser.UserInfo.Email = value; OnPropertyChanged(nameof(Email)); }
        }
        public string ResidantialAddress
        {
            get => CurrentUser.UserInfo.ResidentialAddress;
            set { CurrentUser.UserInfo.ResidentialAddress = value; OnPropertyChanged(nameof(ResidantialAddress)); }
        }
        public UserInfoFIeldsViewModel()
        {
            CurrentUser = Navigator.EditUser;
        }

    }
}
