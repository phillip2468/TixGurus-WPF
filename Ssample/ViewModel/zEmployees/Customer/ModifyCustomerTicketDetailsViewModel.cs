using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Customer
{
    /// <summary>
    /// A class responsible for modifying the ticket details
    /// </summary>
    public class ModifyCustomerTicketDetailsViewModel : NavigationViewModelBase
    {
        #region Commands

        /// <summary>
        /// A command responsible for navigation
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor

        public ModifyCustomerTicketDetailsViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            CustomerTicketDetails = (from data in context.Customer_Ticket_Details select data).ToList();
            CustomerTransactions = (from data in context.Customer_Transaction select data).ToList();
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// A function which navigates to the view model
        /// given by its argument
        /// </summary>
        /// <param name="viewModel">The view model</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        public List<Customer_Ticket_Details> CustomerTicketDetails { get; set; } = new List<Customer_Ticket_Details>();
        public List<Customer_Transaction> CustomerTransactions { get; set; } = new List<Customer_Transaction>();

        #endregion
    }
}
