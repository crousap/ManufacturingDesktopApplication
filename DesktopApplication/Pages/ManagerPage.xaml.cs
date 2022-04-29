using System.Windows;
using System.Windows.Controls;

namespace DesktopApplication.Pages
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        public ManagerPage()
        {
            InitializeComponent();
        }

        private void buttonShowUsers_Click(object sender, RoutedEventArgs e)
        {
            frameManagerPage.Navigate(new ShowUsersPage());
            this.Title = Classes.Authorizator.CurrentUser.Role;
        }
    }
}
