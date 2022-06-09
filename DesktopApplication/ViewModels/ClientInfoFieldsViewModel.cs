using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopApplication.DbModel;
using DesktopApplication.Pages;
using GalaSoft.MvvmLight.Command;

namespace DesktopApplication.ViewModels
{
    public class ClientInfoFieldsViewModel : ViewModelBase
    {
        #region PrivateFields
        private Client _currentClient;
        private manufacturingEntities _context;
        private Windows.ClientInfoFields _view;
        #endregion
        #region Properties
        public Client CurrentClient
        {
            get
            {
                return _currentClient;
            }
            set
            {
                _currentClient = value;
                OnPropertyChanged(nameof(CurrentClient));
            }
        }
        public manufacturingEntities Context
        {
            get { return _context; }
        }
        public int Id
        {
            get
            {
                return _currentClient.Id;
            }
            set
            {
                _currentClient.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get
            {
                return _currentClient.Name;
            }
            set
            {
                _currentClient.Name= value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get
            {
                return _currentClient.Description;
            }
            set
            {
                _currentClient.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand RemoveClientCommand { get; set; }
        #endregion
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="currentClient">Данные клиента, которого мы сейчас редактируем</param>
        /// <param name="context">Контекст, с которым мы работаем</param>
        public ClientInfoFieldsViewModel(Client clientToEdit, manufacturingEntities context, Windows.ClientInfoFields view)
        {
            SaveChangesCommand = new Commands.SaveChangesCommand(this); // Кнопки
            RemoveClientCommand = new RelayCommand(() => RemoveClient());

            _currentClient = clientToEdit;
            _context = context;
            _view = view;

            var tempView = new ShowContactsPage();
            var tempViewModel = new ShowContactsViewModel(_currentClient, _context);
            tempView.DataContext = tempViewModel;
            _view.ContactsFrame.Navigate(tempView);
        }
        public override void SaveChanges()
        {
            _context.SaveChanges();
            _view.Close();
        }
        public void RemoveClient()
        {
            _context.Clients.Remove(_currentClient);
            SaveChanges();
        }
    }
}
