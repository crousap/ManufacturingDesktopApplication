using System;
using System.ComponentModel;
using System.Windows;
using DesktopApplication.Classes;
using DesktopApplication.DbModel;



namespace DesktopApplication.Windows
{
    /// <summary>
    /// Interaction logic for UserInfoFields.xaml
    /// </summary>
    public partial class UserInfoFields : Window
    {

        public UserInfoFields()
        {
            InitializeComponent();
            infoPanel.DataContext = Holder.User;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Holder.ShowUsersPage.updateList();
            Close();
        }

        private void buttonWrite_Click(object sender, RoutedEventArgs e)
        {
            buttonClose_Click(sender, e);
        }
    }
}
