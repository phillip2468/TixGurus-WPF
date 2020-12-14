using Ssample.Model;
using System.Windows.Controls;

namespace Ssample.Views.zEmployees.Employee
{
    /// <summary>
    /// Interaction logic for AddEmployeeView.xaml
    /// </summary>
    public partial class AddEmployeeView : UserControl
    {
        public AddEmployeeView()
        {
            InitializeComponent();
            this.DataGrid.RowValidating += SfDataGrid1_RowValidating;
        }

        public void SfDataGrid1_RowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Employee_Details newRecord = args.RowData as Employee_Details;
            Employee_Details currentEmployee = new Employee_Details();

            if (newRecord != null)
            {
                currentEmployee.employeeID = newRecord.employeeID;
                currentEmployee.email = newRecord.email;
                currentEmployee.password = newRecord.password;
                currentEmployee.firstName = newRecord.firstName;
                currentEmployee.lastName = newRecord.lastName;
            }


            using (context)
            {
                context.Employee_Details.Add(currentEmployee);
                context.SaveChanges();
                context.Dispose();
            }

        }
    }
}
