using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.zData_about_event
{
    public class MoreDataViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }
        public MoreDataViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            CustomerTicket = (from data in context.Customer_Ticket_Details select data).ToList();
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        public List<Customer_Ticket_Details> CustomerTicket { get; set; }
        public List<Guest_Ticket_Details> GuestTicket { get; set; }
    }
}
