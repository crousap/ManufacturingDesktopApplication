using DesktopApplication.DbModel;
using System.Windows;
using System.Windows.Controls;
using DesktopApplication.Pages;

namespace DesktopApplication.Classes
{
    public static class Holder
    {
        public static UserInfoView User;
        public static Window Window;
        public static ShowUsersPage ShowUsersPage;
    }
    enum EditMode
    {
        Add,
        Edit
    }
}
