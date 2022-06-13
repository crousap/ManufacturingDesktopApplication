using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopApplication.DbModel;
using DesktopApplication.Windows;
using GalaSoft.MvvmLight.Command;

namespace DesktopApplication.ViewModels
{
    public class ProjectInfoFieldsViewModel : ViewModelBase
    {
        #region PrivateFields
        private Project _currentProject;
        private ProjectInfoFields _view;
        private manufacturingEntities _context;
        #endregion

        public string Name
        {
            get
            {
                return _currentProject.Name;
            }
            set
            {
                _currentProject.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get
            {
                return _currentProject.Description;
            }
            set
            {
                _currentProject.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public string Status
        {
            get
            {
                return _currentProject.Status;
            }
            set
            {
                _currentProject.Status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private List<string> _statuses;
        public List<string> Statuses
        {
            get
            {
                return _statuses;
            }
            set
            {
                _statuses = value;
                OnPropertyChanged(nameof(Statuses));
            }
        }

        public ICommand SaveChangesCommand { get; set; }
        public ICommand RemoveProjectCommand { get; set; }

        public ProjectInfoFieldsViewModel()
        {
            _statuses = new List<string> { "Завершен", "В процессе" };
            SaveChangesCommand = new Commands.SaveChangesCommand(this);
            RemoveProjectCommand = new RelayCommand(() => RemoveProject());
        }

        public ProjectInfoFieldsViewModel(manufacturingEntities context, Project currentProject, ProjectInfoFields view) : this()
        {
            _currentProject = currentProject;
            _view = view;
            _context = context;
            if (Status == null)
                Status = "В процессе";
            var tempView = new Pages.ShowStocksForProjectPage();
            var tempViewModel = new ShowStocksForProjectViewModel(currentProject, tempView, _context);
            tempView.DataContext = tempViewModel;
            _view.StocksFrame.Navigate(tempView);
        }

        private void RemoveProject()
        {
            _context.Projects.Remove(_currentProject);
            SaveChanges();
        }

        public override void SaveChanges()
        {
            _context.SaveChanges();
            _view.Close();
        }
    }
}
