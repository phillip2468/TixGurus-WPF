using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Event_Management
{
    /// <summary>
    /// Generates tickets based on the events
    /// </summary>
    public class GenerateTicketsViewModel : NavigationViewModelBase
    {
        #region Fields
        /// <summary>
        /// Field for navigating to successful tickets page
        /// </summary>
        private NavigationViewModelBase successfulTicketsViewModel;

        #endregion

        #region Commands
        /// <summary>
        /// Command to go to the successful generation
        /// of tickets view model
        /// </summary>
        public ICommand GoToSuccessfulTicketsCommand { get; set; }

        /// <summary>
        /// Command to go back to the previous page
        /// </summary>
        public ICommand NavigateBackCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the class
        /// </summary>
        public GenerateTicketsViewModel()
        {
            //Initializes the view model for successful generation of tickets
            successfulTicketsViewModel = new SuccessfulTicketsViewModel();

            #region Event list
            //Setting the db context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            //Generate a list using the context
            EventList = (from data in context.Event_Details select data).ToList();

            #endregion

            #region Command parameters

            GoToSuccessfulTicketsCommand = new RelayCommand(NavigateToSuccessfulTickets);
            NavigateBackCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            #endregion
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// A function to navigate to the successful
        /// view model if all fields are correct
        /// </summary>
        private void NavigateToSuccessfulTickets()
        {
            if (SaveChanges())
            {
                Navigate(successfulTicketsViewModel);
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }

        /// <summary>
        /// A function which navigates back to the dashboard
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Used for modifying tickets
        /// </summary>
        public Ticket_Details CurrentTicket = new Ticket_Details();

        #region Level One Properties

        /// <summary>
        /// Property for the event title
        /// </summary>
        public string EventTitle
        {
            get
            {
                if (CurrentTicket.eventTitle == null)
                {
                    return CurrentTicket.eventTitle;
                }

                return CurrentTicket.eventTitle;
            }
            set
            {
                CurrentTicket.eventTitle = value;
                OnPropertyChanged($"EventTitle");
            }
        }

        /// <summary>
        /// Property for the level one tickets string letter
        /// </summary>
        private string _seatLetter;
        /// <summary>
        /// Property for level one string letter
        /// </summary>
        public string LevelOneChar
        {
            get
            {
                if (_seatLetter == null)
                {
                    return _seatLetter;
                }

                return _seatLetter;
            }
            set
            {
                _seatLetter = value;
                OnPropertyChanged($"LevelOneChar");
            }
        }

        /// <summary>
        /// Property for the price of level one tickets
        /// </summary>
        public decimal LevelOnePrice
        {
            get
            {
                if (CurrentTicket.price == 0)
                {
                    return CurrentTicket.price;
                }

                return CurrentTicket.price;
            }
            set
            {
                CurrentTicket.price = value;
                OnPropertyChanged($"LevelOnePrice");
            }
        }

        private int _levelOneCapacity;
        /// <summary>
        /// Gets the amount of tickets
        /// that is to be generated
        /// </summary>
        public int LevelOneCapacity
        {
            get
            {
                if (_levelOneCapacity == 0)
                {
                    return _levelOneCapacity;
                }

                return _levelOneCapacity;
            }
            set
            {
                _levelOneCapacity = value;
                OnPropertyChanged($"LevelOneCapacity");
            }
        }
        /// <summary>
        /// Property for the start time of level one events
        /// </summary>
        public DateTime EventStartLevelOne
        {
            get
            {
                if (CurrentTicket.eventStart == DateTime.MinValue)
                {
                    return CurrentTicket.eventStart;
                }

                return CurrentTicket.eventStart;
            }
            set
            {
                CurrentTicket.eventStart = value;
                OnPropertyChanged($"EventStart");
            }
        }

        /// <summary>
        /// Property for the end time of level one events
        /// </summary>
        public DateTime EventEndLevelOne
        {
            get
            {
                if (CurrentTicket.eventEnd == DateTime.MinValue)
                {
                    return CurrentTicket.eventEnd;
                }

                return CurrentTicket.eventEnd;
            }
            set
            {
                CurrentTicket.eventEnd = value;
                OnPropertyChanged($"EventEnd");
            }
        }

        #endregion

        #region Level Two properties

        private string _LevelTwoChar;
        /// <summary>
        /// Property for level two string letter
        /// </summary>
        public string LevelTwoChar
        {
            get
            {
                if (_LevelTwoChar == null)
                {
                    return _LevelTwoChar;
                }

                return _LevelTwoChar;
            }
            set
            {
                _LevelTwoChar = value;
                OnPropertyChanged($"LevelTwoChar");
            }
        }

        private decimal _levelTwoPrice;
        /// <summary>
        /// Property for level two pricing
        /// </summary>
        public decimal LevelTwoPrice
        {
            get
            {
                if (_levelTwoPrice == 0)
                {
                    return _levelTwoPrice;
                }

                return _levelTwoPrice;
            }
            set
            {
                _levelTwoPrice = value;
                OnPropertyChanged($"LevelTwoPrice");
            }
        }

        /// <summary>
        /// Property for level two amount of tickets
        /// </summary>
        private int _levelTwoCapacity;
        /// <summary>
        /// Gets the amount of tickets
        /// </summary>
        public int LevelTwoCapacity
        {
            get
            {
                if (_levelTwoCapacity == 0)
                {
                    return _levelTwoCapacity;
                }

                return _levelTwoCapacity;
            }
            set
            {
                _levelTwoCapacity = value;
                OnPropertyChanged($"LevelTwoCapacity");
            }
        }
        #endregion

        #region Level Three Properties
        private string _levelThreeChar;
        /// <summary>
        /// Property for level three tickets string letter
        /// </summary>
        public string LevelThreeChar
        {
            get
            {
                if (_levelThreeChar == null)
                {
                    return _levelThreeChar;
                }

                return _levelThreeChar;
            }
            set
            {
                _levelThreeChar = value;
                OnPropertyChanged($"LevelThreeChar");
            }
        }

        private decimal _levelThreePrice;
        /// <summary>
        /// Property for the price of level three tickets
        /// </summary>
        public decimal LevelThreePrice
        {
            get
            {
                if (_levelThreePrice == 0)
                {
                    return _levelTwoPrice;
                }

                return _levelThreePrice;
            }
            set
            {
                _levelThreePrice = value;
                OnPropertyChanged($"LevelThreePrice");
            }
        }

        private int _levelThreeCapacity;
        /// <summary>
        /// Gets the amount of tickets to be generated for level 3
        /// </summary>
        public int LevelThreeCapacity
        {
            get
            {
                if (_levelTwoCapacity == 0)
                {
                    return _levelThreeCapacity;
                }

                return _levelThreeCapacity;
            }
            set
            {
                _levelThreeCapacity = value;
                OnPropertyChanged($"LevelThreeCapacity");
            }
        }

        #endregion

        #endregion

        #region Save changes to database
        /// <summary>
        /// A function which returns a boolean
        /// value if the generation of tickets
        /// is correct
        /// </summary>
        /// <returns>Boolean value</returns>
        private bool SaveChanges()
        {
            //Gets a value for the number of tickets
            int numberOfTickets = LevelOneCapacity;

            //Set the initial value to false
            bool output = false;

            //Loop for generating tickets
            for (int i = 1; i < numberOfTickets + 1; i++)
            {
                //Based on the context
                CustomerDatabaseEntities context = new CustomerDatabaseEntities();

                #region Adding tickets to database
                CurrentTicket.eventTitle = EventTitle;
                CurrentTicket.price = LevelOnePrice;
                CurrentTicket.seatLocation = LevelOneChar + i;
                CurrentTicket.eventStart = EventStartLevelOne;
                CurrentTicket.eventEnd = EventEndLevelOne;
                context.Ticket_Details.Add(CurrentTicket);
                context.SaveChanges();
                context.Dispose();
                #endregion
            }

            //Assigning a value to level 2
            string levelTwoChar = LevelTwoChar;
            decimal levelTwoPrice = LevelTwoPrice;
            int levelTwoCapacity = LevelTwoCapacity;

            //Checks if any of these values are zero. If not continue however return true (since level one tickets succeeded )
            if (levelTwoChar == null || levelTwoPrice == 0 || levelTwoCapacity == 0)
            {
                return true;
            }

            //If the level two capacity is above zero (ie there are tickets to generate)
            if (levelTwoCapacity > 0)
            {
                //Loop for generating level 2 tickets
                for (int i = 1; i < LevelTwoCapacity + 1; i++)
                {
                    //Based on the database context
                    CustomerDatabaseEntities context = new CustomerDatabaseEntities();

                    #region Adding tickets and saving to database
                    CurrentTicket.eventTitle = EventTitle;
                    CurrentTicket.price = LevelTwoPrice;
                    CurrentTicket.seatLocation = LevelTwoChar + i;
                    CurrentTicket.eventStart = EventStartLevelOne;
                    CurrentTicket.eventEnd = EventEndLevelOne;
                    context.Ticket_Details.Add(CurrentTicket);
                    context.SaveChanges();
                    context.Dispose();
                    #endregion
                }
            }

            //Assigning values to level 3
            string levelThreeChar = LevelThreeChar;
            int levelThreeCapacity = LevelThreeCapacity;
            decimal levelThreePrice = LevelThreePrice;

            //Checks if any of these values are zero. If not continue however return true (since level two tickets succeeded )
            if (levelThreeChar == null || levelThreePrice == 0 || levelThreeCapacity == 0)
            {
                return true;
            }

            //If the level three capacity is above zero (ie there are tickets to generate)
            if (levelThreeCapacity > 0)
            {
                //Loop for generating tickets
                for (int i = 1; i < levelThreeCapacity + 1; i++)
                {
                    //Assign the database context
                    CustomerDatabaseEntities context = new CustomerDatabaseEntities();
                    #region Saving to database
                    CurrentTicket.eventTitle = EventTitle;
                    CurrentTicket.price = LevelThreePrice;
                    CurrentTicket.seatLocation = LevelThreeChar + i;
                    CurrentTicket.eventStart = EventStartLevelOne;
                    CurrentTicket.eventEnd = EventEndLevelOne;
                    context.Ticket_Details.Add(CurrentTicket);
                    context.SaveChanges();
                    context.Dispose();
                    #endregion
                }
            }
            //Return true
            output = true;

            return output;
        }

        #endregion

        #region For datagrid generation
        /// <summary>
        /// List for the event details
        /// </summary>
        private List<Event_Details> Event { get; set; }

        /// <summary>
        /// Property for the event
        /// </summary>
        public List<Event_Details> EventList
        {
            get => Event;
            set => Event = value;
        }

        #endregion

    }

}
