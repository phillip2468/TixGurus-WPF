using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.Buying_tickets
{
    /// <summary>
    /// A class responsible for viewing the purchasing of tickets
    /// </summary>
    public class SuccessfulPurchaseViewModel : NavigationViewModelBase
    {

        #region Commands
        /// <summary>
        /// Navigation command responsible for navigating to default view model
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for successful purchase
        /// </summary>
        public SuccessfulPurchaseViewModel()
        {
            //Assign the database context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Get the ticket Id's and split them into an array
            string guestTicketId = Settings.Default.guestTicketId;
            String[] separator = { "," };
            string[] guestTicketList = guestTicketId.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            //For each value in guest ticket list add them to the data source
            foreach (var ticketId in guestTicketList)
            {
                var rowData = (from data in context.Guest_Ticket_Details where data.ticketId.ToString().Contains(ticketId) && data.email.Contains(Settings.Default.guestEmail) select data).ToList();
                foreach (var guestTicketDetails in rowData) GuestTicketDetails.Add(guestTicketDetails);
            }

            //Display guest transaction according to the guest transaction id
            GuestTransactionsDetails = (from data in context.Guest_Transaction
                                        where data.transactionId.ToString().Contains(Settings.Default.guestTransactionId)
                                        select data).ToList();

            //Command parameter
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        #endregion

        #region Helper functions

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        #endregion

        #region List properties

        public List<Guest_Ticket_Details> GuestTicketDetails { get; set; } = new List<Guest_Ticket_Details>();

        public List<Guest_Transaction> GuestTransactionsDetails { get; set; } = new List<Guest_Transaction>();

        #endregion
    }
}
