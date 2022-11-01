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

        //Sends the user back to the TravelWindow and closes the UserDetailsWindow when clicking the Return-button and closes the UserDetailsWindow
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(userManager, travelManager);
            travelsWindow.Show();
            Close();
        }

        //Enables all boxes if the user clicks the Edit-button
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            pabxCurrentPasswordBox.IsEnabled = true;
            tbxUsername.IsEnabled = true;
            pabxPasswordBox.IsEnabled = true;
            pabxPasswordBox2.IsEnabled = true;
            cbCountries.IsEnabled = true;
            btnSave.IsEnabled = true;
            chbxShowPassword.IsEnabled = true;
        }

        //Saves updated account information if the user enters correct input and shows warnings if input is missing or incorrect
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string username = "";

            if (tbxUsername.Text.Trim().Length >= 3)
            {
                username = tbxUsername.Text;
            }

            else
            {
                MessageBox.Show("You have to choose a username with at least 3 characters...", "Warning!", MessageBoxButton.OK);
            }

            if (pabxCurrentPasswordBox.Password == user.Password)
            {
                bool isUpdatedUser = userManager.UpdateUsername(user, username);

                if (isUpdatedUser)
                {
                    string country = cbCountries.SelectedItem as string;
                    Countries countryEnum = (Countries)Enum.Parse(typeof(Countries), country);
                    user.Location = countryEnum;

                    if (pabxPasswordBox.Password == pabxPasswordBox2.Password)
                    {
                        if (pabxPasswordBox.Password.Trim().Length >= 5)
                        {
                            user.Password = pabxPasswordBox.Password;

                            MessageBox.Show("Account details was updated!", "Info", MessageBoxButton.OK);

                            TravelsWindow travelsWindow = new(userManager, travelManager);
                            travelsWindow.Show();
                            Close();
                        }

                        else
                        {
                            MessageBox.Show("You have to choose a password with at least 5 characters...", "Warning!", MessageBoxButton.OK);
                        }
                    }

                    else if (tbxPasswordBox.Text == tbxPasswordBox2.Text)
                    {
                        if (tbxPasswordBox.Text.Trim().Length >= 5)
                        {
                            user.Password = tbxPasswordBox.Text;

                            MessageBox.Show("Account details was updated!", "Info", MessageBoxButton.OK);

                            TravelsWindow travelsWindow = new(userManager, travelManager);
                            travelsWindow.Show();
                            Close();
                        }

                        else
                        {
                            MessageBox.Show("You have to choose a password with at least 5 characters...", "Warning!", MessageBoxButton.OK);
                        }
                    }

                    else
                    {
                        MessageBox.Show("Your passwords have to match...", "Warning!", MessageBoxButton.OK);
                    }
                }

                else if (!isUpdatedUser)
                {
                    {
                        MessageBox.Show("That username is already taken! Please choose another one...", "Warning!", MessageBoxButton.OK);
                        cbCountries.SelectedItem = user.Location.ToString();
                    }
                }
            }

            else
            {
                MessageBox.Show("Please enter your current password correctly to update user details...", "Warning!", MessageBoxButton.OK);
            }

            ClearTextBoxes();
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
    }
}
