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
using System.Windows.Shapes;
using TravelPal.Enums;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        private TravelManager travelManager;
        private UserManager userManager;
        private User user;

        public UserDetailsWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.travelManager = travelManager;
            this.userManager = userManager;
            this.user = userManager.signedInUser as User;

            lblUsername.Content = user.Username;
            tbxUsername.Text = user.Username;
            cbCountries.SelectedItem = user.Location.ToString();

            DisableTextBoxes();
            PopulateCountryComboBox();
        }

        //Populates combobox with all countries from Countries-enum
        private void PopulateCountryComboBox()
        {
            string[] countries = Enum.GetNames(typeof(Countries));
            cbCountries.ItemsSource = countries;
        }

        //Disables all boxes in the window
        private void DisableTextBoxes()
        {
            pabxCurrentPasswordBox.IsEnabled = false;
            tbxUsername.IsEnabled = false;
            pabxPasswordBox.IsEnabled = false;
            pabxPasswordBox2.IsEnabled = false;
            cbCountries.IsEnabled = false;
            btnSave.IsEnabled = false;
            chbxShowPassword.IsEnabled = false;
        }

        //Collapses password-boxes and displays password-textboxes if the checkbox for "Show Password" is checked
        private void chbxShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            tbxPasswordBox.Text = pabxPasswordBox.Password;
            tbxPasswordBox2.Text = pabxPasswordBox2.Password;
            pabxPasswordBox.Visibility = Visibility.Collapsed;
            pabxPasswordBox2.Visibility = Visibility.Collapsed;
            tbxPasswordBox.Visibility = Visibility.Visible;
            tbxPasswordBox2.Visibility = Visibility.Visible;
        }
        
        //Collapses password-textboxes and displays password-boxes if the checkbox for "Show Password" is unchecked
        private void chbxShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            pabxPasswordBox.Password = tbxPasswordBox.Text;
            pabxPasswordBox2.Password = tbxPasswordBox2.Text;
            tbxPasswordBox.Visibility = Visibility.Collapsed;
            tbxPasswordBox2.Visibility = Visibility.Collapsed;
            pabxPasswordBox.Visibility = Visibility.Visible;
            pabxPasswordBox2.Visibility = Visibility.Visible;
        }

        //Resets and clears the password-boxes
        private void ClearTextBoxes()
        {
            pabxCurrentPasswordBox.Clear();
            pabxPasswordBox.Clear();
            pabxPasswordBox2.Clear();
            tbxPasswordBox.Clear();
            tbxPasswordBox2.Clear();
        }

        //Enables click and drag for the window's position
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //Minimizes the window when pressing "-"
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Closes the program when clicking "X"
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        //Saves updated account information if the user enters correct input and shows warnings if input is missing or incorrect
        private void btnSaveDetails_Click(object sender, RoutedEventArgs e)
        {
            string username = "";

            if (pabxCurrentPasswordBox.Password == user.Password)
            {
                if (tbxUsername.Text.Trim().Length >= 3)
                {
                    username = tbxUsername.Text;
                    bool isAvailableUsename = userManager.UpdateUsername(user, username);

                    if (isAvailableUsename)
                    {
                        user.Username = username;

                        string country = cbCountries.SelectedItem as string;
                        Countries countryEnum = (Countries)Enum.Parse(typeof(Countries), country);
                        user.Location = countryEnum;

                        if (pabxPasswordBox.Password.Trim().Length > 0 && pabxPasswordBox2.Password.Trim().Length > 0)
                        {
                            if (pabxPasswordBox.Password.Trim().Length >= 5 && pabxPasswordBox.Password == pabxPasswordBox2.Password)
                            {
                                user.Password = pabxPasswordBox.Password;

                                MessageBox.Show("Username and password was updated!", "Info", MessageBoxButton.OK);

                                TravelsWindow travelsWindow = new(userManager, travelManager);
                                travelsWindow.Show();
                                Close();
                            }

                            else if (pabxPasswordBox.Password.Trim().Length < 5)
                            {
                                MessageBox.Show("Username was updated, but you have to choose a password with at least 5 characters...", "Warning!", MessageBoxButton.OK);
                            }

                            else if (pabxPasswordBox.Password != pabxPasswordBox2.Password)
                            {
                                MessageBox.Show("Username was updated, but your passwords have to match in order to update your current one...", "Warning!", MessageBoxButton.OK);
                            }
                        }

                        else if (tbxPasswordBox.Text.Trim().Length > 0 && tbxPasswordBox2.Text.Trim().Length > 0)
                        {
                            if (tbxPasswordBox.Text.Trim().Length >= 5 && tbxPasswordBox.Text == tbxPasswordBox2.Text)
                            {
                                user.Password = tbxPasswordBox.Text;

                                MessageBox.Show("Username and password was updated!", "Info", MessageBoxButton.OK);

                                TravelsWindow travelsWindow = new(userManager, travelManager);
                                travelsWindow.Show();
                                Close();
                            }

                            else if (tbxPasswordBox.Text.Trim().Length < 5)
                            {
                                MessageBox.Show("Username was updated, but you have to choose a password with at least 5 characters...", "Warning!", MessageBoxButton.OK);
                            }

                            else if (tbxPasswordBox.Text != tbxPasswordBox2.Text)
                            {
                                MessageBox.Show("Username was updated, but your passwords have to match in order to update your current one...", "Warning!", MessageBoxButton.OK);
                            }
                        }

                        if (isAvailableUsename)
                        {
                            MessageBox.Show("Your username was updated!", "Warning!", MessageBoxButton.OK);
                            TravelsWindow travelsWindow = new(userManager, travelManager);
                            travelsWindow.Show();
                            Close();
                        }
                    }

                    else if (!isAvailableUsename)
                    {
                        {
                            MessageBox.Show("That username is already taken! Please choose another one...", "Warning!", MessageBoxButton.OK);
                            cbCountries.SelectedItem = user.Location.ToString();
                        }
                    }
                }

                else
                {
                    MessageBox.Show("You have to choose a username with at least 3 characters...", "Warning!", MessageBoxButton.OK);
                }
            }

            else
            {
                MessageBox.Show("Please enter your current password correctly to update user details...", "Warning!", MessageBoxButton.OK);
            }

            ClearTextBoxes();
        }


        //Enables all boxes if the user clicks the Edit-button
        private void btnEditDetails_Click(object sender, RoutedEventArgs e)
        {
            pabxCurrentPasswordBox.IsEnabled = true;
            tbxUsername.IsEnabled = true;
            pabxPasswordBox.IsEnabled = true;
            pabxPasswordBox2.IsEnabled = true;
            cbCountries.IsEnabled = true;
            btnSave.IsEnabled = true;
            chbxShowPassword.IsEnabled = true;
        }

        //Sends the user back to the TravelWindow and closes the UserDetailsWindow when clicking the Return-button and closes the UserDetailsWindow
        private void btnReturn_Click_1(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(userManager, travelManager);
            travelsWindow.Show();
            Close();
        }
    }
}
