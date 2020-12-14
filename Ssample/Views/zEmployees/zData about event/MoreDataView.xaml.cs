using Ssample.ViewModel.zEmployees.zData_about_event;
using System.Windows.Controls;

namespace Ssample.Views.zEmployees.zData_about_event
{
    /// <summary>
    /// Interaction logic for MoreDataView.xaml
    /// </summary>
    public partial class MoreDataView : UserControl
    {
        public MoreDataView()
        {
            InitializeComponent();
            DataContext = new MoreDataViewModel();
        }
    }
}
