using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using System.Windows.Input;

namespace Ssample.ViewModel.Signing_In
{
    /// <summary>
    /// View model when successfully signed into account
    /// </summary>
    public class SuccessfulSignInViewModel : NavigationViewModelBase
    {
        #region Fields
        //Field for home page
        private NavigationViewModelBase HomePageSignedInViewModel;
        #endregion

        #region Commands
        /// <summary>
        /// A command for the button to go to the home
        /// page
        /// </summary>
        public ICommand GoToHomePage { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for sign in
        /// </summary>
        public SuccessfulSignInViewModel()
        {
            //Initialization of home page view model
            HomePageSignedInViewModel = new HomePageSignedInViewModel();

            //Command for going to home page
            GoToHomePage = new RelayCommand(GoToHomePageView);
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// A helper class which goes to home page signed in view model
        /// </summary>
        private void GoToHomePageView()
        {
            Navigate(HomePageSignedInViewModel);
        }
        #endregion

    }
}
