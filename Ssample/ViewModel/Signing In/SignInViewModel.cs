using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Ssample.ViewModel.Signing_In
{
    /// <summary>
    /// A view model responsible for signing in for customers
    /// </summary>
    public class SignInViewModel : NavigationViewModelBase
    {
        #region Fields
        //Successful sign in view model for customers
        private NavigationViewModelBase succesfulSignInViewModel;

        //Navigate to employee login view model
        private NavigationViewModelBase employeeLoginViewModel;

        #endregion

        #region Commands
        /// <summary>
        /// Command for going to the employee login
        /// in view model
        /// </summary>
        public ICommand GoToEmployeeViewCommand { get; set; }

        /// <summary>
        /// Command to go back to the previous view model
        /// </summary>
        public ICommand NavCommand { get; set; }

        /// <summary>
        /// Command which navigates to the successful view model
        /// </summary>
        public ICommand GoToSuccessfulSignInViewCommand { get; set; }

        #endregion

        #region Constructor

        public SignInViewModel()
        {
            //Initialization of employee view model / sign in
            employeeLoginViewModel = new EmployeeLoginViewModel();
            succesfulSignInViewModel = new SuccessfulSignInViewModel();

            #region Command Parameters

            //Command to go to the successful sign in page
            GoToSuccessfulSignInViewCommand = new RelayCommand(GoToSuccessfulSignIn);

            //Command to go to employee view model
            GoToEmployeeViewCommand = new RelayCommand(GoToEmployeeLogin);

            //Command for going back
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

            #endregion
        }

        #endregion

        #region Helper functions

        /// <summary>
        /// A function that checks whether entered in values
        /// corresponds to those in database. If false, returns
        /// a message box indicating to user it has been blocked
        /// </summary>
        private void GoToSuccessfulSignIn()
        {
            if (CanSignIn(Email, Password))
            {
                Navigate(succesfulSignInViewModel);
            }
            else
            {
                MessageBox.Show("You have entered the wrong sign in values");
            }
        }

        /// <summary>
        /// Functionality to go back
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        /// <summary>
        /// Helper function which navigates to the
        /// employee login view model
        /// </summary>
        private void GoToEmployeeLogin()
        {
            Navigate(employeeLoginViewModel);
        }
        #endregion

        #region Login property

        public Customer_Details CurrentCustomer { get; set; } = new Customer_Details(); //Current customer you need to edit

        #endregion

        #region Property for sign in

        /// <summary>
        /// Property for email
        /// </summary>
        public string Email
        {
            get => CurrentCustomer.email;
            set
            {
                CurrentCustomer.email = value;
                OnPropertyChanged($"Email");
            }
        }

        /// <summary>
        /// Property for password
        /// </summary>
        public string Password
        {
            get => CurrentCustomer.password;
            set
            {
                CurrentCustomer.password = value;
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
            var user = context.Customer_Details.FirstOrDefault(u => u.email == Email);

            //Checks if the entered in values are longer than 0
            if (email?.Length > 0 && password?.Length > 0 && user != null)
            {
                //If an entry equals a values in the database return true
                if (user.password == Password)
                {
                    Properties.Settings.Default.Email = CurrentCustomer.email;
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
