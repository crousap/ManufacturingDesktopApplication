using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopApplication.DbModel;
using DesktopApplication.Classes;

namespace Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = textBoxLogin.Text;
            var password = passwordBox.Password;

            var isLogged = Authorizator.LoginCheck(login, password);

            if (isLogged)
            {
                var window = new DesktopApplication.Windows.PagesHolder();
                window.Show();
                this.Close();
            }
            else
                MessageBox.Show("Не правильный логин или пароль");
        }
    }
}
