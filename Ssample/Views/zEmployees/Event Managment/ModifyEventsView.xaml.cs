using Ssample.Model;
using Ssample.ViewModel.zEmployees.Event_Management;
using Syncfusion.UI.Xaml.Grid;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace Ssample.Views.zEmployees.Event_Managment
{
    /// <summary>
    /// Interaction logic for ModifyEventsView.xaml
    /// </summary>
    public partial class ModifyEventsView : UserControl
    {
        /// <summary>
        /// Sets the context
        /// </summary>
        CustomerDatabaseEntities context = new CustomerDatabaseEntities();
        public ModifyEventsView()
        {
            DataContext = new ModifyEventsViewModel();
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
            Event_Details newRecord = args.RowData as Event_Details;
            Event_Details order = context.Event_Details.First(i => i.eventID == newRecord.eventID);

            order.eventID = newRecord.eventID;
            order.eventAddress = newRecord.eventAddress;
            order.eventDescription = newRecord.eventDescription;
            order.capacity = newRecord.capacity;
            order.eventStart = newRecord.eventStart;
            order.eventEnd = newRecord.eventEnd;
            order.eventLocation = newRecord.eventLocation;
            order.eventPicture = newRecord.eventPicture;
            order.eventTitle = newRecord.eventTitle;
            order.eventGenre = newRecord.eventGenre;
            order.showOnHomePage = newRecord.showOnHomePage;
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
            var item = args.Items[0] as Event_Details;
            var id = item.eventID;
            var address = item.eventAddress;
            var description = item.eventDescription;
            var capacity = item.capacity;
            var eventStart = item.eventStart;
            var eventEnd = item.eventEnd;
            var eventPic = item.eventPicture;
            var eventTitle = item.eventTitle;
            var eventGenre = item.eventGenre;
            var showOnHomePage = item.showOnHomePage;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }

        /// <summary>
        /// Show the full time pattern for the column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void datagrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "eventStart" || e.Column.MappingName == "eventEnd")
            {
                // Setting default date and time format for Event Start and Event End column
                ((e.Column) as GridDateTimeColumn).Pattern = Syncfusion.Windows.Shared.DateTimePattern.FullDateTime;
            }
        }
    }
}
