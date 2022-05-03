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
        public manufacturingEntities Context;
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
                EditUserFields(value);
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
            Context = new manufacturingEntities();
            LoadUserList();
            Roles = manufacturingEntities.GetContext().Roles.ToList();
            Roles.Add(new Role { Name = Pages.ShowUsersPage.NoFilter });
        }

        public void LoadUserList(Role filter = null)
        {
            Context.Users.Load();
            Context.UserInfoes.Load();
            if (filter == null || filter.Name == Pages.ShowUsersPage.NoFilter)
            {
                Users = Context.Users.Local;
                return;
            }
            Users = ToObservableCollection(Context.Users.Local.Where(user => user.Role.Equals(CurrentFilter.Name)));
        }
        private void EditUserFields(User user)
        {
            Classes.Navigator.EditUser = user;
            var window = new Windows.UserInfoFields();
            window.ShowDialog();
            Context.SaveChanges();
            LoadUserList(CurrentFilter);
        }
        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
