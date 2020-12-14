using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using System.Windows.Input;

namespace Ssample.ViewModel.Register
{
    /// <summary>
    /// View model for successfully creating an account with TixGurus
    /// </summary>
    public class SuccessfulRegisterViewModel : NavigationViewModelBase
    {
        #region Commands
        //Command for going to the default view model
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor

        public SuccessfulRegisterViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }
        #endregion

        #region Helper functions
        /// <summary>
        /// Function to navigate using command parameters
        /// </summary>
        /// <param name="viewModel">ViewModel parameter</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        #endregion

    }
}
