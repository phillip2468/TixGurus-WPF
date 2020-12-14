using Ssample.ViewModel.Buying_tickets;
using System.Windows;
using System.Windows.Controls;

namespace Ssample.Views.Buying_tickets
{
    /// <summary>
    /// Interaction logic for PurchaseTicketsView.xaml
    /// </summary>
    public partial class PurchaseTicketsView : UserControl
    {
        public PurchaseTicketsView()
        {
            InitializeComponent();
            DataContext = new PurchaseTicketsViewModel();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/au/home");
        }
    }
}
