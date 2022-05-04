using DesktopApplication.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DesktopApplication.Services;
using System.Data.Entity;

namespace DesktopApplication.Pages
{
    /// <summary>
    /// Interaction logic for ShowUsersPage.xaml
    /// </summary>
    public partial class ShowUsersPage : Page
    {
        

        public ShowUsersPage()
        {
            InitializeComponent();
        }

        private void textBoxSearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            //var query = textBoxSearchBar.Text.Trim();
            //listViewUsers.ItemsSource = SearchBy(query);      
        }

    }
}
