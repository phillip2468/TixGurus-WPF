using System.Collections.Generic;
using System.Linq;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using System.Windows.Input;
using Ssample.Model;
using Ssample.Properties;

namespace Ssample.ViewModel.Base_view_models
{
    public class SearchEventViewModel : NavigationViewModelBase
    {
        public ICommand NavigateTicketDetailsCommand { get; set; }
        public ICommand NavCommand { get; set; }
        public SearchEventViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            NavigateTicketDetailsCommand = new RelayCommand<NavigationViewModelBase>(Nav2);
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            Events = (from data in context.Event_Details select data).ToList();

            foreach (var eventss in Events)
            {
                var rowData = (from data in Events
                    where data.eventTitle == Settings.Default.SearchText.Trim()
                    select data).ToList();
                foreach (var eventDetails in rowData) MatchingEvent.Add(eventDetails);
            }

        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Properties.Settings.Default.SearchText = "";
            Navigate(viewModel);
        }

        private void Nav2(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        public List<Event_Details> Events { get; set; }
        public List<Event_Details> MatchingEvent { get; set; } = new List<Event_Details>();

        private string _textOfSearch;

        public string TextOfSearch
        {
            get { return _textOfSearch = Properties.Settings.Default.SearchText; }
            set { _textOfSearch = value; }
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

    }
}
