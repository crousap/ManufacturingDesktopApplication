using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApplication.DbModel;
using DesktopApplication.ViewModels;

namespace DesktopApplication.Models
{
    public class WarehouseDependency : ViewModelBase
    {
        #region Fields
        private Warehouse _warehouse;
        public Warehouse Warehouse
        {
            get
            {
                return _warehouse;
            }
            set
            {
                _warehouse = value;
                OnPropertyChanged(nameof(Warehouse));
            }
        }
        public User User;
        public manufacturingEntities Context;
        #endregion


        public bool IsDepended
        {
            get
            {
                return User.Warehouses.Contains(Warehouse);
            }
            set
            {
                if (value)
                {
                    User.Warehouses.Add(Warehouse);
                }
                else
                {
                    User.Warehouses.Remove(Warehouse);
                }
                Context.SaveChanges();
                OnPropertyChanged(nameof(IsDepended));
            }
        }

        public WarehouseDependency(Warehouse warehouse, User user, manufacturingEntities context)
        {
            Warehouse = warehouse;
            User = user;
            Context = context;
        }
    }
}
