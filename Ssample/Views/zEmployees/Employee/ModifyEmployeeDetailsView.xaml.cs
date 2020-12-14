using Ssample.Model;
using Syncfusion.UI.Xaml.Grid;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace Ssample.Views
{
    /// <summary>
    /// Interaction logic for ModifyEmployeeDetailsView.xaml
    /// </summary>
    public partial class ModifyEmployeeDetailsView : UserControl
    {
        CustomerDatabaseEntities context = new CustomerDatabaseEntities();

        public ModifyEmployeeDetailsView()
        {
            InitializeComponent();
            this.DataGrid.RowValidated += dataGrid_RowValidated;
            this.DataGrid.RecordDeleting += dataGrid_RecordDeleting;
        }

        /// <summary>
        /// Changes record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RowValidated(object sender, Syncfusion.UI.Xaml.Grid.RowValidatedEventArgs args)
        {
            Employee_Details newRecord = args.RowData as Employee_Details;
            Employee_Details order = context.Employee_Details.First(i => i.employeeID == newRecord.employeeID);

            order.employeeID = newRecord.employeeID;
            order.email = newRecord.email;
            order.password = newRecord.password;
            order.firstName = newRecord.firstName;
            order.lastName = newRecord.lastName;
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
            var item = args.Items[0] as Employee_Details;
            var id = item.employeeID;
            var email = item.email;
            var password = item.password;
            var firstName = item.firstName;
            var lastName = item.lastName;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }
    }
}
