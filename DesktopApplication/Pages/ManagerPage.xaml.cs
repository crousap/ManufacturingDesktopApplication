using DesktopApplication.ViewModels;
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
            this.Title = Services.Authorizator.CurrentUser.Role;
        }

        private void buttonShowWarehouses_Click(object sender, RoutedEventArgs e)
        {
            var view = new ShowWareHouses();
            var viewModel = new ShowWarehousesViewModel(view);
            view.DataContext = viewModel;
            frameManagerPage.Navigate(view);
        }

        private void buttonShowClients_Click(object sender, RoutedEventArgs e)
        {
            var view = new ShowClientsPage();
            var viewModel = new ShowClientsViewModel(view);
            view.DataContext = viewModel;
            frameManagerPage.Navigate(view);

        }
    }
}
