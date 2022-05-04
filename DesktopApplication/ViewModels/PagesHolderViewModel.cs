using DesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesktopApplication.ViewModels
{
    public class PagesHolderViewModel : ViewModelBase
    {
        private string _windowTitle;
        public string WindowTitle
        {
            get
            {
                return _windowTitle;
            }
            set
            {
                _windowTitle = value;
                OnPropertyChanged(nameof(WindowTitle));
            }
        }

        //private Page _currentPage;
        //public Page CurrentPage
        //{
        //    get
        //    {
        //        return _currentPage;
        //    }
        //    set
        //    {
        //        if (Equals(_currentPage, value))
        //            return;
        //        _currentPage = value;
        //        OnPropertyChanged(nameof(CurrentPage));
        //    }
        //}

        public PagesHolderViewModel()
        {
            _windowTitle = Services.Authorizator.GetCaption();
            //PageLoad();
        }
        public void PageLoad()
        {
            //switch (Authorizator.CurrentRole)
            //{
            //    case Roles.Инженер:
            //        break;
            //    case Roles.Менеджер:
            //        CurrentPage.Navigate(new Pages.ManagerPage());
            //        break;
            //    case Roles.Сварщик:
            //        break;
            //    case Roles.Складовщик:
            //        break;
            //    case Roles.Станочник:
            //        break;
            //    case Roles.Универсальный_рабочий:
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
