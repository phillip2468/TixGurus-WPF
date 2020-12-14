using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Ssample.ViewModel.Buying_tickets
{
    /// <summary>
    /// A class responsible for buying tickets
    /// </summary>
    public class PurchaseTicketsViewModel : NavigationViewModelBase
    {

        #region Fields

        private NavigationViewModelBase successfulPurchaseViewModel;
        private NavigationViewModelBase customerPurchaseTicketViewModel;
        #endregion

        #region Commands

        /// <summary>
        /// Command for navigating backwards
        /// </summary>
        public ICommand NavCommand { get; set; }

        /// <summary>
        /// Command for navigating to the successful ticket purchase
        /// </summary>
        public ICommand Nav2Command { get; set; }

        public ICommand NavToCustomerPurchase { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for purchasing tickets
        /// </summary>
        public PurchaseTicketsViewModel()
        {
            #region Initilization of view models

            // ReSharper disable once InvalidXmlDocComment
            ///Initialization of view models
            customerPurchaseTicketViewModel = new CustomerPurchaseTicketViewModel();
            successfulPurchaseViewModel = new SuccessfulPurchaseViewModel();

            #endregion

            #region Command parameter

            //Command to navigate
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            //Command to save changes
            Nav2Command = new RelayCommand<NavigationViewModelBase>(Nav2);

            // ReSharper disable once InvalidXmlDocComment
            ///Command to navigate to customer purchases
            NavToCustomerPurchase = new RelayCommand<NavigationViewModelBase>(Nav3);

            #endregion

            #region Generate list of tickets

            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Tickets = (from data in context.Ticket_Details select data).ToList();

            string seatLocations = Settings.Default.SeatLocation;
            String[] separator = { "," };
            String[] strList = seatLocations.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var seatLocation in strList)
            {
                var item = Tickets.FindAll(i => i.seatLocation == seatLocation && i.eventTitle == Settings.Default.EventTitle);
                foreach (var ticketDetails in item) ListOfMatchingTickets.Add(ticketDetails);
            }

            #endregion

        }

        #endregion

        #region Helper functions

        /// <summary>
        /// Function to navigate backwards
        /// </summary>
        /// <param name="viewModel">ViewModel</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        /// <summary>
        /// Function for saving guest tickets
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav2(NavigationViewModelBase viewModel)
        {
            if (SaveChanges())
            {
                MessageBox.Show("Successful");
                Navigate(viewModel);
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }

        /// <summary>
        /// Function to navigate to purchase customer ticket model
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav3(NavigationViewModelBase viewModel)
        {
            if (CanSignIn(LoginEmail, LoginPassword))
            {
                MessageBox.Show("Successful sign in");
                Navigate(viewModel);
            }
            else
            {
                MessageBox.Show("Wrong sign in details");
            }
        }

        #endregion

        #region Properties for lists
        /// <summary>
        /// Gets a list of matching tickets according to the event name
        /// </summary>
        public List<Ticket_Details> ListOfMatchingTickets { get; set; } = new List<Ticket_Details>();

        public List<Ticket_Details> Tickets { get; set; }

        public List<Event_Details> Events { get; set; } = new List<Event_Details>();

        public List<Event_Details> ListOfMatchingEvents { get; set; } = new List<Event_Details>();

        private Guest_Ticket_Details CurrentGuestTicket { get; set; } = new Guest_Ticket_Details();

        private Guest_Transaction CurrentTransaction { get; set; } = new Guest_Transaction();

        #endregion

        #region Properties


        private decimal _totalPrice;
        /// <summary>
        /// A property for the total price. It takes into account an
        /// 15 % fee for the total
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                if (_totalPrice == 0)
                {
                    _totalPrice = SetTotalPrice() * (decimal)1.15;
                }

                return _totalPrice;
            }
            set => _totalPrice = value;
        }

        /// <summary>
        /// Property for email
        /// </summary>
        public string Email
        {
            get
            {
                if (CurrentGuestTicket.email == null)
                {
                    return CurrentGuestTicket.email;
                }
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(CurrentGuestTicket.email);
                if (!match.Success)
                {
                    return null;
                }
                return CurrentGuestTicket.email;
            }
            set
            {
                CurrentGuestTicket.email = value;
                OnPropertyChanged($"Email");
            }
        }

        private string _firstName;
        /// <summary>
        /// A property for the first name
        /// </summary>
        public string FirstName
        {
            get
            {
                if (_firstName == null)
                {
                    return _firstName;
                }
                if (_firstName.Length < 3)
                {
                    return null;
                }
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged($"FirstName");
            }
        }

        private string _lastName;
        /// <summary>
        /// A property for the last name
        /// </summary>
        public string LastName
        {
            get
            {
                if (_lastName == null)
                {
                    return _lastName;
                }
                if (_lastName.Length < 3)
                {
                    return null;
                }
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged($"LastName");
            }
        }

        /// <summary>
        /// Property for address
        /// </summary>
        public string Address
        {
            get
            {
                if (CurrentTransaction.address == null)
                {
                    return CurrentTransaction.address;
                }
                Regex regex = new Regex(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
                Match match = regex.Match(CurrentTransaction.address);
                if (!match.Success)
                {
                    return null;
                }

                return CurrentTransaction.address;
            }
            set
            {
                CurrentTransaction.address = value;
                OnPropertyChanged($"Address");
            }
        }

        private string _eventAddress;

        /// <summary>
        /// This property returns the address of the location
        /// </summary>
        public string EventAddress
        {
            get
            {
                if (_eventAddress != null) return _eventAddress;
                foreach (var item in ListOfMatchingEvents)
                {
                    var eventAddress = item.eventAddress;
                    _eventAddress = eventAddress;
                    return _eventAddress;
                }

                return _eventAddress;
            }
            set => _eventAddress = value;
        }

        private string _placeName;
        /// <summary>
        /// This property returns the name of the location e.g.
        /// Louis Events Museum
        /// </summary>
        public string PlaceName
        {
            get
            {
                if (_placeName != null) return _placeName;
                foreach (var item in ListOfMatchingEvents)
                {
                    var placeName = item.eventLocation;
                    _placeName = placeName;
                    return _placeName;
                }

                return _placeName;
            }
            set => _placeName = value;
        }


        #endregion

        #region Login property lists

        public Customer_Details CurrentCustomer { get; set; } = new Customer_Details(); //Current customer you need to edit

        #endregion

        #region Properties for sign in

        /// <summary>
        /// Property for email
        /// </summary>
        public string LoginEmail
        {
            get => CurrentCustomer.email;
            set
            {
                CurrentCustomer.email = value;
                OnPropertyChanged($"LoginEmail");
            }
        }

        /// <summary>
        /// Property for password
        /// </summary>
        public string LoginPassword
        {
            get => CurrentCustomer.password;
            set
            {
                CurrentCustomer.password = value;
                OnPropertyChanged($"LoginPassword");
            }
        }
        #endregion

        #region Helper funtcion for setting price

        private decimal SetTotalPrice()
        {
            string seatLocations = Settings.Default.SeatLocation;
            String[] separator = { "," };
            String[] strList = seatLocations.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            decimal total = 0;

            foreach (var seatLocation in strList)
            {
                var item = Tickets.FindAll(i => i.seatLocation == seatLocation && i.eventTitle == Settings.Default.EventTitle);
                foreach (var ticketDetails in item) total += ticketDetails.price;
            }

            return total;
        }

        #endregion

        #region Saving guest transaction to database

        /// <summary>
        /// A function responsible for saving changes to database
        /// </summary>
        /// <returns>Boolean</returns>
        private bool SaveChanges()
        {
            //Sets the inital output to false
            bool output = false;

            //Assign context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            Events = (from data in context.Event_Details select data).ToList();

            //Match event title from properties to the events list and add them to the listofmatchingevents list
            foreach (var eventsEvent in Events)
            {
                var item = Events.FindAll(i => i.eventTitle == Settings.Default.EventTitle);
                foreach (var matchingEvent in item) ListOfMatchingEvents.Add(matchingEvent);
            }

            //Check if for null
            bool isTransactionNull = ArePropertiesNull(CurrentTransaction);
            bool isGuestNull = ArePropertiesNull(CurrentGuestTicket);

            //If not
            if (!isGuestNull)
            {
                foreach (var ticket in ListOfMatchingTickets)
                {
                    CurrentGuestTicket.fullName = FirstName.Trim() + " " + LastName.Trim();
                    CurrentGuestTicket.email = Email.Trim();
                    CurrentGuestTicket.price = ticket.price;
                    CurrentGuestTicket.timeStart = ticket.eventStart;
                    CurrentGuestTicket.timeEnd = ticket.eventEnd;
                    CurrentGuestTicket.seatLocation = ticket.seatLocation.Trim();
                    CurrentGuestTicket.eventName = Settings.Default.EventTitle;
                    CurrentGuestTicket.eventAddress = EventAddress.Trim();
                    CurrentGuestTicket.placeName = PlaceName.Trim();
                    context.Guest_Ticket_Details.Add(CurrentGuestTicket);
                    Properties.Settings.Default.guestTicketId += CurrentGuestTicket.ticketId + ",";
                }
                foreach (var detail in ListOfMatchingTickets)
                {
                    var num1 = detail;
                    context.Entry(num1).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }

            if (!isTransactionNull)
            {
                CurrentTransaction.email = Email.Trim();
                CurrentTransaction.address = Address.Trim();
                CurrentTransaction.eventAddress = EventAddress.Trim();
                CurrentTransaction.eventName = Settings.Default.EventTitle;
                CurrentTransaction.totalPrice = TotalPrice;
                CurrentTransaction.fullName = FirstName.Trim() + " " + LastName.Trim();
                context.Guest_Transaction.Add(CurrentTransaction);
                context.SaveChanges();
                Properties.Settings.Default.guestEmail = Email.Trim();
                Properties.Settings.Default.guestTransactionId += CurrentTransaction.transactionId.ToString();
                output = true;
                return output;
            }

            return output;
        }

        #endregion

        #region Function for signing in
        /// <summary>
        /// Function which checks if values entered are in the database
        /// </summary>
        /// <param name="email">The email property</param>
        /// <param name="password">The password property</param>
        /// <returns></returns>
        public bool CanSignIn(string email, string password)
        {
            //Declares a data context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            bool output = false;

            //Checks for null values
            var user = context.Customer_Details.FirstOrDefault(u => u.email == LoginEmail);

            //Checks if the entered in values are longer than 0
            if (email?.Length > 0 && password?.Length > 0 && user != null)
            {
                //If an entry equals a values in the database return true
                if (user.password == LoginPassword)
                {
                    Properties.Settings.Default.Email = CurrentCustomer.email;
                    Properties.Settings.Default.Save();
                    return true;
                }
            }
            //Returns false if sign in failed
            context.Dispose();
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
        public bool ArePropertiesNull<T>(T obj)
        {
            return typeof(T).GetProperties().All(propertyInfo => propertyInfo.GetValue(obj) != null);
        }

        #endregion

    }
}
