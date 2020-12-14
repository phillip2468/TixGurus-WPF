using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Ssample.ViewModel.Buying_tickets
{
    /// <summary>
    /// A class responsible for purchasing tickets as a customer
    /// </summary>
    public class CustomerPurchaseTicketViewModel : NavigationViewModelBase
    {

        #region Commands
        /// <summary>
        /// Navigates to the default view model
        /// </summary>
        public ICommand NavDefaultCommand { get; set; }
        /// <summary>
        /// Navigates to the successful view model if purchase
        /// succeeds
        /// </summary>
        public ICommand NavSuccessCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for view model
        /// </summary>
        public CustomerPurchaseTicketViewModel()
        {
            #region Command parameter
            //Default command
            NavDefaultCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            //Successful command
            NavSuccessCommand = new RelayCommand<NavigationViewModelBase>(Nav2);

            #endregion

            //Set the database context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Tickets list
            Tickets = (from data in context.Ticket_Details select data).ToList();

            //Create a ticket list with the selected seats
            string seatLocations = Settings.Default.SeatLocation;
            String[] separator = { "," };
            String[] strList = seatLocations.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var seatLocation in strList)
            {
                var item = Tickets.FindAll(i => i.seatLocation == seatLocation && i.eventTitle == Settings.Default.EventTitle);
                foreach (var ticketDetails in item) ListOfMatchingTickets.Add(ticketDetails);
            }
        }

        #endregion

        #region List properties

        public List<Ticket_Details> ListOfMatchingTickets { get; set; } = new List<Ticket_Details>();

        public List<Ticket_Details> Tickets { get; set; }

        /// <summary>
        /// Gets a list of matching customers from the properties
        /// </summary>
        public List<Customer_Details> ListOfMatchingCustomers { get; set; } = new List<Customer_Details>();

        /// <summary>
        /// Atttempts to match current customer
        /// </summary>
        public List<Customer_Details> MatchCurrentCustomer { get; set; } = new List<Customer_Details>();

        /// <summary>
        /// Change current ticket
        /// </summary>
        private Customer_Ticket_Details CurrentTicketCustomer { get; set; } = new Customer_Ticket_Details();

        /// <summary>
        /// Sets the current transaction
        /// </summary>
        public Customer_Transaction CurrentCustomerTransactions { get; set; } = new Customer_Transaction();

        /// <summary>
        /// Events list
        /// </summary>
        public List<Event_Details> Events { get; set; } = new List<Event_Details>();

        /// <summary>
        /// Matching events from properties
        /// </summary>
        public List<Event_Details> ListOfMatchingEvents { get; set; } = new List<Event_Details>();

        #endregion

        #region Navigation functions

        private void Nav(NavigationViewModelBase viewModel)
        {
            Settings.Default.SeatLocation = "";
            Settings.Default.Email = "";
            Navigate(viewModel);
        }
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
                    _totalPrice = SetTotalPrice() * (decimal)1.05;
                }

                return _totalPrice;
            }
            set => _totalPrice = value;
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

        #region Setting the price property

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

        #region Saving details to database

        /// <summary>
        /// Save changes to database
        /// </summary>
        /// <returns></returns>
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

            //Get a list of all customers
            MatchCurrentCustomer = (from data in context.Customer_Details select data).ToList();

            var customerDet = Settings.Default.Email;

            //Find the customer that equals to the email
            foreach (var cust in MatchCurrentCustomer)
            {
                if (cust.email == Settings.Default.Email)
                {
                    ListOfMatchingCustomers.Add(cust);
                }
            }

            //Check if for null
            bool isTransactionNull = ArePropertiesNull(CurrentCustomerTransactions);
            bool isCustomerNull = ArePropertiesNull(CurrentTicketCustomer);

            //If not
            if (!isCustomerNull)
            {
                foreach (var ticket in ListOfMatchingTickets)
                {
                    foreach (var customer in ListOfMatchingCustomers)
                    {
                        CurrentTicketCustomer.fullName = customer.firstName.Trim() + " " + customer.lastName.Trim();
                        CurrentTicketCustomer.email = customer.email.Trim();
                        CurrentTicketCustomer.price = ticket.price;
                        CurrentTicketCustomer.timeStart = ticket.eventStart;
                        CurrentTicketCustomer.timeEnd = ticket.eventEnd;
                        CurrentTicketCustomer.seatLocation = ticket.seatLocation.Trim();
                        CurrentTicketCustomer.eventName = Settings.Default.EventTitle;
                        CurrentTicketCustomer.placeName = PlaceName.Trim();
                        CurrentTicketCustomer.eventAddress = EventAddress.Trim();
                        context.Customer_Ticket_Details.Add(CurrentTicketCustomer);
                        context.SaveChanges();
                        Settings.Default.customerTicketId += CurrentTicketCustomer.ticketId + ",";
                    }
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
                foreach (var customer in ListOfMatchingCustomers)
                {
                    CurrentCustomerTransactions.email = customer.email.Trim();
                    CurrentCustomerTransactions.address = customer.address.Trim();
                    CurrentCustomerTransactions.eventAddress = EventAddress.Trim();
                    CurrentCustomerTransactions.eventName = Settings.Default.EventTitle;
                    CurrentCustomerTransactions.totalPrice = TotalPrice;
                    CurrentCustomerTransactions.fullname = customer.firstName.Trim() + " " + customer.lastName.Trim();
                    context.Customer_Transaction.Add(CurrentCustomerTransactions);
                }

                context.SaveChanges();
                Settings.Default.customerTransactionId += CurrentCustomerTransactions.transactionId.ToString();
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
        public bool ArePropertiesNull<T>(T obj)
        {
            return typeof(T).GetProperties().All(propertyInfo => propertyInfo.GetValue(obj) != null);
        }

        #endregion
    }
}
