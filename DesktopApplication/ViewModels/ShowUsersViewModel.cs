using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DesktopApplication.Commands;
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

        private User _selectedUser;
        /// <summary>
        /// Выбранный в данный момент пользователь из списка
        /// </summary>
        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                EditUserFields(value);
            }
        }

        private List<Role> _roles;
        public List<Role> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        public ICommand AddUserCommand { get; set; }

        public ShowUsersViewModel()
        {
            UpdateUserList();
            NoFilterRole = new Role { Name = NoFilterCaption }; // Объявляем роль затычку
            AddUserCommand = new AddUserCommand(this);

            PropertyChanged += OnSearchBarChanges; // Для отслеживания изменений в поисковой строке
            using (var context = new manufacturingEntities())
                Roles = context.Roles.ToList();
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
            using (var ctx = new manufacturingEntities())
            {
                if (filter == null || filter.Name == NoFilterCaption)
                {
                    ctx.Users.Load();
                    ctx.UserInfoes.Load();
                    Users = ctx.Users.Local;
                    return;
                }
                Users = ToObservableCollection(ctx.Users.Local.Where(user => user.Role.Equals(CurrentFilter.Name)));
            }
        }
        public void UpdateUserList(string query)
        {
            using (var ctx = new manufacturingEntities())
            {
                if (string.IsNullOrEmpty(query))
                    UpdateUserList();
                query = query.ToLower();
                var results = from user in ctx.Users.Local
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
        }
        private void UpdateContext()
        {
            //Context.Users.Load();
            //Context.UserInfoes.Load();
        }
        private void EditUserFields(User user)
        {
            var ctx = new manufacturingEntities();
            ctx.Users.Load();
            ctx.UserInfoes.Load();
            user = ctx.Users.Local.FirstOrDefault(usr => usr.Login.Equals(user?.Login));
            if (user == null) return;
            //Services.Navigator.EditUser = user; попытка отказаться от холдера
            var window = new Windows.UserInfoFields(); // Создаём новое окно
            
            var viewModel = new UserInfoFieldsViewModel() // С соответствующей ViewModel в DataContext 
            {
                CurrentUser = user, // В ViewModel ставим пользователя, чьи поля хотим отредактировать
                Context = ctx,
                Window = window
            };
            window.DataContext = viewModel;
            window.ShowDialog();
            //Context.SaveChanges();
            UpdateUserList(CurrentFilter);
            SelectedUser = null;
        }
        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
