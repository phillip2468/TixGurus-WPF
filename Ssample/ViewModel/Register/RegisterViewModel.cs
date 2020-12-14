using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Ssample.ViewModel.Register
{
    /// <summary>
    /// A class responsible for registering
    /// users to TixGurus
    /// </summary>
    public class RegisterViewModel : NavigationViewModelBase
    {
        #region Fields for view models
        //Field for successful register view model
        private NavigationViewModelBase successfulRegisterViewModel;

        #endregion

        #region Navigation Commands

        /// <summary>
        /// Command which go to the successful register screen
        /// </summary>
        public ICommand GoToSuccessfulViewCommand { get; set; }

        /// <summary>
        /// Command which navigates to the respective view model
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisterViewModel()
        {
            successfulRegisterViewModel = new SuccessfulRegisterViewModel();

            //Commands to go to successful page
            GoToSuccessfulViewCommand = new RelayCommand(GoToSuccessful);

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        #endregion

        #region Helper functions
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
        /// <summary>
        /// Function that accepts a view model as the
        /// parameter
        /// </summary>
        /// <param name="viewModel">ViewModel parameter</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
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
                if (CurrentCustomer.email == null)
                {
                    return CurrentCustomer.email;
                }
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(CurrentCustomer.email);
                if (!match.Success)
                {
                    return null;
                }
                return CurrentCustomer.email;

            }
            set
            {
                CurrentCustomer.email = value;
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
                if (CurrentCustomer.password == null)
                {
                    return CurrentCustomer.password;
                }

                if (CurrentCustomer.password.Length < 5)
                {
                    return null;
                }
                return CurrentCustomer.password;
            }
            set
            {
                CurrentCustomer.password = value;
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
                if (CurrentCustomer.firstName == null)
                {
                    return CurrentCustomer.firstName;
                }

                if (CurrentCustomer.firstName.Length < 3)
                {
                    return null;
                }
                return CurrentCustomer.firstName;
            }
            set
            {
                CurrentCustomer.firstName = value;
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
                if (CurrentCustomer.lastName == null)
                {
                    return CurrentCustomer.lastName;
                }

                if (CurrentCustomer.lastName.Length < 3)
                {
                    return null;
                }
                return CurrentCustomer.lastName;
            }
            set
            {
                CurrentCustomer.lastName = value;
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
                if (CurrentCustomer.lastName == null)
                {
                    return CurrentCustomer.phoneNumber;
                }
                Regex regex = new Regex("^[0-9]+$");
                Match match = regex.Match(CurrentCustomer.phoneNumber.ToString());
                var num1 = CurrentCustomer.phoneNumber.ToString();

                if (!match.Success || num1.Length < 5)
                {
                    return 0;
                }

                return CurrentCustomer.phoneNumber;
            }
            set
            {
                CurrentCustomer.phoneNumber = value;
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
                if (CurrentCustomer.address == null)
                {
                    return CurrentCustomer.address;
                }
                Regex regex = new Regex(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
                Match match = regex.Match(CurrentCustomer.address);
                if (!match.Success)
                {
                    return null;
                }

                return CurrentCustomer.address;
            }
            set
            {
                CurrentCustomer.address = value;
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
                if (CurrentCustomer.city == null)
                {
                    return CurrentCustomer.city;
                }
                Regex regex = new Regex(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$");
                Match match = regex.Match(CurrentCustomer.city);
                if (!match.Success)
                {
                    return null;
                }

                return CurrentCustomer.city;
            }
            set
            {
                CurrentCustomer.city = value;
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
                if (CurrentCustomer.state == null)
                {
                    return CurrentCustomer.state;
                }
                Regex regex = new Regex(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$");
                Match match = regex.Match(CurrentCustomer.state);

                if (!match.Success || CurrentCustomer.state.Length > 3)
                {
                    return null;
                }

                return CurrentCustomer.state;
            }
            set
            {
                CurrentCustomer.state = value;
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
                if (CurrentCustomer.postcode == 0)
                {
                    return CurrentCustomer.postcode;
                }
                Regex regex = new Regex(@"^\d{4}(?:[-\s]\d{4})?$");
                Match match = regex.Match(CurrentCustomer.postcode.ToString());
                var pc = CurrentCustomer.postcode.ToString();

                if (!match.Success || pc.Length < 4 || pc.Length > 4)
                {
                    return 0;
                }

                return CurrentCustomer.postcode;
            }
            set
            {
                CurrentCustomer.postcode = value;
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
                if (CurrentCustomer.dateOfBirth == DateTime.MinValue)
                {
                    return DateTime.Now;
                }

                return CurrentCustomer.dateOfBirth;
            }
            set
            {
                CurrentCustomer.dateOfBirth = value;
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
                CurrentCustomer.email = Email.Trim();
                CurrentCustomer.password = Password.Trim();
                CurrentCustomer.firstName = FirstName.Trim();
                CurrentCustomer.lastName = LastName.Trim();
                CurrentCustomer.phoneNumber = PhoneNumber;
                CurrentCustomer.address = Address.Trim();
                CurrentCustomer.city = City.Trim();
                CurrentCustomer.state = State.Trim();
                CurrentCustomer.postcode = Postcode;
                CurrentCustomer.dateOfBirth = DateOfBirth;
                CurrentCustomer.dateCreated = DateTime.Now;

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
