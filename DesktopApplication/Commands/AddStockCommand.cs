using DesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.Commands
{
    public class AddStockCommand : CommandBase
    {
        private ViewModels.ShowStocksViewModel _viewModel;

        public AddStockCommand(ShowStocksViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.AddStock();
        }
    }
}
