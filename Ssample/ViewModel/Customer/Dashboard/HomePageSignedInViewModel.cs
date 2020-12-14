using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.ViewModel.Dashboard;
using Ssample.Views.Dashboard;

namespace Ssample.ViewModel
{
    /// <summary>
    /// This class represents
    /// a user who has signed in
    /// and wants to view their dashboard
    /// </summary>
    public class HomePageSignedInViewModel : NavigationViewModelBase
    {
        private NavigationViewModelBase _myAccountView;

        public ICommand GoToAccount { get; set; }

        public HomePageSignedInViewModel(NavigationViewModelBase myAccountViewModel)
        {
            _myAccountView = new MyAccountViewModel();

            GoToAccount = new RelayCommand(GoToAccountView);
        }

        public HomePageSignedInViewModel()
        {
            _myAccountView = new MyAccountViewModel();

            GoToAccount = new RelayCommand(GoToAccountView);
        }

        private void GoToAccountView()
        {
            Navigate(_myAccountView);
        }
    }
}
