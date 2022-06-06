﻿using DesktopApplication.DbModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DesktopApplication.Services;
using DesktopApplication.Windows;
using System.Collections.Specialized;
using System.Windows.Input;
using DesktopApplication.Commands;
using System.Windows;

namespace DesktopApplication.ViewModels
{
    public class ShowStocksViewModel : ViewModelBase
    {
        private ObservableCollection<Stock> _stocks;

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

        private Stock _selectedStock;
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
                Context.SaveChanges();
            }
        }

        private string _searchQuery;
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

        public manufacturingEntities Context { get; set; }
        /// <summary>
        /// Та View, к которой принадлежит данная ViewModel
        /// </summary>
        public Page View { get; private set; }
        /// <summary>
        /// Склад, предметы которого мы в данный момент просматриваем
        /// </summary>
        public Warehouse CurrentWarehouse { get; private set; }

        public ICommand AddStockCommand { get; set; }
        public ICommand RemoveStockCommand { get; set; }

        #region Конструкторы
        public ShowStocksViewModel()
        {
            AddStockCommand = new AddStockCommand(this);
            RemoveStockCommand = new RemoveStockCommand(this);
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="viewModel">Экземпляр вызвавшего ViewModel</param>
        /// <param name="selectedWarehouse">Экземпляр склада, предметы которого нужно показать</param>
        /// <param name="view">View, к которой пр ивязана текущая ViewModel</param>
        public ShowStocksViewModel(Page view, ShowWarehousesViewModel viewModel) : this()
        {
            View = view;
            CurrentWarehouse = viewModel.SelectedWarehouse;
            Context = viewModel.Context;
            UpdateStocks();
        }

        #endregion
        private void UpdateStocks()
        {
            Context.Stocks.Load();
            Stocks = CurrentWarehouse.Stocks.ToObservableCollection();
        }
        private void UpdateStocks(string query)
        {
            if (string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
                UpdateStocks();
            int tryQueryToInt;
            bool tryQueryToIntResult = int.TryParse(query, out tryQueryToInt);

            var results = from stock in CurrentWarehouse.Stocks
                          where (
                          // Если удаолсь конвертировать в инт, а так же id совпадает, то возвращает true
                          (tryQueryToIntResult && stock.Id == tryQueryToInt)
                          || (stock.Name.ToLower().StartsWith(query.ToLower()))
                          || (stock.Description?.ToLower().StartsWith(query.ToLower()) ?? false)
                                )
                          select stock;
            Stocks = results.ToObservableCollection();
        }

        public void AddStock()
        {
            var window = new StockInfoFields();
            var viewModel = new ViewModels.StockInfoFieldsViewModel(window, CurrentWarehouse.Id);
            window.DataContext = viewModel;
            window.ShowDialog();
            UpdateStocks();
            SearchQuery = String.Empty;
        }

        public void RemoveStock()
        {
            Context.Stocks.Remove(SelectedStock);
            Context.SaveChanges();
            UpdateStocks();
        }
    }
}
