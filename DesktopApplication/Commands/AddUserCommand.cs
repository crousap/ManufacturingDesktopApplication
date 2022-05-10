using DesktopApplication.ViewModels;
using DesktopApplication.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.Commands
{
    public class AddUserCommand : CommandBase
    {
        private ShowUsersViewModel _showUsersViewModel;

        public AddUserCommand(ShowUsersViewModel showUsersViewModel)
        {
            _showUsersViewModel = showUsersViewModel;
        }

        public override void Execute(object parameter)
        {
            
            //_showUsersViewModel.OnAddUserCommand();
        }

    }
}
