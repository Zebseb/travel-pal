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
        private TravelManager travelManager;

        public AddTravelWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.travelManager = travelManager;
            this.userManager = userManager;
            this.user = userManager.signedInUser as User;

            lblUsername.Content = user.Username;

            CollapseTextBoxesAndLabels();
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            string[] countries = Enum.GetNames(typeof(Countries));
            cbCountries.ItemsSource = countries;
            string[] travelTypes = Enum.GetNames(typeof(TravelType));
            cbTravelType.ItemsSource = travelTypes;
            string[] tripTypes = Enum.GetNames(typeof(TripTypes));
            cbTripType.ItemsSource = tripTypes;
        }

        private void CollapseTextBoxesAndLabels()
        {
            cbxAllInclusive.Visibility = Visibility.Collapsed;
            lblAllInclusive.Visibility = Visibility.Collapsed;
            cbTripType.Visibility = Visibility.Collapsed;
            lblTripType.Visibility = Visibility.Collapsed;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(userManager, travelManager);
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
            int numOfTravelers = 0;
            string country = "";
            string destination = "";
            string tripType = "";
            bool isAllInclusive = false;

            if (cbTravelType.SelectedIndex == 0)
            {
                numOfTravelers = int.Parse(tbxNumOfTravelers.Text);
                country = cbCountries.SelectedItem as string;
                destination = tbxDestination.Text;

                Countries countryEnum = (Countries)Enum.Parse(typeof(Countries), country);
                
                if ((bool)cbxAllInclusive.IsChecked)
                {
                    isAllInclusive = true;
                }

                Vacation newVacation = new(isAllInclusive, destination, numOfTravelers, countryEnum);
                user.travels.Add(newVacation);
                travelManager.AddTravel(newVacation);

                TravelsWindow travelsWindow = new(userManager, travelManager);
                travelsWindow.Show();
                Close();
            }

            else if (cbTravelType.SelectedIndex == 1)
            {
                numOfTravelers = int.Parse(tbxNumOfTravelers.Text);
                country = cbCountries.SelectedItem as string;
                destination = tbxDestination.Text;
                tripType = cbTripType.SelectedItem as string;

                Countries countryEnum = (Countries)Enum.Parse(typeof(Countries), country);
                TripTypes tripEnum = (TripTypes)Enum.Parse(typeof(TripTypes), tripType);

                Trip newTrip = new(tripEnum, destination, numOfTravelers, countryEnum);
                user.travels.Add(newTrip);
                travelManager.AddTravel(newTrip);

                TravelsWindow travelsWindow = new(userManager, travelManager);
                travelsWindow.Show();
                Close();
            }
        }
    }
}
