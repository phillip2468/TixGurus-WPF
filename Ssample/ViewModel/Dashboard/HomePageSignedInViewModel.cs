using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.ViewModel.Dashboard;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel
{
    /// <summary>
    /// This class represents
    /// a user who has signed in
    /// and wants to view their dashboard
    /// </summary>
    public class HomePageSignedInViewModel : NavigationViewModelBase
    {
        #region Fields
        private NavigationViewModelBase _myAccountView;
        #endregion

        #region Commands
        public ICommand NavLogoutCommand { get; set; }
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor

        public HomePageSignedInViewModel()
        {
            _myAccountView = new MyAccountViewModel();

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav2);
            NavLogoutCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            #region Declaration of the events list for the tile views

            //Using db context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Create a list of events
            Events = (from data in context.Event_Details select data).ToList();

            #endregion
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// Helper function to go to log out user
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Properties.Settings.Default.Email = "guestuser@email.com";
            Navigate(viewModel);
        }

        /// <summary>
        /// Helper function to navigate to other pages
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav2(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        #endregion


        #region List properties
        /// <summary>
        /// List property
        /// </summary>
        public List<Event_Details> Events { get; set; }

        #endregion


    }
}
