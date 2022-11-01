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

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserManager userManager;
        private TravelManager travelManager;

        public RegisterWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.userManager = userManager;
            this.travelManager = travelManager;

            PopulateCountryComboBox();
        }

        private void PopulateCountryComboBox()
        {
            string[] countries = Enum.GetNames(typeof(Countries));
            cbCountries.ItemsSource = countries;
        }

        private void chbxShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            tbxPasswordBox.Text = pabxPasswordBox.Password;
            tbxPasswordBox2.Text = pabxPasswordBox2.Password;
            pabxPasswordBox.Visibility = Visibility.Collapsed;
            pabxPasswordBox2.Visibility = Visibility.Collapsed;
            tbxPasswordBox.Visibility = Visibility.Visible;
            tbxPasswordBox2.Visibility = Visibility.Visible;
        }

        private void chbxShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            pabxPasswordBox.Password = tbxPasswordBox.Text;
            pabxPasswordBox2.Password = tbxPasswordBox2.Text;
            tbxPasswordBox.Visibility = Visibility.Collapsed;
            tbxPasswordBox2.Visibility = Visibility.Collapsed;
            pabxPasswordBox.Visibility = Visibility.Visible;
            pabxPasswordBox2.Visibility = Visibility.Visible;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(userManager, travelManager);
            mainWindow.Show();
            Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = "";

            if (tbxUsername.Text.Trim().Length >= 3)
            {
                username = tbxUsername.Text;

                string password = "";
                string country = cbCountries.SelectedItem as string;

                Countries countryEnum = (Countries)Enum.Parse(typeof(Countries), country);

                if (pabxPasswordBox.Password == pabxPasswordBox2.Password)
                {
                    if (pabxPasswordBox.Password.Trim().Length >= 5)
                    {
                        password = pabxPasswordBox.Password;

                        bool isAvailableUsername = userManager.AddUser(username, password, countryEnum);

                        if (!isAvailableUsername)
                        {
                            MessageBox.Show("That username is already taken! Please choose another one...", "Warning!", MessageBoxButton.OK);
                            ClearPasswordBoxes();
                        }

                        else
                        {
                            MainWindow mainWindow = new(userManager, travelManager);
                            mainWindow.Show();
                            Close();
                        }
                    }

                    else
                    {
                        MessageBox.Show("You have to choose a password with at least 5 characters...", "Warning!", MessageBoxButton.OK);
                        ClearPasswordBoxes();
                    }
                }

                else
                {
                    MessageBox.Show("Your passwords have to match...", "Warning!", MessageBoxButton.OK);
                }
            }

            else
            {
                MessageBox.Show("You have to choose a username with at least 3 characters...", "Warning!", MessageBoxButton.OK);
                ClearPasswordBoxes();
            }
        }

        private void ClearPasswordBoxes()
        {
            tbxPasswordBox.Clear();
            tbxPasswordBox2.Clear();
            pabxPasswordBox.Clear();
            pabxPasswordBox2.Clear();
        }
    }
}
