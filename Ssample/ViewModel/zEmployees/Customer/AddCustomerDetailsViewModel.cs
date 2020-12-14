using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Customer
{
    /// <summary>
    /// A class responsible for adding customers
    /// if needed by the employee
    /// </summary>
    public class AddCustomerDetailsViewModel : NavigationViewModelBase
    {
        #region Commands
        /// <summary>
        /// A command responsible for navigating to view model
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor
        public AddCustomerDetailsViewModel()
        {
            //Command parameters
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            //Creating the list of customers
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            CustomerDetails = (from data in context.Customer_Details select data).ToList();
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// Function responsible to go to respective view model
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        #endregion

        #region Customer list property
        private List<Customer_Details> Customer { get; set; }

        public List<Customer_Details> CustomerDetails
        {
            get => Customer;
            set => Customer = value;
        }

        #endregion
    }
}
