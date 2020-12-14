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
    public class SuccessfulCustomerTicketPurchaseViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }
        public SuccessfulCustomerTicketPurchaseViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            //Assign the database context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Get the ticket Id's and split them into an array
            string guestTicketId = Settings.Default.customerTicketId;
            String[] separator = { "," };
            string[] guestTicketList = guestTicketId.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            //For each value in guest ticket list add them to the data source
            foreach (var ticketId in guestTicketList)
            {
                var rowData = (from data in context.Customer_Ticket_Details where data.ticketId.ToString().Contains(ticketId) && data.email.Contains(Settings.Default.Email) select data).ToList();
                foreach (var customerTicketDetails in rowData) CustomerTicketDetails.Add(customerTicketDetails);
            }

            //Display guest transaction according to the guest transaction id
            CustomerTransactionsDetails = (from data in context.Customer_Transaction
                                           where data.transactionId.ToString().Contains(Settings.Default.customerTransactionId)
                                           select data).ToList();
        }
        public List<Customer_Ticket_Details> CustomerTicketDetails { get; set; } = new List<Customer_Ticket_Details>();

        public List<Customer_Transaction> CustomerTransactionsDetails { get; set; } = new List<Customer_Transaction>();

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

    }
}
