using Ssample.ViewModel.zEmployees.Event_Management;
using Syncfusion.UI.Xaml.Grid;
using System.Windows.Controls;

namespace Ssample.Views.zEmployees.Event_Managment
{
    /// <summary>
    /// Interaction logic for GenerateTicketsView.xaml
    /// </summary>
    public partial class GenerateTicketsView : UserControl
    {
        public GenerateTicketsView()
        {
            InitializeComponent();
            DataContext = new GenerateTicketsViewModel();
            DataGrid.AutoGeneratingColumn += datagrid_AutoGeneratingColumn;
        }


        private void datagrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "eventStart" || e.Column.MappingName == "eventEnd")
            {
                // Setting default date and time format for Event Start and Event End column
                ((e.Column) as GridDateTimeColumn).Pattern = Syncfusion.Windows.Shared.DateTimePattern.FullDateTime;
            }
        }

    }
}
