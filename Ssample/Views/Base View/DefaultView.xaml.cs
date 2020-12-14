using Ssample.ViewModel.Base_view_models;
using System.Windows.Controls;

namespace Ssample.Views.Base_View
{
    /// <summary>
    /// Interaction logic for DefaultView.xaml
    /// </summary>
    public partial class DefaultView : UserControl
    {
        public DefaultView()
        {
            InitializeComponent();
            DataContext = new DefaultViewModel();
        }


    }
}
