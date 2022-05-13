using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DesktopApplication.DbModel;
using DesktopApplication.Pages;
using DesktopApplication.Services;

namespace DesktopApplication.ViewModels
{
    public class ShowWarehousesViewModel : ViewModelBase
    {
        private ObservableCollection<Warehouse> _warehouses;
        public ObservableCollection<Warehouse> Warehouses
        {
            get
            {
                return _warehouses;
            }
            set
            {
                _warehouses = value;
                OnPropertyChanged(nameof(Warehouses));
            }
        }

        private Warehouse _selectedWarehouse;
        public manufacturingEntities Context { get; set; }

        public Warehouse SelectedWarehouse
        {
            get
            {
                return _selectedWarehouse;
            }
            set
            {
                _selectedWarehouse = value;
                OnPropertyChanged(nameof(SelectedWarehouse));
            }
        }
        public ShowWareHouses View { get; set; }

        public ShowWarehousesViewModel()
        {
            PropertyChanged += SelectedWarehouseChanged;
            Context = new manufacturingEntities();
            if (Authorizator.CurrentRole == Roles.Менеджер) // Если пользователь является менеджером, то ему будет доступна информация о всех складах
            {
                Warehouses = Context.Warehouses.ToObservableCollection();
                return;
            } // В противном случае пользователь будет складовщиком
            Context.Warehouses.Load();
            // Отображать только те склады, к которым у пользователя есть доступ
            Warehouses = Context.Warehouses.Local.Where(wrh => wrh.Users.Contains(Authorizator.CurrentUser)).ToObservableCollection();
        }
        public ShowWarehousesViewModel(ShowWareHouses page) : this()
        {
            View = page;
        }
        /// <summary>
        /// Случается, если пользователь выбирает 1 из складов из списка
        /// </summary>
        private void SelectedWarehouseChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!(e.PropertyName == nameof(SelectedWarehouse)))
                return;

            var view = new ShowStocks();
            var viewModel = new ShowStocksViewModel(view, SelectedWarehouse, this);
            view.DataContext = viewModel;
            View.frameShowStocks.Navigate(view);


        }


    }
}
