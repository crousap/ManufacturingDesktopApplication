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

            Context = new manufacturingEntities();
            Holder.ShowUsersPage = this;
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
