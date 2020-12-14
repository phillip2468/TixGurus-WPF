using Microsoft.Win32;
using Ssample.ViewModel;
using Syncfusion.UI.Xaml.Grid.Converter;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Ssample.Views.Sign_In
{
    /// <summary>
    /// Interaction logic for DataEventsView.xaml
    /// </summary>
    public partial class DataEventsView : UserControl
    {
        public DataEventsView()
        {
            InitializeComponent();
            DataContext = new DataEventsViewModel();
        }

        private void ButtonPrintGuestTickets_OnClick(object sender, RoutedEventArgs e)
        {
            var document = DataGridGuestTickets.ExportToPdf();
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

        private void ButtonPrintCustomerTicket_OnClick(object sender, RoutedEventArgs e)
        {
            var document = DataGridCustomer.ExportToPdf();
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
