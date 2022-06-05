using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApplication.Services;
using DesktopApplication.DbModel;
using System.Windows.Input;
using DesktopApplication.Commands;
using DesktopApplication.Windows;
using System.Data.Entity;
using DesktopApplication.Models;
using System.Collections.ObjectModel;

namespace DesktopApplication.ViewModels
{
    public class UserInfoFieldsViewModel : ViewModelBase
    {
        #region fields
        private string _windowTitle;
        public string WindowTitle
        {
            get
            {
                return _windowTitle;
            }
            set
            {
                _windowTitle = value;
                OnPropertyChanged(nameof(WindowTitle));
            }
        }
        /// <summary>
        /// Пользователь, чьи данные подвергаются редактированию в данный момент
        /// </summary>
        public User CurrentUser;

        #region UserInfo
        #region Personal
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
            set { CurrentUser.UserInfo.MiddleName = value; OnPropertyChanged(nameof(MiddleName)); }
        }
        public DateTime BirthDate
        {
            get { return CurrentUser.UserInfo.BirthDate; }
            set { CurrentUser.UserInfo.BirthDate = value; OnPropertyChanged(nameof(BirthDate)); }
        }
        #endregion

        #region Contact
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

        #endregion
        #region LoginPassword
        public string Login
        {
            get => CurrentUser.Login;
            set { CurrentUser.Login = value; OnPropertyChanged(nameof(Login)); }
        }
        public string Password
        {
            get => CurrentUser.Password;
            set { CurrentUser.Password = value; OnPropertyChanged(nameof(Password)); }
        }
        #endregion

        /// <summary>
        /// Отображает роль (<see cref="Role"/>) пользователя (<see cref="User"/>)
        /// </summary>
        public Role SelectedRole
        {
            get
            {
                return CurrentUser.Role1;
            }
            set
            {
                CurrentUser.Role1 = value;
                OnPropertyChanged(nameof(Role));
            }
        }
        #endregion

        /// <summary>
        /// Для заполнение listbox для выбора роли
        /// </summary>
        public List<Role> Roles { get; set; }
        public ObservableCollection<WarehouseDependency> WarehouseDependencies { get; set; }

        public ICommand SaveChangesCommand { get; set; }
        /// <summary>
        /// Контекст, который передаётся из вызвавшего ViewModel
        /// </summary>
        public manufacturingEntities Context { get; set; }
        /// <summary>
        /// Окно, к которому относится это ViewModel
        /// </summary>
        public UserInfoFields Window { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор ViewModel, для редактирования пользователя
        /// </summary>
        /// <param name="currentUser">Пользователь, чьи поля нужно редактировать</param>
        /// <param name="context">Контекст, чтобы можно было сохранить результат в БД</param>
        public UserInfoFieldsViewModel(User currentUser, manufacturingEntities context) : this()
        {
            Context = context;
            CurrentUser = currentUser;
            Context.Roles.Load();
            Context.Warehouses.Load();

            Roles = Context.Roles.Local.ToList(); // Роли для комбобокса
            WarehouseDependencies = (from wrh in Context.Warehouses.Local
                                     select new WarehouseDependency(wrh, CurrentUser, Context)).ToObservableCollection();

            if (currentUser.UserInfo == null) // Когда у пользователя нет ничего кроме Логина и Пароля
                currentUser.UserInfo = new UserInfo();

            WindowTitle = $"Редактирование : {CurrentUser.Login}"; // Название окна
        }
        public UserInfoFieldsViewModel()
        {
            SaveChangesCommand = new SaveChangesCommand(this);
        }
        #endregion
        public override void SaveChanges()
        {
            Context.SaveChanges();
            Window.Close();
            Context.Dispose();
        }
    }
}
