using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.Dashboard
{
    /// <summary>
    /// Class for viewing past records
    /// </summary>
    public class ViewPastTicketsViewModel : NavigationViewModelBase
    {
        #region Commands
        /// <summary>
        /// Command for view model
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewPastTicketsViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Set the lists according to the properties
            CustomerTransactions = (from data in context.Customer_Transaction where data.email.Contains(Settings.Default.Email) select data).ToList();
            CustomerTicketDetails = (from data in context.Customer_Ticket_Details where data.email.Contains(Settings.Default.Email) select data).ToList();

        }

        #endregion

        #region Navigation Property

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        #endregion

        #region Properties for lists

        public List<Customer_Transaction> CustomerTransactions { get; set; } = new List<Customer_Transaction>();
        public List<Customer_Ticket_Details> CustomerTicketDetails { get; set; } = new List<Customer_Ticket_Details>();

        #endregion

    }
}
