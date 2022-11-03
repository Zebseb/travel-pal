using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelPal.Interfaces;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserManager userManager = new();
        private TravelManager travelManager = new();
        private IUser user;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public MainWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.userManager = userManager;
            this.travelManager = travelManager;
        }

        //Sends the user to the RegisterWindow and closes the MainWindow when clicking the hyperlink
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new(userManager, travelManager);
            registerWindow.Show();
            Close();
        }

        //Collapses the password-box and displays the password-textbox if the checkbox for "Show Password" is checked
        private void chbxShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            tbxPasswordBox.Text = pabxPasswordBox.Password;
            pabxPasswordBox.Visibility = Visibility.Collapsed;
            tbxPasswordBox.Visibility = Visibility.Visible;
        }
        //Collapses the password-textbox and displays the password-box if the checkbox for "Show Password" is unchecked
        private void chbxShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            pabxPasswordBox.Password = tbxPasswordBox.Text;
            tbxPasswordBox.Visibility = Visibility.Collapsed;
            pabxPasswordBox.Visibility = Visibility.Visible;
        }

        //Clears all inputboxes
        private void ClearTextBoxes()
        {
            chbxShowPassword.IsChecked = false;
            tbxUsername.Clear();
            tbxPasswordBox.Clear();
            pabxPasswordBox.Clear();
        }

        //Enables click and drag for the window's position
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //Minimizes the window when clicking "-"
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Closes the program when clicking "X"
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        //Checks if the user is registered and signs in. Shows a warning if the username or password is incorrect
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string password = "";

            if (!(bool)chbxShowPassword.IsChecked)
            {
                password = pabxPasswordBox.Password;
            }

            else if ((bool)chbxShowPassword.IsChecked)
            {
                password = tbxPasswordBox.Text;
            }

            bool isUserFound = userManager.SignInUser(username, password);
            user = userManager.signedInUser;

            if (isUserFound)
            {
                if (user is User)
                {
                    TravelsWindow travelsWindow = new(userManager, travelManager);
                    travelsWindow.Show();
                    Close();
                }

                else if (user is Admin)
                {
                    AdminWindow adminWindow = new(userManager, travelManager);
                    adminWindow.Show();
                    Close();
                }
            }

            if (!isUserFound)
            {
                MessageBox.Show("Username or password is incorrect... Please register if you are not a member yet!", "Warning!", MessageBoxButton.OK);
                ClearTextBoxes();
            }
        }
    }
}
