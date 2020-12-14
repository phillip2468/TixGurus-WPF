using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel
{
    public class DataEventsViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }
        public DataEventsViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            GuestTicketDetails = (from data in context.Guest_Ticket_Details select data).ToList();
            CustomerTicketDetails = (from data in context.Customer_Ticket_Details select data).ToList();
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        public List<Customer_Ticket_Details> CustomerTicketDetails { get; set; } = new List<Customer_Ticket_Details>();
        public List<Guest_Ticket_Details> GuestTicketDetails { get; set; } = new List<Guest_Ticket_Details>();
    }
}
