using DesktopApplication.Classes;
using System.Windows;

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
            var password = textBoxPassword.Text;

            var isLogged = Authorizator.LoginCheck(login, password);
            

            if (isLogged)
            {
                InitiatePagesHolder();
            }
            else
                MessageBox.Show("Не правильный логин или пароль");
        }   

        private void InitiatePagesHolder()
        {
            var window = new DesktopApplication.Windows.PagesHolder();
            window.Show();
            this.Close();
        }
    }
    //public class NotEmptyValidationRule : ValidationRule
    //{
    //    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    //    {
    //        return string.IsNullOrWhiteSpace((value ?? "").ToString())
    //            ? new ValidationResult(false, "Обязательное поле.")
    //            : ValidationResult.ValidResult;
    //    }
    //}
}
