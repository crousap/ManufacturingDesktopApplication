using DesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.Commands
{
    public class RemoveUserCommand : CommandBase
    {
        private UserInfoFieldsViewModel _viewModel;

        public RemoveUserCommand(UserInfoFieldsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.RemoveUser();
        }
    }
}
