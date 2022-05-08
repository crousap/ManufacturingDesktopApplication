using DesktopApplication.DbModel;
using System.Windows;
using System.Windows.Controls;
using DesktopApplication.Pages;

namespace DesktopApplication.Services
{
    public static class Holder
    {
        public static UserInfoView User;
        public static Window Window;
        public static ShowUsersPage ShowUsersPage;
    }
    public enum EditMode
    {
        Add,
        Edit
    }
}
