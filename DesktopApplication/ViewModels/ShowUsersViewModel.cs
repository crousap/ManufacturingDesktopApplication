using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
                OnPropertyChanged(nameof(CurrentFilter));
                LoadUserList(value);
            }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        private List<Role> _roles;
        public List<Role> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        public ShowUsersViewModel()
        {
            LoadUserList();
            Roles = manufacturingEntities.GetContext().Roles.ToList();
            Roles.Add(new Role { Name = Pages.ShowUsersPage.NoFilter });
        }

        public void LoadUserList(Role filter = null)
        {
            using (var ctx = new manufacturingEntities())
            {
                ctx.Users.Load();
                ctx.UserInfoes.Load();
                if (filter == null || filter.Name == Pages.ShowUsersPage.NoFilter)
                {
                    Users = ctx.Users.Local;
                    return;
                }
                Users = ToObservableCollection(ctx.Users.Local.Where(user => user.Role.Equals(CurrentFilter.Name)));
            }
        }
        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
