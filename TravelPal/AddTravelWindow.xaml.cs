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
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        private User user;
        private UserManager userManager;

        public AddTravelWindow(UserManager userManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.userManager = userManager;
            this.user = userManager.signedInUser as User;

            lblUsername.Content = user.Username;
            cbxAllInclusive.Visibility = Visibility.Collapsed;
            lblAllInclusive.Visibility = Visibility.Collapsed;
            cbTripType.Visibility = Visibility.Collapsed;
            lblTripType.Visibility = Visibility.Collapsed;
            
            string[] countries = Enum.GetNames(typeof(Countries));
            cbCountries.ItemsSource = countries;
            string[] travelTypes = Enum.GetNames(typeof(TravelType));
            cbTravelType.ItemsSource = travelTypes;
            string[] tripTypes = Enum.GetNames(typeof(TripTypes));
            cbTripType.ItemsSource = tripTypes;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(userManager);
            travelsWindow.Show();
            Close();
        }

        private void cbTravelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTravelType.SelectedIndex == 0)
            {
                cbTripType.Visibility = Visibility.Collapsed;
                lblTripType.Visibility = Visibility.Collapsed;
                cbxAllInclusive.Visibility = Visibility.Visible;
                lblAllInclusive.Visibility = Visibility.Visible;
            }

            else if (cbTravelType.SelectedIndex == 1)
            {
                cbxAllInclusive.IsChecked = false;
                cbxAllInclusive.Visibility = Visibility.Collapsed;
                lblAllInclusive.Visibility = Visibility.Collapsed;
                cbTripType.Visibility = Visibility.Visible;
                lblTripType.Visibility = Visibility.Visible;
            }
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
