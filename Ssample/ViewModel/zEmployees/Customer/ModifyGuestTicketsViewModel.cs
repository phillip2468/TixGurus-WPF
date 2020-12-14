using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Customer
{
    /// <summary>
    /// A class responsible for editing guest transactions/ tickets
    /// </summary>
    public class ModifyGuestTicketsViewModel : NavigationViewModelBase
    {
        #region Commands
        /// <summary>
        /// Command for moving backwards
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor

        public ModifyGuestTicketsViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            #region Lists with data context

            GuestTransactionsDetails = (from data in context.Guest_Transaction select data).ToList();
            GuestTicketDetails = (from data in context.Guest_Ticket_Details select data).ToList();

            #endregion
        }

        #endregion

        #region Helper functions

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        #endregion

        #region List properties

        public List<Guest_Transaction> GuestTransactionsDetails { get; set; } = new List<Guest_Transaction>();
        public List<Guest_Ticket_Details> GuestTicketDetails { get; set; } = new List<Guest_Ticket_Details>();

        #endregion
    }
}
