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
        private ShowUsersViewModel showUsersViewModel;

        public AddUserCommand(ShowUsersViewModel showUsersViewModel)
        {
            this.showUsersViewModel = showUsersViewModel;
        }

        public override void Execute(object parameter)
        {
            showUsersViewModel.AddNewUser();
        }

    }
}
