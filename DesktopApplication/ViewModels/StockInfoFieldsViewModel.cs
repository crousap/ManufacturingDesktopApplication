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
        public int Id
        {
            get
            {
                return _currentStock.Id;
            }
            set
            {
                _currentStock.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get
            {
                return _currentStock.Name;
            }
            set
            {
                _currentStock.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }   
        public int Ammount
        {
            get { return _currentStock.Ammount; }
            set { _currentStock.Ammount = value; OnPropertyChanged(nameof(Ammount)); }
        }

        private Stock _currentStock { get; set; }
        private ICommand SaveChangesCommand { get; set; }
        private manufacturingEntities _context { get; set; }
        private Window _window { get; set; }

        public StockInfoFieldsViewModel()
        {
            SaveChangesCommand = new SaveChangesCommand(this);
        }

        public StockInfoFieldsViewModel(Stock stock, manufacturingEntities context, Window window) : this()
        {
            _currentStock = stock;
            _context = context;
            _window = window;
        }

        public override void SaveChanges()
        {
            _context.SaveChanges();
            _window.Close();
            _context.Dispose();
        }
    }
}
