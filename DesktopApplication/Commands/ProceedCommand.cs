using DesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.Commands
{
    public class ProceedCommand : CommandBase
    {
        private LoginPasswordFieldsViewModel ViewModel;

        public ProceedCommand(LoginPasswordFieldsViewModel loginPasswordFieldsViewModel)
        {
            this.ViewModel = loginPasswordFieldsViewModel;
        }

        public override void Execute(object parameter)
        {
            ViewModel.Procced();
        }
    }
}
