using DesktopApplication.Commands;
using DesktopApplication.DbModel;
using DesktopApplication.Services;
using DesktopApplication.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopApplication.ViewModels
{
    public class LoginPasswordFieldsViewModel : ViewModelBase
    {
        public string Login
        {
            get
            {
                return User.Login;
            }
            set
            {
                User.Login = value;
                ProceedCommand.CanExecute(this);
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get
            {
                return User.Password;
            }
            set
            {
                User.Password = value;
                ProceedCommand.CanExecute(this);
                OnPropertyChanged(nameof(Password));
            }
        }
        public string SelectedRole
        {
            get
            {
                return User.Role;
            }
            set
            {
                User.Role = value;
                OnPropertyChanged(nameof(SelectedRole));
            }
        }

        private List<string> _roles;
        public List<string> Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }

        public ICommand ProceedCommand { get; set; }
        public User User { get; set; }
        public LoginPasswordFieldsView Window { get; set; }

        public LoginPasswordFieldsViewModel()
        {
            using (var ctx = new manufacturingEntities())
            {
                _roles = (from role in ctx.Roles.ToList()
                         select role.Name).ToList();
            }
            ProceedCommand = new ProceedCommand(this);
        }
        public void Procced()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || SelectedRole == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            if (!LoginAvaliable())
            {
                MessageBox.Show("Пользователь с таким логином уже сущетсвует");
                return;
            }
            Holder.Result = true;
            Window.Close();
        }
        private bool LoginAvaliable()
        {
            User user = null;
            try
            {
                using (var ctx = new manufacturingEntities())
                    user = ctx.Users.ToList().First(usr => usr.Login == Login);
                return user == null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return true;
            }
        }
    }
}
