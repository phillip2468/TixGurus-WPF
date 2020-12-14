using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;

namespace Ssample.ViewModel.Dashboard
{
    public class MyAccountViewModel : NavigationViewModelBase
    {
        private NavigationViewModelBase ChangeMyDetailsViewModel;

        public ICommand NavCommand { get; set; }
        public ICommand GoToChangeDetailsCommand { get; set; }


        public MyAccountViewModel()
        {
            ChangeMyDetailsViewModel = new ChangeMyDetailsViewModel(this);

            GoToChangeDetailsCommand = new RelayCommand(GoToChangeDetails);

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        public void GoToChangeDetails()
        {
            Navigate(ChangeMyDetailsViewModel);
        }

        public void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
    }
}
