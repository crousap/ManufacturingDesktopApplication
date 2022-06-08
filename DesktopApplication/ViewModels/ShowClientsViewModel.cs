using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApplication.DbModel;
using DesktopApplication.Services;
using DesktopApplication.Pages;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using DesktopApplication.Windows;

namespace DesktopApplication.ViewModels
{
    public class ShowClientsViewModel : ViewModelBase
    {

        #region Fields
        #region PrivateFields
        private ObservableCollection<Client> _clients;
        private Client _selectedClient;
        private ShowClientsPage _view;
        private manufacturingEntities _context;
        #endregion
        #region Properties
        public manufacturingEntities Context
        {
            get { return _context; }
            set { _context = value; }
        }
        public ObservableCollection<Client> Clients
        {
            get
            {
                return _clients;
            }
            set
            {
                _clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }
        public Client SelectedClient
        {
            get
            {
                return _selectedClient;
            }
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
                EditClientFields(value);
            }
        }
        #endregion
        public ICommand AddClientCommand { get; set; }
        public ICommand RemoveClientCommand { get; set; }
        #endregion
        #region Constructors
        public ShowClientsViewModel()
        {
            AddClientCommand = new RelayCommand(() => AddClient("AddClientButton"));
            RemoveClientCommand = new RelayCommand(() => RemoveClient("RemoveClientButton"));

            _context = new manufacturingEntities();
            UpdateClientsList();
        }
        public ShowClientsViewModel(ShowClientsPage view) : this()
        {
            _view = view;
        }
        #endregion
        public void AddClient(object sender)
        {
            UpdateClientsList();
            var newClient = new Client();
            var view = new ClientInfoFields();
            var viewModel = new ClientInfoFieldsViewModel(newClient, _context, view);
            view.DataContext = viewModel;
            view.ShowDialog();

        }
        public void RemoveClient(object sender)
        {

        }
        private void UpdateClientsList()
        {
            _context = new manufacturingEntities();
            _context.Clients.Load();
            Clients = _context.Clients.ToObservableCollection();
        }
        private void EditClientFields(Client client)
        {
            var view = new Windows.ClientInfoFields();
            var viewModel = new ClientInfoFieldsViewModel(client, _context, view);
            view.DataContext = viewModel;
            view.ShowDialog();
        }
    }
}
