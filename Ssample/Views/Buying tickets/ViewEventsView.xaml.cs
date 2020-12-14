using Ssample.ViewModel.Buying_tickets;
using Syncfusion.UI.Xaml.Grid;
using System.Windows;
using System.Windows.Controls;

namespace Ssample.Views.Buying_tickets
{
    /// <summary>
    /// Interaction logic for ViewEventsView.xaml
    /// </summary>
    public partial class ViewEventsView : UserControl
    {
        public ViewEventsView()
        {
            InitializeComponent();
            DataContext = new ViewEventsViewModel();
            DataGrid.AutoGeneratingColumn += SfDataGrid_OnAutoGeneratingColumn;
        }

        private void SfDataGrid_OnAutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "eventStart" || e.Column.MappingName == "eventEnd")
                ((e.Column) as GridDateTimeColumn).Pattern = Syncfusion.Windows.Shared.DateTimePattern.FullDateTime;
            if (e.Column.MappingName == "ticketId")
                e.Cancel = true;
        }
    }
}
