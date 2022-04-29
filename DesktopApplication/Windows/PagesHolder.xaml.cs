using DesktopApplication.Classes;
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
            InitializeComponent();
            if (Authorizator.CurrentRole == Roles.Менеджер)
                frameMain.Navigate(new Pages.ManagerPage());

            Holder.Window = this;
        }
    }
}
