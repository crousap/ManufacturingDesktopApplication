using DesktopApplication.Commands;
using DesktopApplication.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopApplication.ViewModels
{
    public class StockInfoFieldsViewModel : ViewModelBase
    {
        public string Name
        {
            get
            {
                return _newStock.Name;
            }
            set
            {
                _newStock.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }   
        public int Ammount
        {
            get { return _newStock.Ammount; }
            set { _newStock.Ammount = value; OnPropertyChanged(nameof(Ammount)); }
        }
        public string Position
        {
            get { return _newStock.Position; }
            set { _newStock.Position = value; OnPropertyChanged(nameof(Position)); }
        }        
        public string Description
        {
            get { return _newStock.Description; }
            set { _newStock.Description = value; OnPropertyChanged(nameof(Description)); }
        }

        private Stock _newStock { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        private manufacturingEntities _context { get; set; }
        private Window _window { get; set; }
        private int _currentWarehouseId { get; set; }

        public StockInfoFieldsViewModel()
        {
            SaveChangesCommand = new SaveChangesCommand(this);
            _newStock = new Stock();
            _context = new manufacturingEntities();
        }

        public StockInfoFieldsViewModel(Window window, int currentWarehouseId) : this()
        {
            _window = window;
            _currentWarehouseId = currentWarehouseId;
        }

        public override void SaveChanges()
        {
            _newStock.WarehouseId = _currentWarehouseId;
            _context.Stocks.Add(_newStock);
            _context.SaveChanges();
            _window.Close();
            _context.Dispose();
        }
    }
}
