using DesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.Commands
{
    public class TheButtonClicledCommand : CommandBase
    {

        private ManagerPageViewModel _managerPageViewModel;

        public TheButtonClicledCommand(ManagerPageViewModel managerPageViewModel)
        {
            _managerPageViewModel = managerPageViewModel;
        }

        public override void Execute(object parameter)
        {
            _managerPageViewModel.OnExecute();
        }
    }
}
