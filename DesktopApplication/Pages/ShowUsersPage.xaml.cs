using DesktopApplication.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DesktopApplication.Classes;
using System.Data.Entity;

namespace DesktopApplication.Pages
{
    /// <summary>
    /// Interaction logic for ShowUsersPage.xaml
    /// </summary>
    public partial class ShowUsersPage : Page
    {
        public const string NoFilter = "Без фильтра";
        public manufacturingEntities Context;

        public ShowUsersPage()
        {
            InitializeComponent();
            var query = (from role in manufacturingEntities.GetContext().Roles select role.Name).ToList();
            query.Add(NoFilter);
            comboBoxFilter.ItemsSource = query;

            Context = new manufacturingEntities();
            Holder.ShowUsersPage = this;
        }

        private void listViewUsers_Loaded(object sender, RoutedEventArgs e)
        {
            updateList();
        }

        private void comboBoxFilter_DropDownClosed(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBoxFilter.Text);
            if (comboBoxFilter.Text == NoFilter)
                updateList();
            else
                listViewUsers.ItemsSource = (from user in manufacturingEntities.GetContext().UserInfoViews
                                             where user.Role == comboBoxFilter.Text.Trim()
                                             select user).ToList();
        }

        private void listViewUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Holder.User = (UserInfoView)listViewUsers.SelectedItem;
            var userInfoFields = new Windows.UserInfoFields();
            userInfoFields.ShowDialog();
            //updateList(); 
        }

        private void updateList()
        {
            listViewUsers.SelectedItem = null;
            Context.UserInfoViews.Load();
            listViewUsers.ItemsSource = Context.UserInfoViews.Local;
        }

        private void textBoxSearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            var query = textBoxSearchBar.Text.Trim();
            listViewUsers.ItemsSource = SearchBy(query);
        }

        private List<UserInfoView> SearchBy(string query)
        {
            query = query.ToLower();
            return (from user in manufacturingEntities.GetContext().UserInfoViews // Retrie
                    where (
                        user.FirstName.ToLower().StartsWith(query)
                        || user.LastName.ToLower().StartsWith(query)
                        || user.MiddleName.ToLower().StartsWith(query)
                        || user.UserLogin.ToLower().StartsWith(query))
                    select user).ToList();

        }

    }
}
