using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Нужно для реализации кнопки Сохранения записей.
        /// <para>
        ///     В таких ViewModel как <see cref="StockInfoFieldsViewModel"/> и <see cref="UserInfoFieldsViewModel"/>
        /// </para>
        /// </summary>
        public virtual void SaveChanges() { }

    }
}
