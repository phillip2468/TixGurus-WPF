using Ssample.ViewModel.Buying_tickets;
using System.Windows;
using System.Windows.Controls;

namespace Ssample.Views.Buying_tickets
{
    /// <summary>
    /// Interaction logic for CustomerPurchaseTicketView.xaml
    /// </summary>
    public partial class CustomerPurchaseTicketView : UserControl
    {
        public CustomerPurchaseTicketView()
        {
            InitializeComponent();
            DataContext = new CustomerPurchaseTicketViewModel();
        }

        private void PaypalButton_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/au/home");
        }
    }
}
