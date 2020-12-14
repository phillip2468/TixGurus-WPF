using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.ViewModel.zEmployees.Customer;
using Ssample.ViewModel.zEmployees.Event_Management;
using System.Windows.Input;

namespace Ssample.ViewModel
{
    /// <summary>
    /// A view model responsible for the components
    /// of the employee dashboard
    /// </summary>
    public class EmployeeDashboardViewModel : NavigationViewModelBase
    {
        #region Fields

        private NavigationViewModelBase ModifyCustomerDetailsViewModel;
        private NavigationViewModelBase ModifyEmployeeDetailsViewModel;
        private NavigationViewModelBase GenerateTicketsViewModel;
        private NavigationViewModelBase ModifyEventsViewModel;
        private NavigationViewModelBase DataEventsViewModel;
        private NavigationViewModelBase SuccessfulTicketsViewModel;

        #endregion

        #region Commands
        /// <summary>
        /// Command for navigate to the respective view model
        /// </summary>
        public ICommand NavCommand { get; set; }
        /// <summary>
        /// Command for navigating to the default view model
        /// </summary>
        public ICommand NavLogoutCommand { get; set; }

        #endregion

        #region Constructor
        public EmployeeDashboardViewModel()
        {
            #region Initilization of view models
            GenerateTicketsViewModel = new GenerateTicketsViewModel();
            ModifyCustomerDetailsViewModel = new ModifyCustomerDetailsViewModel();
            ModifyEmployeeDetailsViewModel = new ModifyEmployeeDetailsViewModel();
            DataEventsViewModel = new DataEventsViewModel();
            ModifyEventsViewModel = new ModifyEventsViewModel();
            SuccessfulTicketsViewModel = new SuccessfulTicketsViewModel();
            #endregion

            #region Command Parameters
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            NavLogoutCommand = new RelayCommand<NavigationViewModelBase>(Nav2);
            #endregion
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// Helper function that only is implemented
        /// to log users out.
        /// </summary>
        /// <param name="viewModel">The view model</param>
        private void Nav2(NavigationViewModelBase viewModel)
        {
            Properties.Settings.Default.Email = "guestuser@email.com";
            Navigate(viewModel);
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        #endregion
    }
}
