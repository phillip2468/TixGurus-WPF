using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.Properties;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Ssample.ViewModel.Dashboard
{
    /// <summary>
    /// A class which allows the
    /// selected user to change
    /// their personal details
    /// </summary>
    public class ChangeMyDetailsViewModel : NavigationViewModelBase
    {
        #region Commands
        public ICommand GoBackCommand { get; set; }

        public ICommand NavCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for changning my details
        /// </summary>
        public ChangeMyDetailsViewModel()
        {
            //Navigation command that goes to the default screen
            //.Logs the user out
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            //Command to go backwards
            GoBackCommand = new RelayCommand(GoBack);
        }
        #endregion

        #region Helper functions
        /// <summary>
        /// Function to go backwards
        /// using the navigate back
        /// helper
        /// </summary>
        private void GoBack()
        {
            NavigateBack();
        }

        /// <summary>
        /// Function which navigates to the
        /// default viewmodel if successful
        /// </summary>
        /// <param name="viewModel">The view model</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            //If true
            if (SaveCustomerChanges())
            {
                //Go to view model
                Navigate(viewModel);
            }
            else
            {
                MessageBox.Show("An error has occured");
            }
        }
        #endregion

        #region Fields for entering values for database

        /// <summary>
        /// Setting the customer database context
        /// </summary>
        public CustomerDatabaseEntities context = new CustomerDatabaseEntities();

        /// <summary>
        /// Current customer in which you wish
        /// to edit
        /// </summary>
        public Customer_Details CurrentCustomer { get; set; } = new Customer_Details();

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
                    return EmailSet();
                }

                if (CurrentCustomer.email != EmailSet())
                {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(CurrentCustomer.email);
                    if (!match.Success)
                    {
                        return null;
                    }

                    return CurrentCustomer.email;
                }
                CurrentCustomer.email = EmailSet();
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
                    return PasswordSet();
                }

                if (CurrentCustomer.password != PasswordSet())
                {
                    if (CurrentCustomer.password.Length < 5)
                    {
                        return null;
                    }
                    return CurrentCustomer.password;
                }
                CurrentCustomer.password = PasswordSet();
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
                    return FirstNameSet();
                }

                if (CurrentCustomer.firstName != FirstNameSet())
                {
                    if (CurrentCustomer.firstName.Length < 3)
                    {
                        return null;
                    }
                    return CurrentCustomer.firstName;
                }
                CurrentCustomer.firstName = FirstNameSet();
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
                    return LastNameSet();
                }
                if (CurrentCustomer.lastName != LastNameSet())
                {
                    if (CurrentCustomer.lastName.Length < 3)
                    {
                        return null;
                    }
                    return CurrentCustomer.lastName;
                }
                CurrentCustomer.lastName = LastNameSet();
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
                if (CurrentCustomer.phoneNumber == 0)
                {
                    return PhoneNumberSet();
                }

                if (CurrentCustomer.phoneNumber != PhoneNumberSet())
                {
                    Regex regex = new Regex("^[0-9]+$");
                    Match match = regex.Match(CurrentCustomer.phoneNumber.ToString());
                    var num1 = CurrentCustomer.phoneNumber.ToString();

                    if (!match.Success || num1.Length < 5)
                    {
                        return 0;
                    }

                    return CurrentCustomer.phoneNumber;
                }
                CurrentCustomer.phoneNumber = PhoneNumberSet();
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
                    return AddressSet();
                }

                if (CurrentCustomer.address != AddressSet())
                {
                    Regex regex = new Regex(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
                    Match match = regex.Match(CurrentCustomer.address);
                    if (!match.Success)
                    {
                        return null;
                    }

                    return CurrentCustomer.address;
                }
                CurrentCustomer.address = AddressSet();
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
                    return CitySet();
                }

                if (CurrentCustomer.city != CitySet())
                {
                    Regex regex = new Regex(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$");
                    Match match = regex.Match(CurrentCustomer.city);
                    if (!match.Success)
                    {
                        return null;
                    }

                    return CurrentCustomer.city;
                }
                CurrentCustomer.city = CitySet();
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
                    return StateSet();
                }

                if (CurrentCustomer.state != StateSet())
                {
                    Regex regex = new Regex(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$");
                    Match match = regex.Match(CurrentCustomer.state);

                    if (!match.Success || CurrentCustomer.state.Length > 3)
                    {
                        return null;
                    }

                    return CurrentCustomer.state;
                }
                CurrentCustomer.state = StateSet();
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
                    return PostcodeSet();
                }

                if (CurrentCustomer.postcode != PostcodeSet())
                {
                    Regex regex = new Regex(@"^\d{4}(?:[-\s]\d{4})?$");
                    Match match = regex.Match(CurrentCustomer.postcode.ToString());
                    var pc = CurrentCustomer.postcode.ToString();

                    if (!match.Success || pc.Length < 4 || pc.Length > 4)
                    {
                        return 0;
                    }

                    return CurrentCustomer.postcode;
                }
                CurrentCustomer.postcode = PostcodeSet();
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
                    return DateOfBirthSet();
                }

                if (CurrentCustomer.dateOfBirth != DateOfBirthSet())
                {
                    return CurrentCustomer.dateOfBirth;
                }
                CurrentCustomer.dateOfBirth = DateOfBirthSet();
                return CurrentCustomer.dateOfBirth;
            }
            set
            {
                CurrentCustomer.dateOfBirth = value;
                OnPropertyChanged($"Date_Of_Birth");
            }
        }

        public DateTime DateCreated
        {
            get
            {
                if (CurrentCustomer.dateCreated == DateTime.MinValue)
                {
                    return DateCreatedSet();
                }

                if (CurrentCustomer.dateCreated != DateCreatedSet())
                {
                    return CurrentCustomer.dateCreated;
                }
                CurrentCustomer.dateCreated = DateCreatedSet();
                return CurrentCustomer.dateCreated;
            }
            set => CurrentCustomer.dateCreated = value;
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

            //Attempts to search the database for
            //a matching customer
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);

            //Set the initial value to false
            bool output = false;

            //Assigns the value of the helper class to the class
            bool isNull = ArePropertiesNotNull(CurrentCustomer);

            //Assign the getter fields to a value

            #region Assigning each property to a value

            var email = Email;
            var password = Password;
            var firstName = FirstName;
            var lastName = LastName;
            var phoneNumber = PhoneNumber;
            var address = Address;
            var city = City;
            var state = State;
            var postcode = Postcode;
            var dob = DateOfBirth;
            var dc = DateCreated;
            var ID = currentCustomer.customerID;

            #endregion


            //Statement which checks if there are any changes to the 
            //entered in fields
            if (isNull == false || email != currentCustomer.email || password != currentCustomer.password || FirstName != currentCustomer.firstName || PhoneNumber != currentCustomer.phoneNumber || Address != currentCustomer.address || City != currentCustomer.city || State != currentCustomer.state || Postcode != currentCustomer.postcode || DateOfBirth != currentCustomer.dateOfBirth)
            {
                //Assign each value to be sent to the database
                #region Assigning values to entity framework
                CurrentCustomer.customerID = ID;
                CurrentCustomer.email = email;
                CurrentCustomer.password = password;
                CurrentCustomer.firstName = firstName;
                CurrentCustomer.lastName = lastName;
                CurrentCustomer.phoneNumber = phoneNumber;
                CurrentCustomer.address = address;
                CurrentCustomer.city = city;
                CurrentCustomer.state = state;
                CurrentCustomer.postcode = postcode;
                CurrentCustomer.dateOfBirth = dob;
                CurrentCustomer.dateCreated = dc;
                #endregion

                //Saving if new customer field otherwise updating current field
                context.Customer_Details.AddOrUpdate(CurrentCustomer);
                context.SaveChanges();

                //Return true
                output = true;
                return output;
            }
            return output;
        }

        #endregion

        #region Setting values helper functions

        /// <summary>
        /// Function which returns
        /// the first name from selected
        /// customer
        /// </summary>
        /// <returns>String of first name</returns>
        private string FirstNameSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var firstName = currentCustomer.firstName;
                return firstName;
            }

            return "Error setting first name";
        }

        /// <summary>
        /// Function which returns last
        /// name from selected customer
        /// </summary>
        /// <returns>String of last name</returns>
        private string LastNameSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var lastName = currentCustomer.firstName;
                return lastName;
            }

            return "Error setting first name";
        }
        /// <summary>
        /// Function which returns email from selected customer
        /// in database
        /// </summary>
        /// <returns>String of email</returns>
        private string EmailSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var email = currentCustomer.email;
                return email;
            }

            return "Error setting email";
        }
        /// <summary>
        /// Function which returns password from selected customer in database
        /// </summary>
        /// <returns>String of password</returns>
        private string PasswordSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var password = currentCustomer.password;
                return password;
            }

            return "Error setting password";
        }
        /// <summary>
        /// Function which returns the phone number from selected customer in database
        /// </summary>
        /// <returns>Integer values of phone number</returns>
        private int PhoneNumberSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var phoneNumber = currentCustomer.phoneNumber;
                return phoneNumber;
            }

            return 123;
        }
        /// <summary>
        /// Function which returns the address from selected customer in database
        /// </summary>
        /// <returns>String of address</returns>
        private string AddressSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var address = currentCustomer.address;
                return address;
            }

            return "Error setting address";
        }
        /// <summary>
        /// Function which returns the city from selected customer in database
        /// </summary>
        /// <returns>String of city</returns>
        private string CitySet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var city = currentCustomer.city;
                return city;
            }

            return "Error setting city";
        }
        /// <summary>
        /// Function which returns the state from selected customer in database
        /// </summary>
        /// <returns>String of state</returns>
        private string StateSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var state = currentCustomer.state;
                return state;
            }

            return "Error setting state";
        }
        /// <summary>
        /// Function which returns the postcode from selected customer in database
        /// </summary>
        /// <returns>Int of postcode</returns>
        private int PostcodeSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var postcode = currentCustomer.postcode;
                return postcode;
            }

            return 123;
        }
        /// <summary>
        ///  Function which returns the date of birth from selected customer in database
        /// </summary>
        /// <returns>DateTime of date of birth</returns>
        private DateTime DateOfBirthSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var dob = currentCustomer.dateOfBirth;
                return dob;
            }

            return DateTime.Now;
        }

        /// <summary>
        ///  Function which returns the date created from selected customer in database
        /// </summary>
        /// <returns>DateTime of date created</returns>
        private DateTime DateCreatedSet()
        {
            var currentCustomer = context.Customer_Details
                .FirstOrDefault(c => c.email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var dc = currentCustomer.dateCreated;
                return dc;
            }

            return DateTime.Now;
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
