using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;

namespace Ssample.ViewModel
{
    /// <summary>
    /// View model for successfully creating an account with TixGurus
    /// </summary>
    public class SuccessfulRegisterViewModel : NavigationViewModelBase
    {
        #region Fields
        //Field for the default viewmodel
        private NavigationViewModelBase _goToDefault;

        #endregion

        #region Commands
        //Command for going to the default view model
        public ICommand GoToDefaultViewCommand { get; set; }

        #endregion

        #region Constructor

        public SuccessfulRegisterViewModel(NavigationViewModelBase goToDefault)
        {
            _goToDefault = goToDefault;
            GoToDefaultViewCommand = new RelayCommand(GoBackToDefault);
        }
        #endregion

        #region Helper functions
        /// <summary>
        /// Function for navigating back to the default view
        /// </summary>
        private void GoBackToDefault()
        {
            Navigate(_goToDefault);
        }

        #endregion

    }
}
