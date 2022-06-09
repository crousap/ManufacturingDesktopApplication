using DesktopApplication.DbModel;
using DesktopApplication.Services;
using DesktopApplication.Windows;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApplication.ViewModels
{
    public class ShowContactsViewModel : ViewModelBase
    {
        private manufacturingEntities _context;
        private ObservableCollection<Contact> _contacts;
        private Contact _selectedContact;
        private int? _previousConttactToEditId;

        public ICommand AddContactCommand { get; set; }
        public ObservableCollection<Contact> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }
        public Contact SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
                EditContact(value);
            }
        }
        public Client CurrentClient { get; set; }
        private string _searchQuery;
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

        public ShowContactsViewModel()
        {
            AddContactCommand = new RelayCommand(() => AddContact());
        }
        public ShowContactsViewModel(Client currentClient, manufacturingEntities context) : this()
        {
            CurrentClient = currentClient;
            _context = context;
            UpdateContactsList();
        }
        private void EditContact(Contact contactToEdit)
        {
            if (contactToEdit == null || _previousConttactToEditId == contactToEdit.Id)
                return;
            _previousConttactToEditId = contactToEdit.Id;
            var view = new ContactInfoFields();
            var viewModel = new ContactInfoFieldsViewModel(contactToEdit, _context, view);
            view.DataContext = viewModel;
            view.ShowDialog();
            SelectedContact = null;
            UpdateContactsList();
        }
        private void UpdateContactsList()
        {
            _context.Contacts.Load();
            Contacts = CurrentClient.Contacts.ToObservableCollection();
        }
        private void AddContact()
        {
            UpdateContactsList();
            var newContact = new Contact();
            newContact.ClientId = CurrentClient.Id;
            _context.Contacts.Add(newContact);
            var view = new ContactInfoFields();
            var viewModel = new ContactInfoFieldsViewModel(newContact, _context, view);
            view.DataContext = viewModel;
            view.ShowDialog();
            UpdateContactsList();
        }
    }
}
