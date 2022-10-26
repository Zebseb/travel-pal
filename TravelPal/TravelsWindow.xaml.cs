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
using TravelPal.Interfaces;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        private UserManager userManager;
        private User user;

        public TravelsWindow(UserManager userManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.user = userManager.signedInUser as User;
            this.userManager = userManager;
            lblUsername.Content = user.Username;

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(userManager);
            mainWindow.Show();
            Close();
        }

        private void btnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new(userManager);
            userDetailsWindow.Show();
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new(userManager);
            addTravelWindow.Show();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            TravelDetailsWindow travelDetailsWindow = new(userManager);
            travelDetailsWindow.Show();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAboutUs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your go-to traveling agency, making dreams come true since 1992!", "About Us", MessageBoxButton.OK);   
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("-Use the Add-button for adding new travels.\n" +
                "-Use the Details-button to show detailed info about a selected travel in the list.\n" +
                "-Use the Remove-button to remove a selected travel from the list.\n" +
                "-Use the Edit Account-button to edit profile settings (username, password etc.).", "Help", MessageBoxButton.OK);
        }
    }
}
