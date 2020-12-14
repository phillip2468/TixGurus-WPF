using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.Properties;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Ssample.ViewModel.Buying_tickets
{
    /// <summary>
    /// A class responsible for viewing the details about an event
    /// </summary>
    public class ViewEventsViewModel : NavigationViewModelBase
    {
        #region Fields
        /// <summary>
        /// Buying tickets view model declaration
        /// </summary>
        private NavigationViewModelBase buyingTicketsViewModel;

        #endregion

        #region Commands
        /// <summary>
        /// Navigation command responsible for
        /// moving back to the default view
        /// </summary>
        public ICommand NavCommand { get; set; }

        /// <summary>
        /// Navigation command responsible for moving
        /// to the purchasing of tickets view
        /// </summary>
        public ICommand NavCommand2 { get; set; }

        #endregion

        #region Constructor

        public ViewEventsViewModel()
        {
            //Initialization of the next view model
            buyingTicketsViewModel = new PurchaseTicketsViewModel();

            //Setting the data context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Setting the navigation command parameters
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            NavCommand2 = new RelayCommand<NavigationViewModelBase>(Nav2);

            //Provide an item source for the data grid
            Tickets = (from data in context.Ticket_Details where data.eventTitle == Settings.Default.EventTitle select data).ToList();

        }

        #endregion

        #region Helper functions

        /// <summary>
        /// Navigates back to the previous page (the default view model)
        /// </summary>
        /// <param name="viewModel">The view model</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Settings.Default.SeatLocation = "";
            Properties.Settings.Default.SearchText = "";
            Navigate(viewModel);
        }

        /// <summary>
        /// Navigates to the purchasing of tickets page
        /// </summary>
        /// <param name="viewModel">The viewmodel</param>
        private void Nav2(NavigationViewModelBase viewModel)
        {
            if (SaveSeatSelection() != true)
            {
                MessageBox.Show("Not correct seats");
            }
            else
            {
                Navigate(viewModel);
            }
        }

        #endregion

        #region Properties for lists
        /// <summary>
        /// Property for tickets
        /// </summary>
        public List<Ticket_Details> Tickets { get; set; }

        /// <summary>
        /// Property for events
        /// </summary>
        public List<Event_Details> Event { get; set; }

        private List<Ticket_Details> tickets = new List<Ticket_Details>();

        #endregion

        #region Properties

        private BitmapImage _image;
        /// <summary>
        /// Loads the image from the view model
        /// </summary>
        public BitmapImage Image
        {
            get
            {
                _image = LoadImage(LoadImage());
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        private BitmapImage _imageOfLocation;
        /// <summary>
        /// Loads the image from the view model
        /// </summary>
        public BitmapImage ImageOfLocation
        {
            get
            {
                _image = LoadImage(LoadImageOfLocation());
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged($"ImageOfLocation");
            }
        }

        /// <summary>
        /// Function which converts imageData to
        /// an actual image
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        public BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        /// <summary>
        /// A function which loads the image
        /// of the layout of the location
        /// </summary>
        /// <returns></returns>
        public byte[] LoadImage()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Event = (from data in context.Event_Details select data).ToList();
            var selectedEvent = Event.Find(x => x.eventTitle == Settings.Default.EventTitle);
            byte[] selectedImage = selectedEvent.eventLayout;
            return selectedImage;
        }

        /// <summary>
        /// A function which loads the image
        /// of the layout of the location
        /// </summary>
        /// <returns></returns>
        public byte[] LoadImageOfLocation()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Event = (from data in context.Event_Details select data).ToList();
            var selectedEvent = Event.Find(x => x.eventTitle == Settings.Default.EventTitle);
            byte[] selectedImage = selectedEvent.eventPicture;
            return selectedImage;
        }

        private string _seatPlace;
        public string SeatPlace
        {
            get
            {
                if (_seatPlace == null)
                {
                    return _seatPlace;
                }
                return _seatPlace;
            }
            set
            {
                _seatPlace = value;
                OnPropertyChanged($"Seat");
            }
        }

        #endregion

        #region Save to database

        /// <summary>
        /// A function which returns a true or false if the typed in
        /// seat is correct
        /// </summary>
        /// <returns></returns>
        private bool SaveSeatSelection()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Tickets = (from data in context.Ticket_Details select data).ToList();
            var selectedEvent = tickets.Find(x => x.eventTitle == Settings.Default.EventTitle);

            if (SeatPlace == null)
            {
                return false;
            }

            else
            {
                Settings.Default.SeatLocation = SeatPlace.Trim();
                return true;
            }

        }

        #endregion

        #region Misc
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { OnPropertyChanged(ref _isSelected, value); }
        }
        #endregion

    }


}
