using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.ViewModel.Signing_In;

namespace Ssample.ViewModel.Employee
{
    public class EmployeeLoginViewModel : NavigationViewModelBase
    {
        private NavigationViewModelBase successfulSignInEmployeeViewModel;
        public ICommand NavCommand { get; set; }
        public ICommand GoToEmployeeSuccessCommand { get; set; }
        

        public EmployeeLoginViewModel()
        {
            successfulSignInEmployeeViewModel = new SuccessfulSignInEmployeeViewModel();
            GoToEmployeeSuccessCommand = new RelayCommand(GoToSuccessfulSignIn);
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        public EmployeeLoginViewModel(NavigationViewModelBase viewModel)
        {
            successfulSignInEmployeeViewModel = new SuccessfulSignInEmployeeViewModel();
            GoToEmployeeSuccessCommand = new RelayCommand(GoToSuccessfulSignIn);
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        private void GoToSuccessfulSignIn()
        {
            if (CanSignIn(Email, Password))
            {
                Navigate(successfulSignInEmployeeViewModel);
            }
            else
            {
                MessageBox.Show("You have entered the wrong sign in values");
            }
        }
        public Customer_Details CurrentEmployee { get; set; } = new Customer_Details();

        /// <summary>
        /// Field for email
        /// </summary>
        public string Email
        {
            get => CurrentEmployee.Email;
            set
            {
                CurrentEmployee.Email = value;
                OnPropertyChanged($"Email");
            }
        }

        /// <summary>
        /// Field for password
        /// </summary>
        public string Password
        {
            get => CurrentEmployee.Password;
            set
            {
                CurrentEmployee.Password = value;
                OnPropertyChanged($"Password");
            }
        }

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
            var user = context.Customer_Details.FirstOrDefault(u => u.Email == Email);

            //Checks if the entered in values are longer than 0
            if (email?.Length > 0 && password?.Length > 0 && user != null)
            {
                //If an entry equals a values in the database return true
                if (user.Password == Password)
                {
                    Properties.Settings.Default.Email = CurrentEmployee.Email;
                    Properties.Settings.Default.Save();
                    return true;
                }
            }
            //Returns false if sign in failed
            context.Dispose();
            return output;
        }
    }
}
