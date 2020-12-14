using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Customer
{
    /// <summary>
    /// A class responsible for modifying details about
    /// a customer, by an administrator/employee
    /// </summary>
    public class ModifyCustomerDetailsViewModel : NavigationViewModelBase
    {
        #region Commands
        /// <summary>
        /// Command to navigate to respective view model
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor
        public ModifyCustomerDetailsViewModel()
        {
            //Command parameter
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            //List generation
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            CustomerDetails = (from data in context.Customer_Details select data).ToList();
        }

        #endregion

        #region Helper function

        /// <summary>
        /// Function which navigates to view model given by the command
        /// parameter
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
            get { return Customer; }
            set { Customer = value; }
        }

        #endregion
    }
}
