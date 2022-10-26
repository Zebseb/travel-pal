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
        private User user;
        public TravelsWindow(UserManager userManager, IUser user)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.user = user as User;

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new();
            userDetailsWindow.Show();
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new();
            addTravelWindow.Show();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            TravelDetailsWindow travelDetailsWindow = new();
            travelDetailsWindow.Show();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAboutUs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Traveling agency making dreams come true since 1992!", "Abous Us", MessageBoxButton.OK);   
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
