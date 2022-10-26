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
        private UserManager userManager;
        private User user;
        public UserDetailsWindow(UserManager userManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.userManager = userManager;
            this.user = userManager.signedInUser as User;

            tbxUsername.Text = user.Username;
            cbCountries.SelectedItem = user.Location.ToString();

            tbxUsername.IsEnabled = false;
            pabxPasswordBox.IsEnabled = false;
            pabxPasswordBox2.IsEnabled = false;
            cbCountries.IsEnabled = false;
            btnSave.IsEnabled = false;
            chbxShowPassword.IsEnabled = false;

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
            TravelsWindow travelsWindow = new(userManager);
            travelsWindow.Show();
            Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            tbxUsername.IsEnabled = true;
            pabxPasswordBox.IsEnabled = true;
            pabxPasswordBox2.IsEnabled = true;
            cbCountries.IsEnabled = true;
            btnSave.IsEnabled = true;
            chbxShowPassword.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            user.Username = tbxUsername.Text;

            string country = cbCountries.SelectedItem as string;
            Countries countryEnum = (Countries)Enum.Parse(typeof(Countries), country);
            user.Location = countryEnum;

            if (pabxPasswordBox.Password == pabxPasswordBox2.Password)
            {
                user.Password = pabxPasswordBox.Password;
            }

            tbxUsername.Text = user.Username;
            cbCountries.SelectedItem = user.Location.ToString();

            pabxPasswordBox.Clear();
            pabxPasswordBox2.Clear();
            tbxPasswordBox.Clear();
            tbxPasswordBox2.Clear();

            tbxUsername.IsEnabled = false;
            pabxPasswordBox.IsEnabled = false;
            pabxPasswordBox2.IsEnabled = false;
            cbCountries.IsEnabled = false;
            btnSave.IsEnabled = false;
            chbxShowPassword.IsEnabled = false;
        }
    }
}
