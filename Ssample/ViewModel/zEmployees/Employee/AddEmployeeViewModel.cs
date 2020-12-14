using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Employee
{
    public class AddEmployeeViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }

        public AddEmployeeViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            EmployeeDetails = (from data in context.Employee_Details select data).ToList();
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        private List<Employee_Details> EmployeeDetails { get; set; }

        public List<Employee_Details> Employee
        {
            get { return EmployeeDetails; }
            set { EmployeeDetails = value; }
        }
    }
}

