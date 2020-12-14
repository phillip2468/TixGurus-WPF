using Ssample.ViewModel.zEmployees.Event_Management;
using System.Windows;
using System.Windows.Controls;

namespace Ssample.Views.zEmployees.Event_Managment
{
    /// <summary>
    /// Interaction logic for AddEventsView.xaml
    /// </summary>
    public partial class AddEventsView : UserControl
    {
        public AddEventsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setting the data context
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEventsView_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AddEventsViewModel();
        }
    }
}
