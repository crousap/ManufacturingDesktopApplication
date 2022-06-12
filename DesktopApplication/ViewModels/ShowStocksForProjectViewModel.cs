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
        public ShowStocksForProjectViewModel(Project currentStock, ShowStocksForProjectPage view) : this()
        {
            _currentProject = currentStock;
            _view = view;
            UpdateStockList();
            _currentProject = _context.Projects.FirstOrDefault((x) => x.Id == currentStock.Id);
        }
        private void UpdateStockList()
        {
            _context = new manufacturingEntities();
            _context.Stocks.Load();
            _context.Projects.Load();
            Stocks = _currentProject.Stocks.ToObservableCollection();
        }
        public void ChangeStock()
        {
            
        }
    }
}
