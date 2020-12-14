using Microsoft.Win32;
using Ssample.ViewModel.Dashboard;
using Syncfusion.UI.Xaml.Grid.Converter;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Ssample.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for ViewPastTicketsView.xaml
    /// </summary>
    public partial class ViewPastTicketsView : UserControl
    {
        public ViewPastTicketsView()
        {
            InitializeComponent();
            DataContext = new ViewPastTicketsViewModel();
        }

        private void Buttonforprintingtickets_OnClick(object sender, RoutedEventArgs e)
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

        private void ButtonPrintTransaction_OnClick(object sender, RoutedEventArgs e)
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
    }
}
