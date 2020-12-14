using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Ssample.ViewModel
{
    /// <summary>
    /// A view model responsible for signing in
    /// employees/administrators 
    /// </summary>
    public class EmployeeLoginViewModel : NavigationViewModelBase
    {
        #region 
        /// <summary>
        /// Field for employee dashboard
        /// </summary>
        private NavigationViewModelBase employeeDashboard;
        #endregion

        #region Commands

        /// <summary>
        /// Command for employee navigating to the employee dashboard
        /// </summary>
        public ICommand GoToEmployeeDashboardCommand { get; set; }

        /// <summary>
        /// Command to navigate to the respective view model
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor
        public EmployeeLoginViewModel()
        {
            //Initialization
            employeeDashboard = new EmployeeDashboardViewModel();

            //Command parameters
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            GoToEmployeeDashboardCommand = new RelayCommand(GoToSuccessfulSignIn);
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// Navigating to respective view model
        /// </summary>
        /// <param name="viewModel">View model</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        /// <summary>
        /// Functions for responsible to ensure correct
        /// login
        /// </summary>
        private void GoToSuccessfulSignIn()
        {
            if (CanSignIn(Email, Password))
            {
                Navigate(employeeDashboard);
            }
            else
            {
                MessageBox.Show("You have entered the wrong sign in values");
            }
        }

        /// <summary>
        /// Current employee list
        /// </summary>
        public Employee_Details CurrentEmployee { get; set; } = new Employee_Details();

        #endregion

        #region Properties for sign in

        /// <summary>
        /// Property for email
        /// </summary>
        public string Email
        {
            get => CurrentEmployee.email;
            set
            {
                CurrentEmployee.email = value;
                OnPropertyChanged($"Email");
            }
        }

        /// <summary>
        /// Property for password
        /// </summary>
        public string Password
        {
            get => CurrentEmployee.password;
            set
            {
                CurrentEmployee.password = value;
                OnPropertyChanged($"Password");
            }
        }
        #endregion

        #region Function for signing in
        /// <summary>
        /// Function which checks if values entered are in the database
        /// </summary>
        /// <param name="email">The email property</param>
        /// <param name="password">The password property</param>
        /// <returns></returns>
        public bool CanSignIn(string email, string password)
        {
            //Declares a data context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            bool output = false;

            //Checks for null values
            var user = context.Employee_Details.FirstOrDefault(u => u.email == Email);

            //Checks if the entered in values are longer than 0
            if (email?.Length > 0 && password?.Length > 0 && user != null)
            {
                //If an entry equals a values in the database return true
                if (user.password == Password)
                {
                    Properties.Settings.Default.Email = CurrentEmployee.email;
                    Properties.Settings.Default.Save();
                    return true;
                }
            }
            //Returns false if sign in failed
            context.Dispose();
            return output;
        }

        #endregion
    }
}
