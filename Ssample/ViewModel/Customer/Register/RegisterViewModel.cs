using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Ssample.ViewModel
{
    /// <summary>
    /// Register view model
    /// </summary>
    public class RegisterViewModel : NavigationViewModelBase
    {
        #region Fields for view models
        //Field for successful register view model
        private NavigationViewModelBase successfulRegisterViewModel;

        //Field for success view
        private NavigationViewModelBase _goToSuccessRegisterView;
        #endregion

        #region Navigation Commands
        //Goes back command
        public ICommand GoBackCommand { get; set; }

        //Go to the successful screen
        public ICommand GoToSuccessfulViewCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for class
        /// </summary>
        public RegisterViewModel(NavigationViewModelBase goToSuccessRegisterView)
        {
            _goToSuccessRegisterView = goToSuccessRegisterView;

            successfulRegisterViewModel = new SuccessfulRegisterViewModel(_goToSuccessRegisterView);

            //Commands to go to successful page
            GoToSuccessfulViewCommand = new RelayCommand(GoToSuccessful);

            //Command to go back
            GoBackCommand = new RelayCommand(GoBack);
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// Function for going back
        /// </summary>
        private void GoBack()
        {
            NavigateBack();
        }

        /// <summary>
        /// Function that navigates to successful view model
        /// </summary>
        private void GoToSuccessful()
        {
            if (SaveCustomerChanges())
            {
                Navigate(successfulRegisterViewModel);
            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }

        #endregion

        #region Fields for entering values for database

        public List<Customer_Details> Customers { get; set; }
        public Customer_Details CurrentCustomer { get; set; } = new Customer_Details(); //Current customer you need to edit

        #endregion

        #region Properties

        /// <summary>
        /// Property for email
        /// </summary>
        public string Email
        {
            get
            {
                if (CurrentCustomer.Email == null)
                {
                    return CurrentCustomer.Email;
                }
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(CurrentCustomer.Email);
                if (!match.Success)
                {
                    return null;
                }
                return CurrentCustomer.Email;

            }
            set
            {
                CurrentCustomer.Email = value;
                OnPropertyChanged($"Email");
            }
        }

        /// <summary>
        /// Property for password
        /// </summary>
        public string Password
        {
            get
            {
                if (CurrentCustomer.Password == null)
                {
                    return CurrentCustomer.Password;
                }

                if (CurrentCustomer.Password.Length < 5)
                {
                    return null;
                }
                return CurrentCustomer.Password;
            } 
            set
            {
                CurrentCustomer.Password = value;
                OnPropertyChanged($"Password");
            }
        }
        /// <summary>
        /// Property for first name
        /// </summary>
        public string FirstName
        {
            get
            {
                if (CurrentCustomer.First_Name == null)
                {
                    return CurrentCustomer.First_Name;
                }

                if (CurrentCustomer.First_Name.Length < 3)
                {
                    return null;
                }
                return CurrentCustomer.First_Name;
            }
            set
            {
                CurrentCustomer.First_Name = value;
                OnPropertyChanged($"First_Name");
            }
        }

        /// <summary>
        /// Property for last name
        /// </summary>
        public string LastName
        {
            get
            {
                if (CurrentCustomer.Last_Name == null)
                {
                    return CurrentCustomer.Last_Name;
                }

                if (CurrentCustomer.Last_Name.Length < 3)
                {
                    return null;
                }
                return CurrentCustomer.Last_Name;
            }
            set
            {
                CurrentCustomer.Last_Name = value;
                OnPropertyChanged($"Last_Name");
            }
        }

        /// <summary>
        /// Property for phone number
        /// </summary>
        public int PhoneNumber
        {
            get
            {
                if (CurrentCustomer.Last_Name == null)
                {
                    return CurrentCustomer.Phone_Number;
                }
                Regex regex = new Regex("^[0-9]+$");
                Match match = regex.Match(CurrentCustomer.Phone_Number.ToString());
                var num1 = CurrentCustomer.Phone_Number.ToString();

                if (!match.Success || num1.Length < 5)
                {
                    return 0;
                }

                return CurrentCustomer.Phone_Number;
            }
            set
            {
                CurrentCustomer.Phone_Number = value;
                OnPropertyChanged($"Phone_Number");
            }
        }

        /// <summary>
        /// Property for address
        /// </summary>
        public string Address
        {
            get
            {
                if (CurrentCustomer.Address == null)
                {
                    return CurrentCustomer.Address;
                }
                Regex regex = new Regex(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
                Match match = regex.Match(CurrentCustomer.Address);
                if (!match.Success)
                {
                    return null;
                }

                return CurrentCustomer.Address;
            }
            set
            {
                CurrentCustomer.Address = value;
                OnPropertyChanged($"Address");
            }
        }

        /// <summary>
        /// Property for city
        /// </summary>
        public string City
        {
            get
            {
                if (CurrentCustomer.City == null)
                {
                    return CurrentCustomer.City;
                }
                Regex regex = new Regex(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$");
                Match match = regex.Match(CurrentCustomer.City);
                if (!match.Success)
                {
                    return null;
                }

                return CurrentCustomer.City;
            }
            set
            {
                CurrentCustomer.City = value;
                OnPropertyChanged($"City");
            }
        }

        /// <summary>
        /// Property for state
        /// </summary>
        public string State
        {
            get
            {
                if (CurrentCustomer.State == null)
                {
                    return CurrentCustomer.State;
                }
                Regex regex = new Regex(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$");
                Match match = regex.Match(CurrentCustomer.State);

                if (!match.Success || CurrentCustomer.State.Length > 3)
                {
                    return null;
                }

                return CurrentCustomer.State;
            }
            set
            {
                CurrentCustomer.State = value;
                OnPropertyChanged($"State");
            }
        }
        /// <summary>
        /// Property for postcode
        /// </summary>
        public int Postcode
        {
            get
            {
                if (CurrentCustomer.Postcode == 0)
                {
                    return CurrentCustomer.Postcode;
                }
                Regex regex = new Regex(@"^\d{4}(?:[-\s]\d{4})?$");
                Match match = regex.Match(CurrentCustomer.Postcode.ToString());
                var pc = CurrentCustomer.Postcode.ToString();

                if (!match.Success || pc.Length < 4 || pc.Length > 4)
                {
                    return 0;
                }

                return CurrentCustomer.Postcode;
            }
            set
            {
                CurrentCustomer.Postcode = value;
                OnPropertyChanged($"Postcode");
            }
        }
        /// <summary>
        /// Property for date of birth
        /// </summary>
        public DateTime DateOfBirth
        {
            get
            {
                if (CurrentCustomer.Date_Of_Birth == DateTime.MinValue)
                {
                    return DateTime.Now;
                }

                return CurrentCustomer.Date_Of_Birth;
            }
            set
            {
                CurrentCustomer.Date_Of_Birth = value;
                OnPropertyChanged($"Date_Of_Birth");
            }
        }

        #endregion

        #region Saving context changes
        /// <summary>
        /// Saves changes to the database
        /// </summary>
        /// <returns>Boolean value</returns>
        private bool SaveCustomerChanges()
        {
            //Assign a context value
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Set the initial value to false
            bool output = false;

            //Assigns the value of the helper class to the class
            bool isNull = ArePropertiesNotNull(CurrentCustomer);

            //Checks if helper class returns true or false
            if (isNull)
            {
                CurrentCustomer.Email = Email.Trim();
                CurrentCustomer.Password = Password.Trim();
                CurrentCustomer.First_Name = FirstName.Trim();
                CurrentCustomer.Last_Name = LastName.Trim();
                CurrentCustomer.Phone_Number = PhoneNumber;
                CurrentCustomer.Address = Address.Trim();
                CurrentCustomer.City = City.Trim();
                CurrentCustomer.State = State.Trim();
                CurrentCustomer.Postcode = Postcode;
                CurrentCustomer.Date_Of_Birth = DateOfBirth;
                CurrentCustomer.Date_Created = DateTime.Now;

                context.Customer_Details.Add(CurrentCustomer);

                context.SaveChanges();
                output = true;
                return output;
            }

            return output;

        }

        #endregion

        #region Checking for null
        /// <summary>
        /// A function which checks whether the select object is
        /// null or not null
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">The object itself</param>
        /// <returns>A boolean value</returns>
        public bool ArePropertiesNotNull<T>(T obj)
        {
            return typeof(T).GetProperties().All(propertyInfo => propertyInfo.GetValue(obj) != null);
        }

        #endregion

    }

}
