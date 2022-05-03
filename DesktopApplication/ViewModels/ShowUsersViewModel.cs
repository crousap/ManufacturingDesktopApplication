using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DesktopApplication.DbModel;
using DesktopApplication.Pages;

namespace DesktopApplication.ViewModels
{
    public class ShowUsersViewModel : ViewModelBase
    {
        private Role _currentFilter; // comboboxFilter
        public Role CurrentFilter
        {
            get
            {
                return _currentFilter;
            }
            set
            {
                _currentFilter = value;
                MessageBox.Show(((Role)value).Name);
                OnPropertyChanged(nameof(CurrentFilter));
            }
        }

        private ObservableCollection<UserInfoView> _users;

        public ObservableCollection<UserInfoView> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        private List<Role> _roles;

        public List<Role> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        public ShowUsersViewModel()
        {
            Users = manufacturingEntities.GetContext().UserInfoViews.Local;
            Roles = manufacturingEntities.GetContext().Roles.ToList();
            Roles.Add(new Role { Name = Pages.ShowUsersPage.NoFilter});
        }
    }
}
