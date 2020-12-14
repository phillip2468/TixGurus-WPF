using System.Windows.Controls;
using Ssample.ViewModel.Base_view_models;

namespace Ssample.Views.Base_View
{
    /// <summary>
    /// Interaction logic for SearchEventView.xaml
    /// </summary>
    public partial class SearchEventView : UserControl
    {
        public SearchEventView()
        {
            InitializeComponent();
            DataContext = new SearchEventViewModel();
        }
    }
}
