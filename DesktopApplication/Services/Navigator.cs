using System.Windows;
using System.Windows.Controls;

namespace DesktopApplication.Services
{
    public static class Navigator
    {
        public static Frame MainFrame { get; set; }
        public static Window MainWindow { get; set; }   
        public static DbModel.User EditUser { get; set; }
    }
}
