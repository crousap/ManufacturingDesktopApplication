using DesktopApplication.DbModel;
using DesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ViewModels
{
    public class ChooseStocksForProjectViewModel : ViewModelBase
    {
        private ObservableCollection<Stock> _stocks;
        private manufacturingEntities _context;
        private Stock _selectedStock;
        private string _searchQuery;
        private Project _currentProject;
        private Windows.ChooseStocksForProject _view;

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
                SelecteStock(value);
            }
        }
        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                UpdateStocks(value);
            }
        }

        public ChooseStocksForProjectViewModel(Project currentProject, Windows.ChooseStocksForProject view, manufacturingEntities context)
        {
            _context = context;
            _currentProject = currentProject;
            _view = view;
            UpdateStocks();
        }

        private void UpdateStocks()
        {
            _context.Stocks.Load();
            _context.Projects.Load();
            Stocks = _context.Stocks.ToObservableCollection(); // Вывести товары, которые уже не перечислены в проекте
        }
        private void UpdateStocks(string query)
        {
            if (string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
                UpdateStocks();
            int tryQueryToInt;
            bool tryQueryToIntResult = int.TryParse(query, out tryQueryToInt);

            var results = from stock in _context.Stocks
                          where (
                          // Если удаолсь конвертировать в инт, а так же id совпадает, то возвращает true
                          (tryQueryToIntResult && stock.Id == tryQueryToInt)
                          || (stock.Name.ToLower().StartsWith(query.ToLower()))
                          )
                          select stock;
            Stocks = results.ToObservableCollection();
        }
        private void SelecteStock(Stock stock)
        {
            _context.Projects.Load();
            //_context.Projects.FirstOrDefault((x) => x.Id == _currentProject.Id).Stocks.Add(stock);
            _currentProject.Stocks.Add(stock);
            _context.SaveChanges();
            _view.Close();
        }

    }
}
