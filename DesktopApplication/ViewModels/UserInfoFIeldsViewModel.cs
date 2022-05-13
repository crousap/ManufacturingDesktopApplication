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

namespace DesktopApplication.ViewModels
{
    public class UserInfoFieldsViewModel : ViewModelBase
    {
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
            set { CurrentUser.UserInfo.MiddleName = value; OnPropertyChanged(nameof(MiddleName)); }
        }
        public DateTime BirthDate
        {
            get { return CurrentUser.UserInfo.BirthDate; }
            set { CurrentUser.UserInfo.BirthDate = value; OnPropertyChanged(nameof(BirthDate)); }
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
        public ICommand SaveChangesCommand { get; set; }
        public manufacturingEntities Context { get; set; }
        public UserInfoFields Window { get; set; }
        /// <summary>
        /// Конструктор ViewModel, для редактирования пользователя
        /// </summary>
        /// <param name="currentUser">Пользователь, чьи поля нужно редактировать</param>
        /// <param name="context">Контекст, чтобы можно было сохранить результат в БД</param>
        public UserInfoFieldsViewModel(User currentUser, manufacturingEntities context) : this()
        {
            Context = context;
            CurrentUser = currentUser;

            if (currentUser.UserInfo == null) // Когда у пользователя нет ничего кроме Логина и Пароля
                currentUser.UserInfo = new UserInfo();

            WindowTitle = $"Редактирование : {CurrentUser.Login}"; // Название окна
        }
        public UserInfoFieldsViewModel()
        {
            SaveChangesCommand = new SaveChangesCommand(this);
        }
        public override void SaveChanges()
        {
            Context.SaveChanges();
            Window.Close();
            Context.Dispose();
        }
    }
}
