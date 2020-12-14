using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.ViewModel.Buying_tickets;
using Ssample.ViewModel.Register;
using Ssample.ViewModel.Signing_In;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Ssample.ViewModel.Base_view_models
{
    /// <summary>
    /// The default view model; the start up page
    /// </summary>
    public class DefaultViewModel : NavigationViewModelBase
    {
        #region Fields
        /// <summary>
        /// Field for register view model
        /// </summary>
        private NavigationViewModelBase registerViewModel;

        /// <summary>
        /// Field for sign in view model
        /// </summary>
        private NavigationViewModelBase signInViewModel;

        /// <summary>
        /// Field for view ticket details view model
        /// </summary>
        private NavigationViewModelBase viewEventsViewModel;

        /// <summary>
        /// Field for searching events view model
        /// </summary>
        private NavigationViewModelBase searchEventViewModel;
        #endregion

        #region Commands
        /// <summary>
        /// Command for navigating between pages
        /// (user controls)
        /// </summary>
        public ICommand NavCommand { get; set; }

        /// <summary>
        /// Command for navigating to the
        /// search events page
        /// </summary>
        public ICommand NavigateSearchCommand { get; set; }

        /// <summary>
        /// Command for navigating to the respective
        /// ticket details page
        /// </summary>
        public ICommand NavigateTicketDetailsCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the class containing initialization, declaration
        /// and commands for the class
        /// </summary>
        public DefaultViewModel()
        {
            #region Initialization of user controls

            registerViewModel = new RegisterViewModel();
            viewEventsViewModel = new ViewEventsViewModel();
            signInViewModel = new SignInViewModel();
            searchEventViewModel = new SearchEventViewModel();

            #endregion

            #region Declaration of the events list for the tile views

            //Using db context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Create a list of events
            Events = (from data in context.Event_Details select data).ToList();

            #endregion

            #region Commands arguments

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            NavigateSearchCommand = new RelayCommand<NavigationViewModelBase>(Nav2);
            NavigateTicketDetailsCommand = new RelayCommand<NavigationViewModelBase>(Nav3);

            #endregion

        }

        #endregion

        #region Helper Functions

        public List<Event_Details> Events { get; set; }

        //Navigates to the register view
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        /// <summary>
        /// Helper function for navigating
        /// to the search events page
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav2(NavigationViewModelBase viewModel)
        {

            Properties.Settings.Default.SearchText = SearchText;
            if (string.IsNullOrEmpty(SearchText))
            {
                MessageBox.Show("You need to type something");
            }
            else
            {
                Properties.Settings.Default.SearchText = SearchText.Trim();
                Navigate(viewModel);
            }
        }

        private void Nav3(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Search text
        /// </summary>
        private string _searchText;

        /// <summary>
        /// Property for getting the
        /// search text from the
        /// text box
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged($"SearchText");
            }
        }

        private Event_Details _selectedItem;
        /// <summary>
        /// Property for getting the selected item
        /// </summary>
        public Event_Details SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                //Set the property to the selected item
                Properties.Settings.Default.EventTitle = _selectedItem.eventTitle;
                OnPropertyChanged($"SelectedItem");
            }
        }

        #endregion

    }
    /// <summary>
    /// A class responsible for converting bytes to images
    /// </summary>
    public class ByteToImageConverter : IValueConverter
    {

        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            img.StreamSource = new MemoryStream(imageByteArray);
            return img;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageByteArray = value as byte[];
            if (imageByteArray == null) return null;
            return ConvertByteArrayToBitMapImage(imageByteArray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

