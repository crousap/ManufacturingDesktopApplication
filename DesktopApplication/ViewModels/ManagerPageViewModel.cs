using DesktopApplication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopApplication.ViewModels
{
    public class ManagerPageViewModel : ViewModelBase
    {
        public ICommand Button { get; set; }

        public ManagerPageViewModel()
        {
            Button = new TheButtonClicledCommand(this); // test (можно удалить)
        }

        public void OnExecute()
        {
            MessageBox.Show("Я нажатая");
        }
    }
}
