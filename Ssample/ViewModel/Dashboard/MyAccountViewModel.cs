using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using System.Windows.Input;

namespace Ssample.ViewModel.Dashboard
{
    /// <summary>
    /// A class which shows a user who has been logged in.
    /// Shows all the events as logged in.
    /// </summary>
    public class MyAccountViewModel : NavigationViewModelBase
    {
        #region Fields
        /// <summary>
        /// Navigation view model field
        /// </summary>
        private NavigationViewModelBase ChangeMyDetailsViewModel;

        #endregion

        #region Commands
        /// <summary>
        /// Navigation command
        /// </summary>
        public ICommand NavCommand { get; set; }
        /// <summary>
        /// Change details command
        /// </summary>
        public ICommand GoToChangeDetailsCommand { get; set; }
        /// <summary>
        /// Navigation to log out command
        /// </summary>
        public ICommand NavLogoutCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MyAccountViewModel()
        {
            ChangeMyDetailsViewModel = new ChangeMyDetailsViewModel();

            GoToChangeDetailsCommand = new RelayCommand(GoToChangeDetails);

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            NavLogoutCommand = new RelayCommand<NavigationViewModelBase>(Nav2);
        }

        #endregion

        #region Helper functions

        /// <summary>
        /// Navigate to view model
        /// </summary>
        private void GoToChangeDetails()
        {
            Navigate(ChangeMyDetailsViewModel);
        }

        /// <summary>
        /// Navigation helper function
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        /// <summary>
        /// This helper function logs the user out
        /// </summary>
        /// <param name="viewModel">ViewModel</param>
        private void Nav2(NavigationViewModelBase viewModel)
        {
            Properties.Settings.Default.Email = "guestuser@email.com";
            Navigate(viewModel);
        }
        #endregion
    }
}
