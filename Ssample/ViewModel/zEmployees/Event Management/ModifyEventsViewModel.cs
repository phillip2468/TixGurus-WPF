using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Event_Management
{
    /// <summary>
    /// A view model responsible for modifying details
    /// about the data grid
    /// </summary>
    public class ModifyEventsViewModel : NavigationViewModelBase
    {
        #region Fields
        /// <summary>
        /// A command to navigate to the desired page
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for the page
        /// </summary>
        public ModifyEventsViewModel()
        {
            //Set the data base context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Navigate to the respective page
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            //Generate a list of events from context
            EventList = (from data in context.Event_Details select data).ToList();
        }

        #endregion

        #region Helper function
        /// <summary>
        /// A helper function that navigates to the employee dashboard
        /// </summary>
        /// <param name="viewModel">The view model</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        #endregion

        #region For the datagrid
        /// <summary>
        /// Gets a list
        /// </summary>
        private List<Event_Details> Event { get; set; }

        /// <summary>
        /// Property for list
        /// </summary>
        public List<Event_Details> EventList
        {
            get => Event;
            set => Event = value;
        }

        #endregion
    }
}
