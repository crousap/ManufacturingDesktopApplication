using DesktopApplication.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopApplication.DbModel;
using System.Collections.ObjectModel;
using DesktopApplication.Windows;
using System.Data.Entity;
using DesktopApplication.Services;

namespace DesktopApplication.ViewModels
{
    public class ShowProjectsViewModel : ViewModelBase
    {
        #region PrivateFields
        private Pages.ShowProjectsPage _view;
        private ObservableCollection<Project> _projects;
        private Project _selectedProject;
        private string _searchQuery;
        private manufacturingEntities _context;
        private string _currentFilter;
        private List<string> _statuses;
        #endregion
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
        public string CurrentFilter
        {
            get
            {
                return _currentFilter;
            }
            set
            {
                _currentFilter = value;
                OnPropertyChanged(nameof(CurrentFilter));
            }
        }
        public ObservableCollection<Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }
        public Project SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                _selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
                EditProject(value);
            }
        }
        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }
        public ICommand AddNewProjectCommand { get; set; }

        public ShowProjectsViewModel()
        {
            AddNewProjectCommand = new RelayCommand(() => AddNewProject());
            _statuses = new List<string> { "Завершен", "В процессе", "Без фильтра" };
            UpdateProjectList();
        }
        public ShowProjectsViewModel(ShowProjectsPage view) : this()
        {
            _view = view;
        }
        private void UpdateProjectList()
        {
            _context = new manufacturingEntities();
            _context.Projects.Load();
            Projects = _context.Projects.ToObservableCollection();
        }
        private void EditProject(Project project)
        {
            if (project == null)
                return;
            var view = new ProjectInfoFields();
            var viewModel = new ProjectInfoFieldsViewModel(_context, project, view);
            view.DataContext = viewModel;
            view.ShowDialog();
            SelectedProject = null;
            UpdateProjectList();

        }
        public void AddNewProject()
        {
            var newProject = new Project();
            _context.Projects.Load();
            _context.Projects.Add(newProject);
            var view = new ProjectInfoFields();
            var viewModel = new ProjectInfoFieldsViewModel(_context, newProject, view);
            view.DataContext = viewModel;
            view.ShowDialog();
            UpdateProjectList();
        }
    }
}
