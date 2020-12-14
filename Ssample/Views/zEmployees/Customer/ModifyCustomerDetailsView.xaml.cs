using Ssample.Model;
using Syncfusion.UI.Xaml.Grid;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace Ssample.Views
{
    /// <summary>
    /// Interaction logic for ModifyCustomerDetailsView.xaml
    /// </summary>
    public partial class ModifyCustomerDetailsView : UserControl
    {

        public ModifyCustomerDetailsView()
        {
            InitializeComponent();
            this.DataGrid.RowValidated += dataGrid_RowValidated;
            this.DataGrid.RecordDeleting += dataGrid_RecordDeleting;

        }
        /// <summary>
        /// Edit rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RowValidated(object sender, Syncfusion.UI.Xaml.Grid.RowValidatedEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Customer_Details newRecord = args.RowData as Customer_Details;
            if (sender == null)
            {
                return;
            }
            Customer_Details order = context.Customer_Details.First(i => i.customerID == newRecord.customerID);

            if (newRecord != null)
            {
                order.customerID = newRecord.customerID;
                order.email = newRecord.email;
                order.password = newRecord.password;
                order.firstName = newRecord.firstName;
                order.lastName = newRecord.lastName;
                order.address = newRecord.address;
                order.city = newRecord.city;
                order.state = newRecord.state;
                order.postcode = newRecord.postcode;
                order.dateOfBirth = newRecord.dateOfBirth;
                order.dateCreated = newRecord.dateCreated;
            }

            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
            context.Dispose();
        }
        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RecordDeleting(object sender, RecordDeletingEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            var item = args.Items[0] as Customer_Details;
            var id = item.customerID;
            var email = item.email;
            var password = item.password;
            var firstName = item.firstName;
            var lastName = item.lastName;
            var address = item.address;
            var city = item.city;
            var state = item.state;
            var postcode = item.postcode;
            var dateOfBirth = item.dateOfBirth;
            var dateCreated = item.dateCreated;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }

    }
}
