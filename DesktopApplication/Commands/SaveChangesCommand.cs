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
        private ViewModelBase _viewModel;

        public SaveChangesCommand(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.SaveChanges();
        }
    }
}
