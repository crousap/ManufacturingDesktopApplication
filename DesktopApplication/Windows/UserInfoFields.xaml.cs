using System;
using System.ComponentModel;
using System.Windows;
using DesktopApplication.Classes;


namespace DesktopApplication.Windows
{
    /// <summary>
    /// Interaction logic for UserInfoFields.xaml
    /// </summary>
    public partial class UserInfoFields : Window
    {
        string currentName;
        string currentLastName;
        string currentSecondName;
        DateTime currentBirthdate;
        string currentLogin;
        string currentPassword;
        string currentPhoneNumber;
        string currentEmail;
        string currentAddress;

        public UserInfoFields()
        {
            InitializeComponent();
        }

        private void FieldsUpdate()
        {

            currentName = textBoxFirstName.Text;
            currentLastName = textBoxLastName.Text;
            currentSecondName = textBoxSecondName.Text;
            //currentBirthdate = datePickerBirthdate.SelectedDate;
            currentLogin = textBoxLogin.Text;
            currentPassword = textBoxPassword.Text;
            currentPhoneNumber = textBoxPhoneNumber.Text;
            currentEmail = textBoxEmail.Text;
            currentAddress = textBoxAddress.Text;

        }

        private void FillFromExisting()
        {

        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonWrite_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
