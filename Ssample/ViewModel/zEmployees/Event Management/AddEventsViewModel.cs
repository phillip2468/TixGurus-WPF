using Microsoft.Win32;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Event_Management
{
    /// <summary>
    /// A class which adds events to the database
    /// with their respective details
    /// </summary>
    public class AddEventsViewModel : NavigationViewModelBase
    {
        #region Commands
        /// <summary>
        /// Command for navigating backwards
        /// </summary>
        public ICommand NavigateBackCommand { get; set; }

        /// <summary>
        /// Command for generating and events
        /// </summary>
        public ICommand NavigateToSuccessfulCommand { get; set; }

        /// <summary>
        /// Command for loading image of the event picture
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public ICommand _LoadImageCommand;

        /// <summary>
        /// Command for loading image of the event layour
        /// </summary>
        public ICommand _LoadImageOfLayourCommand;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the class
        /// </summary>
        public AddEventsViewModel()
        {
            #region Commands arguments
            NavigateBackCommand = new RelayCommand<NavigationViewModelBase>(NavigateBack);
            NavigateToSuccessfulCommand = new RelayCommand<NavigationViewModelBase>(NavigateToGenerate);
            #endregion
        }

        #endregion

        #region Hepler functions
        /// <summary>
        /// Function which generates tickets if successful,
        /// otherwise shows a message box indicating something went wrong
        /// </summary>
        /// <param name="viewModel">The view model</param>
        private void NavigateToGenerate(NavigationViewModelBase viewModel)
        {
            if (GoToSuccessful())
            {
                MessageBox.Show("The event has been added successfully");
                Navigate(viewModel);
            }
            else
            {
                MessageBox.Show("You entered something wrong");
            }
        }
        /// <summary>
        /// Function which navigates back to the previous page
        /// according to which viewmodel 
        /// </summary>
        /// <param name="viewModel">The view model</param>
        private void NavigateBack(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        /// <summary>
        /// Determines if event has been
        /// added successfully
        /// </summary>
        /// <returns></returns>
        private bool GoToSuccessful()
        {
            if (SaveCustomerChanges() != true)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converts string path into a byte array
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public byte[] ConvertImageToByteArray(string imagePath)
        {
            byte[] imageByteArray = null;
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                imageByteArray = new byte[reader.BaseStream.Length];
                for (int i = 0; i < reader.BaseStream.Length; i++)
                {
                    imageByteArray[i] = reader.ReadByte();
                }

                return imageByteArray;
            }
        }

        #endregion

        #region Current Event list
        /// <summary>
        /// Edits the current event
        /// </summary>
        public Event_Details CurrentEvent { get; set; } = new Event_Details();

        #endregion

        #region Properties
        /// <summary>
        /// Property for event title
        /// </summary>
        public string EventTitle
        {
            get
            {
                //If null
                if (CurrentEvent.eventTitle == null)
                {
                    return CurrentEvent.eventTitle;
                }

                return CurrentEvent.eventTitle;
            }
            set
            {
                CurrentEvent.eventTitle = value;
                OnPropertyChanged($"EventTitle");
            }
        }

        /// <summary>
        /// Property for event description
        /// </summary>
        public string EventDescription
        {
            get
            {
                if (CurrentEvent.eventDescription == null)
                {
                    return CurrentEvent.eventDescription;
                }

                return CurrentEvent.eventDescription;
            }
            set
            {
                CurrentEvent.eventDescription = value;
                OnPropertyChanged($"EventDescription");
            }
        }

        /// <summary>
        /// Property for event start
        /// </summary>
        public DateTime EventStart
        {
            get
            {
                if (CurrentEvent.eventStart == DateTime.MinValue)
                {
                    return CurrentEvent.eventStart;
                }

                return CurrentEvent.eventStart;
            }
            set
            {
                CurrentEvent.eventStart = value;
                OnPropertyChanged($"EventStart");
            }
        }
        /// <summary>
        /// Property for event end
        /// </summary>
        public DateTime EventEnd
        {
            get
            {
                if (CurrentEvent.eventEnd == DateTime.MinValue)
                {
                    return CurrentEvent.eventEnd;
                }

                return CurrentEvent.eventEnd;
            }
            set
            {
                CurrentEvent.eventEnd = value;
                OnPropertyChanged($"EventEnd");
            }
        }

        /// <summary>
        /// Property for capacity
        /// </summary>
        public int Capacity
        {
            get
            {
                if (CurrentEvent.capacity == 0)
                {
                    return CurrentEvent.capacity;
                }

                return CurrentEvent.capacity;
            }
            set
            {
                CurrentEvent.capacity = value;
                OnPropertyChanged($"Capacity");
            }
        }

        /// <summary>
        /// Property for event location
        /// </summary>
        public string EventLocation
        {
            get
            {
                if (CurrentEvent.eventLocation == null)
                {
                    return CurrentEvent.eventLocation;
                }

                return CurrentEvent.eventLocation;
            }
            set
            {
                CurrentEvent.eventLocation = value;
                OnPropertyChanged($"EventLocation");
            }
        }

        /// <summary>
        /// Property for event address
        /// </summary>
        public string EventAddress
        {
            get
            {
                if (CurrentEvent.eventAddress == null)
                {
                    return CurrentEvent.eventAddress;
                }

                return CurrentEvent.eventAddress;
            }
            set
            {
                CurrentEvent.eventAddress = value;
                OnPropertyChanged($"EventAddress");
            }
        }

        /// <summary>
        /// Property for event genre
        /// </summary>
        public string EventGenre
        {
            get
            {
                if (CurrentEvent.eventGenre == null)
                {
                    return CurrentEvent.eventGenre;
                }

                return CurrentEvent.eventGenre;
            }
            set
            {
                CurrentEvent.eventGenre = value;
                OnPropertyChanged($"EventGenre");
            }
        }

        private bool? _showOnHomePage;

        /// <summary>
        /// Property to determine whether the event shows on the home page
        /// </summary>
        public bool? ShowOnHomePage
        {
            get { return _showOnHomePage; }
            set
            {
                _showOnHomePage = value;
                _showOnHomePage = (_showOnHomePage != null) ? value : false;
                OnPropertyChanged($"ShowOnHomePage");
            }
        }

        #region Images properties
        /// <summary>
        /// Command to open the file dialog, which is used
        /// to open the event image
        /// </summary>
        public ICommand LoadEventImageCommand
        {
            get
            {
                if (_LoadImageCommand == null)
                {
                    _LoadImageCommand = new RelayCommand<NavigationViewModelBase>(param => LoadImageOfEvent());
                }
                return _LoadImageCommand;
            }
        }

        /// <summary>
        /// Command to open the file dialog, which is used
        /// to open the event layour
        /// </summary>
        public ICommand LoadImageOfLayoutCommand
        {
            get
            {
                if (_LoadImageOfLayourCommand == null)
                {
                    _LoadImageOfLayourCommand = new RelayCommand<NavigationViewModelBase>(param => LoadImageOfEventLayout());
                }
                return _LoadImageOfLayourCommand;
            }
        }

        private string _imagePathOfEventImage;
        /// <summary>
        /// Property for getting the path of the image
        /// </summary>
        public string ImagePathOfEventImage
        {
            get { return _imagePathOfEventImage; }
            set
            {
                _imagePathOfEventImage = value;
                OnPropertyChanged($"ImagePathOfEventImage");
            }
        }

        private string _imagePathOfEventLayout;
        /// <summary>
        /// Property for getting the path of the image
        /// of the layout
        /// </summary>
        public string ImagePathOfEventLayout
        {
            get { return _imagePathOfEventLayout; }
            set
            {
                _imagePathOfEventLayout = value;
                OnPropertyChanged($"ImagePathOfEventLayout");
            }
        }

        #region File dialog functions

        /// <summary>
        /// Helper function which opens the file
        /// dialog function
        /// </summary>
        private void LoadImageOfEvent()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
                ImagePathOfEventImage = open.FileName;
        }

        private void LoadImageOfEventLayout()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
                ImagePathOfEventLayout = open.FileName;
        }
        #endregion

        #endregion

        #endregion

        #region Save to database

        /// <summary>
        /// A function which attempts to save values to the database
        /// </summary>
        /// <returns>Boolean values</returns>
        private bool SaveCustomerChanges()
        {
            //Initialization of bool
            bool output = false;

            //Assigns the value of the helper class to the class
            bool isNull = ArePropertiesNotNull(CurrentEvent);

            //Assign a context value
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //If the check for not null is not true
            if (!isNull)
            {
                //Using the helper function which converts the image to bytes
                byte[] imageBytes = ConvertImageToByteArray(ImagePathOfEventImage);
                byte[] imageBytes2 = ConvertImageToByteArray(ImagePathOfEventLayout);

                #region Saving to the database

                CurrentEvent.eventPicture = imageBytes;
                CurrentEvent.eventLayout = imageBytes2;
                CurrentEvent.eventGenre = EventGenre.Trim();
                CurrentEvent.eventTitle = EventTitle.Trim();
                CurrentEvent.showOnHomePage = ShowOnHomePage.Value;
                CurrentEvent.eventDescription = EventDescription.Trim();
                CurrentEvent.eventStart = EventStart;
                CurrentEvent.eventEnd = EventEnd;
                CurrentEvent.capacity = Capacity;
                CurrentEvent.eventLocation = EventLocation.Trim();
                CurrentEvent.eventAddress = EventAddress.Trim();
                context.Event_Details.Add(CurrentEvent);

                //Save changes to the database
                context.SaveChanges();
                context.Dispose();

                #endregion

                //Return true if this function succeeds
                output = true;
                return output;
            }

            //Otherwise return false
            return output;
        }
        #endregion

        #region Checking for null
        /// <summary>
        /// A function which checks whether the select object is
        /// null or not null
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">The object itself</param>
        /// <returns>A boolean value</returns>
        public bool ArePropertiesNotNull<T>(T obj)
        {
            return typeof(T).GetProperties().All(propertyInfo => propertyInfo.GetValue(obj) != null);
        }

        #endregion
    }
}

