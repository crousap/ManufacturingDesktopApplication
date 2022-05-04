using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PagesHolderViewModel()
        {
            _windowTitle = Services.Authorizator.GetCaption();
        }
    }
}
