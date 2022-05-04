using System;
using System.ComponentModel;
using System.Windows;
using DesktopApplication.Services;
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
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
