using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public const string NoFilterCaption = "Без фильтра";
        /// <summary>
        /// NoFilter роль кастыль, чтобы можно было снять фильтр.
        /// </summary>
        public readonly Role NoFilterRole;

        public manufacturingEntities Context;

        private Role _currentFilter;
        /// <summary>
        /// Текущий фильтр
        /// </summary>
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
                UpdateUserList(value);
            }
        }

        private ObservableCollection<User> _users;
        /// <summary>
        /// Листинг пользователей
        /// </summary>
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
        /// <summary>
        /// Поисковая строка
        /// </summary>
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
        /// <summary>
        /// Выбранный в данный момент пользователь из списка
        /// </summary>
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
            NoFilterRole = new Role { Name = NoFilterCaption };

            PropertyChanged += OnSearchBarChanges; // Для отслеживания изменений в поисковой строке

            Roles = Context.Roles.ToList();
            Roles.Add(NoFilterRole); // Добавляем роль затычку в список для фильтра
            CurrentFilter = NoFilterRole;

            UpdateUserList();
        }

        private void OnSearchBarChanges(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchQuery))
                UpdateUserList(SearchQuery);
        }
        public void UpdateUserList(Role filter = null)
        {
            UpdateContext();
            if (filter == null || filter.Name == NoFilterCaption)
            {
                Users = Context.Users.Local;
                return;
            }
            Users = ToObservableCollection(Context.Users.Local.Where(user => user.Role.Equals(CurrentFilter.Name)));
        }
        public void UpdateUserList(string query)
        {
            UpdateContext();
            if (string.IsNullOrEmpty(query))
                UpdateUserList();
            query = query.ToLower();
            var results = from user in Users
                          where (
                              (user.UserInfo?.FirstName.ToLower().StartsWith(query) ?? false)
                              || (user.UserInfo?.LastName.ToLower().StartsWith(query) ?? false)
                              || (user.UserInfo?.MiddleName.ToLower().StartsWith(query) ?? false)
                              || user.Login.ToLower().StartsWith(query))
                          select user;
            // Некоторые пользователи имеют только логин и пароль, но никакой персональной информации,
            // т.е. объект UserInfo является null, поэтому применил выражение к поиску UserInfo? позваляет обращаться к null объектам и в итоге вернёт false
            Users = ToObservableCollection(results);
        }
        private void UpdateContext()
        {
            Context.Users.Load();
            Context.UserInfoes.Load();
        }
        private void EditUserFields(User user)
        {
            Services.Navigator.EditUser = user;
            var window = new Windows.UserInfoFields();
            window.ShowDialog();
            Context.SaveChanges();
            UpdateUserList(CurrentFilter);
        }
        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
