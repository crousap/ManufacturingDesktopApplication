using Application;
using DesktopApplication.Services;
using System;
using System.Windows;

namespace DesktopApplication.Windows
{
    /// <summary>
    /// Interaction logic for PagesHolder.xaml
    /// </summary>
    public partial class PagesHolder : Window
    {
        public PagesHolder()
        {
            /*
             * Определеяем, что будем делать в зависимости от того, под кем павторизируется пользователь
            */
            InitializeComponent();
            if (Authorizator.CurrentRole == Roles.Менеджер)
                frameMain.Navigate(new Pages.ManagerPage());
            else if (Authorizator.CurrentRole == Roles.Складовщик)
            {
                var view = new Pages.ShowWareHouses();
                var viewModel = new ViewModels.ShowWarehousesViewModel(view);
                view.DataContext = viewModel;
                frameMain.Navigate(view);
            }
            else
            {
                throw new NotImplementedException();
            }
            
            Holder.Window = this;
        }

        private void ButtonLogOff_Click(object sender, RoutedEventArgs e)
        {
            Authorizator.LogOff();
            new MainWindow().Show();
            Close();
        }
    }
}
