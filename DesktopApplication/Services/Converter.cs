using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApplication.DbModel;

namespace DesktopApplication.Services
{
    public static class Converter
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
