using Ssample.Model;
using Ssample.ViewModel.zEmployees.Event_Management;
using Syncfusion.UI.Xaml.Grid;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace Ssample.Views.zEmployees.Event_Managment
{
    /// <summary>
    /// Interaction logic for SuccessfulTicketsView.xaml
    /// </summary>
    public partial class SuccessfulTicketsView : UserControl
    {
        /// <summary>
        /// Sets the context
        /// </summary>
        CustomerDatabaseEntities context = new CustomerDatabaseEntities();
        public SuccessfulTicketsView()
        {
            DataContext = new SuccessfulTicketsViewModel();
            InitializeComponent();
            this.DataGrid.RowValidated += dataGrid_RowValidated;
            this.DataGrid.RecordDeleting += dataGrid_RecordDeleting;
            this.DataGrid.AutoGeneratingColumn += datagrid_AutoGeneratingColumn;
        }

        /// <summary>
        /// Adds record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RowValidated(object sender, Syncfusion.UI.Xaml.Grid.RowValidatedEventArgs args)
        {
            Ticket_Details newRecord = args.RowData as Ticket_Details;
            Ticket_Details order = context.Ticket_Details.First(i => i.ticketId == newRecord.ticketId);

            order.ticketId = newRecord.ticketId;
            order.eventTitle = newRecord.eventTitle;
            order.eventStart = newRecord.eventStart;
            order.eventEnd = newRecord.eventEnd;
            order.price = newRecord.price;
            order.seatLocation = newRecord.seatLocation;
            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RecordDeleting(object sender, RecordDeletingEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            var item = args.Items[0] as Ticket_Details;
            var id = item.ticketId;
            var title = item.eventTitle;
            var start = item.eventStart;
            var end = item.eventEnd;
            var price = item.price;
            var seatLocation = item.seatLocation;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }

        public void datagrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "EventStart" || e.Column.MappingName == "EventEnd")
            {
                // Setting default date and time format for Event Start and Event End column
                ((e.Column) as GridDateTimeColumn).Pattern = Syncfusion.Windows.Shared.DateTimePattern.FullDateTime;
            }
        }
    }
}
