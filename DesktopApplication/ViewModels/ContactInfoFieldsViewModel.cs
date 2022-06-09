using DesktopApplication.DbModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApplication.ViewModels
{
    public class ContactInfoFieldsViewModel : ViewModelBase
    {
        private Contact _currentContact;
        private manufacturingEntities _context;
        private Windows.ContactInfoFields _view;

        public Contact CurrentContact
        {
            get { return _currentContact; }
            set { _currentContact = value; }
        }
        public string Contact
        {
            get
            {
                return CurrentContact.Contact1;
            }
            set
            {
                CurrentContact.Contact1 = value;
                OnPropertyChanged(nameof(Contact));
            }
        }
        public string Description
        {
            get
            {
                return CurrentContact.Description;
            }
            set
            {
                CurrentContact.Description = value;
                OnPropertyChanged(nameof(Description));
            }

        }

        public ICommand SaveChangesCommand { get; set; }
        public ICommand RemoveContactCommand { get; set; }

        public ContactInfoFieldsViewModel()
        {
            SaveChangesCommand = new Commands.SaveChangesCommand(this);
            RemoveContactCommand = new RelayCommand(() => RemoveContact());
        }
        public ContactInfoFieldsViewModel(Contact currentContact, manufacturingEntities context, Windows.ContactInfoFields view) : this()
        {
            _currentContact = currentContact;
            _context = context;
            _view = view;
        }

        public override void SaveChanges()
        {
            _context.SaveChanges();
            _view.Close();
        }
        private void RemoveContact()
        {
            _context.Contacts.Remove(_currentContact);
            SaveChanges();
        }


    }
}
