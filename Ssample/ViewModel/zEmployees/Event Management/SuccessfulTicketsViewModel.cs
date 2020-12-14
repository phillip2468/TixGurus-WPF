using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Event_Management
{
    /// <summary>
    /// View model responsible for showing all tickets that have been generated
    /// </summary>
    public class SuccessfulTicketsViewModel : NavigationViewModelBase
    {
        #region Commands
        /// <summary>
        /// Commands for navigating
        /// </summary>
        public ICommand NavCommand { get; set; }
        #endregion

        #region Constructor
        public SuccessfulTicketsViewModel()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            TicketList = (from data in context.Ticket_Details select data).ToList();
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// Navigates to the employee dashboard
        /// </summary>
        /// <param name="viewModel">The viewmodel</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        #endregion

        #region Tickets list

        private List<Ticket_Details> Ticket { get; set; }

        /// <summary>
        /// Shows the ticket list
        /// </summary>
        public List<Ticket_Details> TicketList
        {
            get => Ticket;
            set => Ticket = value;
        }

        #endregion

    }
}
