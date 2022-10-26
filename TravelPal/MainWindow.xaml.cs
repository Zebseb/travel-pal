using System;
using System.Collections.Generic;
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
        private List<IUser> users;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new(userManager);
            registerWindow.Show();
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
            users = userManager.GetUsers();

            bool userIsFound = false;
            string username = tbxUsername.Text;
            string password = pabxPasswordBox.Password;

            foreach (IUser user in users)
            {
                if (username == user.Username && password == user.Password)
                {
                    userIsFound = true; 

                    if (user is User)
                    {
                        TravelsWindow travelsWindow = new(userManager, user);
                        travelsWindow.Show();
                        tbxUsername.Clear();
                        pabxPasswordBox.Clear();
                        tbxPasswordBox.Clear();
                        chbxShowPassword.IsChecked = false;
                    }

                    else if (user is Admin)
                    {
                        AdminWindow adminWindow = new(userManager, user);
                        adminWindow.Show();
                        tbxUsername.Clear();
                        pabxPasswordBox.Clear();
                        tbxPasswordBox.Clear();
                        chbxShowPassword.IsChecked = false;
                    }
                }

                else if (username != user.Username && password == user.Password || username == user.Username && password != user.Password)
                {
                    userIsFound = true;
                    MessageBox.Show("Username or password is incorrect...", "Warning!", MessageBoxButton.OK);
                }
            }

            if (!userIsFound)
            {
                MessageBox.Show("Please register before signing in...", "Warning!", MessageBoxButton.OK);
                tbxUsername.Clear();
                pabxPasswordBox.Clear();
                tbxPasswordBox.Clear();
                chbxShowPassword.IsChecked = false;
            }
        }
    }
}
