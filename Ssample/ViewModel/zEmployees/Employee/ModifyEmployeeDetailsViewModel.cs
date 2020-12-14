using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel
{
    public class ModifyEmployeeDetailsViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }
        public ModifyEmployeeDetailsViewModel()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            EmployeeList = (from data in context.Employee_Details select data).ToList();

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        private List<Employee_Details> Employee { get; set; }

        public List<Employee_Details> EmployeeList
        {
            get => Employee;
            set => Employee = value;
        }
    }
}
