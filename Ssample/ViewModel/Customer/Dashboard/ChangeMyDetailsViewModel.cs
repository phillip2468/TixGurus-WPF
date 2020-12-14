using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.Properties;

namespace Ssample.ViewModel.Dashboard
{
    /// <summary>
    /// A class which allows the
    /// selected user to change
    /// their personal details
    /// </summary>
    public class ChangeMyDetailsViewModel : NavigationViewModelBase
    {
        #region Fields
        private NavigationViewModelBase myAccountViewModelBase;
        #endregion

        #region Commands
        public ICommand GoBackCommand { get; set; }

        public ICommand NavCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="defaultViewModel"></param>
        public ChangeMyDetailsViewModel(NavigationViewModelBase defaultViewModel)
        {
            //Navigation command that goes to the default screen
            //.Logs the user out
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            //Command to go backwards
            GoBackCommand = new RelayCommand(GoBack);
        }
        #endregion

        #region MyRegion
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
                if (CurrentCustomer.Email == null)
                {
                    return EmailSet();
                }

                if (CurrentCustomer.Email != EmailSet())
                {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(CurrentCustomer.Email);
                    if (!match.Success)
                    {
                        return null;
                    }

                    return CurrentCustomer.Email;
                }
                CurrentCustomer.Email = EmailSet();
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
                    return PasswordSet();
                }

                if (CurrentCustomer.Password != PasswordSet())
                {
                    if (CurrentCustomer.Password.Length < 5)
                    {
                        return null;
                    }
                    return CurrentCustomer.Password;
                }
                CurrentCustomer.Password = PasswordSet();
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
                    return FirstNameSet();
                }

                if (CurrentCustomer.First_Name != FirstNameSet())
                {
                    if (CurrentCustomer.First_Name.Length < 3)
                    {
                        return null;
                    }
                    return CurrentCustomer.First_Name;
                }
                CurrentCustomer.First_Name = FirstNameSet();
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
                    return LastNameSet();
                }
                if (CurrentCustomer.Last_Name != LastNameSet())
                {
                    if (CurrentCustomer.Last_Name.Length < 3)
                    {
                        return null;
                    }
                    return CurrentCustomer.Last_Name;
                }
                CurrentCustomer.Last_Name = LastNameSet();
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
                if (CurrentCustomer.Phone_Number == 0)
                {
                    return PhoneNumberSet();
                }

                if (CurrentCustomer.Phone_Number != PhoneNumberSet())
                {
                    Regex regex = new Regex("^[0-9]+$");
                    Match match = regex.Match(CurrentCustomer.Phone_Number.ToString());
                    var num1 = CurrentCustomer.Phone_Number.ToString();

                    if (!match.Success || num1.Length < 5)
                    {
                        return 0;
                    }

                    return CurrentCustomer.Phone_Number;
                }
                CurrentCustomer.Phone_Number = PhoneNumberSet();
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
                    return AddressSet();
                }

                if (CurrentCustomer.Address != AddressSet())
                {
                    Regex regex = new Regex(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
                    Match match = regex.Match(CurrentCustomer.Address);
                    if (!match.Success)
                    {
                        return null;
                    }

                    return CurrentCustomer.Address;
                }
                CurrentCustomer.Address = AddressSet();
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
                    return CitySet();
                }

                if (CurrentCustomer.City != CitySet())
                {
                    Regex regex = new Regex(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$");
                    Match match = regex.Match(CurrentCustomer.City);
                    if (!match.Success)
                    {
                        return null;
                    }

                    return CurrentCustomer.City;
                }
                CurrentCustomer.City = CitySet();
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
                    return StateSet();
                }

                if (CurrentCustomer.State != StateSet())
                {
                    Regex regex = new Regex(@"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$");
                    Match match = regex.Match(CurrentCustomer.State);

                    if (!match.Success || CurrentCustomer.State.Length > 3)
                    {
                        return null;
                    }

                    return CurrentCustomer.State;
                }
                CurrentCustomer.State = StateSet();
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
                    return PostcodeSet();
                }

                if (CurrentCustomer.Postcode != PostcodeSet())
                {
                    Regex regex = new Regex(@"^\d{4}(?:[-\s]\d{4})?$");
                    Match match = regex.Match(CurrentCustomer.Postcode.ToString());
                    var pc = CurrentCustomer.Postcode.ToString();

                    if (!match.Success || pc.Length < 4 || pc.Length > 4)
                    {
                        return 0;
                    }

                    return CurrentCustomer.Postcode;
                }
                CurrentCustomer.Postcode = PostcodeSet();
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
                    return DateOfBirthSet();
                }

                if (CurrentCustomer.Date_Of_Birth != DateOfBirthSet())
                {
                    return CurrentCustomer.Date_Of_Birth;
                }
                CurrentCustomer.Date_Of_Birth = DateOfBirthSet();
                return CurrentCustomer.Date_Of_Birth;
            }
            set
            {
                CurrentCustomer.Date_Of_Birth = value;
                OnPropertyChanged($"Date_Of_Birth");
            }
        }

        public DateTime DateCreated
        {
            get
            {
                if (CurrentCustomer.Date_Created == DateTime.MinValue)
                {
                    return DateCreatedSet();
                }

                if (CurrentCustomer.Date_Created != DateCreatedSet())
                {
                    return CurrentCustomer.Date_Created;
                }
                CurrentCustomer.Date_Created = DateCreatedSet();
                return CurrentCustomer.Date_Created;
            }
            set => CurrentCustomer.Date_Created = value;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);

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
            var ID = currentCustomer.Customer_ID;

            #endregion


            //Statement which checks if there are any changes to the 
            //entered in fields
            if (isNull == false || email != currentCustomer.Email || password != currentCustomer.Password || FirstName != currentCustomer.First_Name || PhoneNumber != currentCustomer.Phone_Number || Address != currentCustomer.Address || City != currentCustomer.City || State != currentCustomer.State || Postcode != currentCustomer.Postcode || DateOfBirth != currentCustomer.Date_Of_Birth)
            {
                //Assign each value to be sent to the database
                #region Assigning values to entity framework
                CurrentCustomer.Customer_ID = ID;
                CurrentCustomer.Email = email;
                CurrentCustomer.Password = password;
                CurrentCustomer.First_Name = firstName;
                CurrentCustomer.Last_Name = lastName;
                CurrentCustomer.Phone_Number = phoneNumber;
                CurrentCustomer.Address = address;
                CurrentCustomer.City = city;
                CurrentCustomer.State = state;
                CurrentCustomer.Postcode = postcode;
                CurrentCustomer.Date_Of_Birth = dob;
                CurrentCustomer.Date_Created = dc;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var firstName = currentCustomer.First_Name;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var lastName = currentCustomer.First_Name;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var email = currentCustomer.Email;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var password = currentCustomer.Password;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var phoneNumber = currentCustomer.Phone_Number;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var address = currentCustomer.Address;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var city = currentCustomer.City;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var state = currentCustomer.State;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var postcode = currentCustomer.Postcode;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var dob = currentCustomer.Date_Of_Birth;
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
                .FirstOrDefault(c => c.Email == Settings.Default.Email);
            if (currentCustomer != null)
            {
                var dc = currentCustomer.Date_Created;
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
