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
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        private User user;
        private UserManager userManager;
        private Travel travel;
        private Vacation vacation;
        private Trip trip;

        public TravelDetailsWindow(UserManager userManager, Travel selectedTravel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.travel = selectedTravel;
            this.userManager = userManager;
            this.user = userManager.signedInUser as User;
            lblUsername.Content = user.Username;

            tbxNumOfTravelers.IsEnabled = false;
            tbxDeparture.IsEnabled = false;
            tbxDestination.IsEnabled = false;
            tbxTravelType.IsEnabled = false;
            tbxAllInclusive.IsEnabled = false;
            tbxTripType.IsEnabled = false;

            lblTripType.Visibility = Visibility.Collapsed;
            tbxTripType.Visibility = Visibility.Collapsed;
            lblAllInclusive.Visibility = Visibility.Collapsed;
            tbxAllInclusive.Visibility = Visibility.Collapsed;
            
            tbxNumOfTravelers.Text = selectedTravel.Travelers.ToString();
            tbxDeparture.Text = selectedTravel.Country.ToString();
            tbxDestination.Text = selectedTravel.Destination;

            if (selectedTravel is Vacation)
            {
                vacation = selectedTravel as Vacation;
                lblAllInclusive.Visibility = Visibility.Visible;
                tbxAllInclusive.Visibility = Visibility.Visible;
                tbxAllInclusive.Text = vacation.AllInclusive.ToString();
                tbxTravelType.Text = "Vacation";
            }

            else if (selectedTravel is Trip)
            {
                trip = selectedTravel as Trip;
                lblTripType.Visibility = Visibility.Visible;
                tbxTripType.Visibility = Visibility.Visible;
                tbxTripType.Text = trip.Type.ToString();
                tbxTravelType.Text = "Trip";
            }

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(userManager);
            travelsWindow.Show();
            Close();
        }
    }
}
