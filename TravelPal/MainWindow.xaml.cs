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
        private TravelManager travelManager = new();
        private IUser user;
        private UserManager userManager = new();
        private List<IUser> users;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public MainWindow(UserManager userManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.userManager = userManager;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new(userManager);
            registerWindow.Show();
            Close();
        }

        private void chbxShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            tbxPasswordBox.Text = pabxPasswordBox.Password;
            pabxPasswordBox.Visibility = Visibility.Collapsed;
            tbxPasswordBox.Visibility = Visibility.Visible;
        }

        private void chbxShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            pabxPasswordBox.Password = tbxPasswordBox.Text;
            tbxPasswordBox.Visibility = Visibility.Collapsed;
            pabxPasswordBox.Visibility = Visibility.Visible;
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string password = pabxPasswordBox.Password;

            bool isUserFound = userManager.SignInUser(username, password);
            user = userManager.signedInUser;

            if (isUserFound)
            {
                if (user is User)
                {
                    TravelsWindow travelsWindow = new(userManager);
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

            //bool usernameFound = false;

            //if (!userIsFound)
            //{
            //    foreach (IUser user in users)
            //    {
            //        if (user.Username == username)
            //        {
            //            usernameFound = true;

            //            MessageBox.Show("Username or password is incorrect...", "Warning!", MessageBoxButton.OK);
            //        }
            //    }
            //}

            //if (!userIsFound && !usernameFound)
            //{
            //    MessageBox.Show("Please register before signing in...", "Warning!", MessageBoxButton.OK);

            //}
        }
    }
}
