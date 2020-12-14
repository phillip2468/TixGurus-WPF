using Microsoft.Win32;
using Ssample.ViewModel.Buying_tickets;
using Syncfusion.UI.Xaml.Grid.Converter;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Ssample.Views.Buying_tickets
{
    /// <summary>
    /// Interaction logic for SuccessfulCustomerTicketPurchaseView.xaml
    /// </summary>
    public partial class SuccessfulCustomerTicketPurchaseView : UserControl
    {
        public SuccessfulCustomerTicketPurchaseView()
        {
            InitializeComponent();
            DataContext = new SuccessfulCustomerTicketPurchaseViewModel();
        }
        /// <summary>
        /// Prints the ticket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnForPrintingTickets_OnClick(object sender, RoutedEventArgs e)
        {
            var document = DataGridTransactions.ExportToPdf();
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files(*.pdf)|*.pdf"
            };

            if (sfd.ShowDialog() == true)
            {
                using (Stream stream = sfd.OpenFile())
                {
                    document.Save(stream);
                }

                //Message box confirmation to view the created Pdf file.

                if (MessageBox.Show("Do you want to view the Pdf file?", "Pdf file has been created",
                        MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {

                    //Launching the Pdf file using the default Application.
                    Process.Start(sfd.FileName);
                }
            }
        }
        /// <summary>
        /// Prints the transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrintTransaction_OnClick(object sender, RoutedEventArgs e)
        {
            var document = DataGridTickets.ExportToPdf();
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files(*.pdf)|*.pdf"
            };

            if (sfd.ShowDialog() == true)
            {
                using (Stream stream = sfd.OpenFile())
                {
                    document.Save(stream);
                }

                //Message box confirmation to view the created Pdf file.

                if (MessageBox.Show("Do you want to view the Pdf file?", "Pdf file has been created",
                        MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {

                    //Launching the Pdf file using the default Application.
                    Process.Start(sfd.FileName);
                }
            }
        }
    }
}
