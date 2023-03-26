using CSharpPractice2.Exceptions;
using CSharpPractice2.Helpers;
using CSharpPractice2.Models;
using CSharpPractice2.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CSharpPractice2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region Properties

        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private DateTime? _birthDate;

        private Person? _person;

        public string FirstName
        {
            get => _firstName ?? "";
            set
            {
                _firstName = value;
                ProceedCommand.InvokeCanExecuteChanged();
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => _lastName ?? "";
            set
            {
                _lastName = value;
                ProceedCommand.InvokeCanExecuteChanged();
                OnPropertyChanged("LastName");
            }
        }

        public string Email
        {
            get => _email ?? "";
            set
            {
                _email = value;
                ProceedCommand.InvokeCanExecuteChanged();
                OnPropertyChanged("Email");
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate ?? DateTime.Now;
            set
            {
                _birthDate = value;
                ProceedCommand.InvokeCanExecuteChanged();
                OnPropertyChanged("BirthDate");
            }
        }

        public Person? Person
        {
            get => _person;
            set 
            {
                _person = value;
                OnPropertyChanged("Person");
                OnPropertyChanged("DisplayPersonInfo");
            }
        }

        public bool DisplayPersonInfo
        {
            get => Person != null;
        }

        #endregion

        #region Commands

        public RelayCommand ProceedCommand { get; private set; }

        private async void Proceed(object input)
        {
            await Task.Run(() =>
            {
                try
                {
                    BirthDateValidator.Instance.ValidateOrThrow(BirthDate);
                }
                catch (BirthDateValidationException e)
                {
                    MessageBox.Show(e.Message, "Invalid birth date", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    EmailValidator.Instance.ValidateOrThrow(Email);
                }
                catch (EmailValidationException e)
                {
                    MessageBox.Show(e.Message, "Invalid email", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Person = new Person(FirstName, LastName, Email, BirthDate);
            });
        }

        private bool CanProceed(object input)
        {
            return
                _firstName?.Length > 0 &&
                _lastName?.Length > 0 &&
                _email?.Length > 0 &&
                _birthDate != null;
        }

        #endregion

        public MainViewModel()
        {
            _firstName = null;
            _lastName = null;
            _email = null;
            _birthDate = null;

            ProceedCommand = new RelayCommand(Proceed, CanProceed);
        }
    }
}
