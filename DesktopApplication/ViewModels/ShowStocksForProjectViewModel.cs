using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopApplication.DbModel;
using GalaSoft.MvvmLight.Command;
using DesktopApplication.Pages;
using System.Data.Entity;
using DesktopApplication.Services;

namespace DesktopApplication.ViewModels
{
    public class ShowStocksForProjectViewModel : ViewModelBase
    {
        private manufacturingEntities _context;
        private ObservableCollection<Stock> _stocks;
        private ShowStocksForProjectPage _view;
        private Stock _selectedStock;
        private Project _currentProject;

        public ObservableCollection<Stock> Stocks
        {
            get
            {
                return _stocks;
            }
            set
            {
                _stocks = value;
                OnPropertyChanged(nameof(Stocks));
            }
        }
        public Stock SelectedStock
        {
            get
            {
                return _selectedStock;
            }
            set
            {
                _selectedStock = value;
                OnPropertyChanged(nameof(SelectedStock));
            }
        }
        public ICommand ChangeStocksCommand { get; set; }
        public ShowStocksForProjectViewModel()
        {
            ChangeStocksCommand = new RelayCommand(() => ChangeStock());
        }
        public ShowStocksForProjectViewModel(Project currentProject, ShowStocksForProjectPage view, manufacturingEntities context) : this()
        {
            _context = context;
            _currentProject = currentProject;
            _view = view;
            UpdateStockList();
            var projectInContext = _context.Projects.FirstOrDefault((x) => x.Id == currentProject.Id);
            if (projectInContext == null)
                return;
            _currentProject = projectInContext;
        }
        private void UpdateStockList()
        {
            //_context = new manufacturingEntities();
            _context.Stocks.Load();
            _context.Projects.Load();
            Stocks = _currentProject.Stocks.ToObservableCollection();
        }
        public void ChangeStock()
        {
            var view = new Windows.ChooseStocksForProject();
            var viewModel = new ViewModels.ChooseStocksForProjectViewModel(_currentProject, view, _context);
            view.DataContext = viewModel;
            view.ShowDialog();
            UpdateStockList();
        }
    }
}
