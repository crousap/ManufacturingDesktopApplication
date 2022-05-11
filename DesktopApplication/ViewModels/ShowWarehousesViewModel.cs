using System;
using System.Collections.Generic;
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
        private Stock _stocks;
        public Stock Stocks
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
            }
        }

        private List<Warehouse> _warehouses;
        public List<Warehouse> Warehouses
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
            using (var ctx = new manufacturingEntities())
            {
                if (Authorizator.CurrentRole == Roles.Менеджер) // Если пользователь является менеджером, то ему будет доступна информация о всех складах
                {
                    Warehouses = ctx.Warehouses.ToList();
                    return;
                } // В противном случае пользователь будет складовщиком
                ctx.Warehouses.Load();
                // Отображать только те склады, к которым у пользователя есть доступ
                Warehouses = ctx.Warehouses.Local.Where(wrh => wrh.Users.Contains(Authorizator.CurrentUser)).ToList();
            }
        }

        private void SelectedWarehouseChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!(e.PropertyName == nameof(SelectedWarehouse)))
                return;

            
        }

        public ShowWarehousesViewModel(ShowWareHouses page) : this()
        {
            View = page;
        }
    }
}
