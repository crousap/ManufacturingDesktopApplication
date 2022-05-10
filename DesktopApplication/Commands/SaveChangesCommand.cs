using DesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopApplication.Commands
{
    public class SaveChangesCommand : CommandBase
    {
        private UserInfoFieldsViewModel userInfoFieldsViewModel;

        public SaveChangesCommand(UserInfoFieldsViewModel userInfoFieldsViewModel)
        {
            this.userInfoFieldsViewModel = userInfoFieldsViewModel;
        }

        public override void Execute(object parameter)
        {
            userInfoFieldsViewModel.SaveChanges();
        }
    }
}
