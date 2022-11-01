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

            if (CheckInputs())
            {
                if (cbTravelType.SelectedIndex == 0)
                {
                    numOfTravelers = ParseNumOfTravelers();
                
                    if (numOfTravelers > 0)
                    {
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
                }

                else if (cbTravelType.SelectedIndex == 1)
                {
                    numOfTravelers = ParseNumOfTravelers();

                    if (numOfTravelers > 0)
                    {
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

            else
            {
                MessageBox.Show("In order to add a travel you need to give us the required info...", "Warning!", MessageBoxButton.OK);
            }
        }

        private int ParseNumOfTravelers()
        {
            int numOfTravelers = 0;

            try
            {
                numOfTravelers = int.Parse(tbxNumOfTravelers.Text);

                if (numOfTravelers <= 0)
                {
                    MessageBox.Show("0 travelers can't go anywhere...", "Warning!", MessageBoxButton.OK);
                }
            }

            catch (OverflowException ex)
            {
                MessageBox.Show("The flight can't take that many passengers...", "Warning", MessageBoxButton.OK);
            }

            catch (FormatException ex)
            {
                MessageBox.Show("Please input a number for the amount of travelers...", "Warning!", MessageBoxButton.OK);
            }

                return numOfTravelers;
        }

        private bool CheckInputs()
        {
            bool isEmptyFieldsVacation = false;
            bool isEmptyFieldsTrip = false;

            string numOfTravelers = tbxNumOfTravelers.Text;
            string country = cbCountries.SelectedItem as string;
            string destination = tbxDestination.Text;
            string travelType = cbTravelType.SelectedItem as string;
            string tripType = cbTripType.SelectedItem as string;

            string[] vacationFields = new[] { numOfTravelers, country, destination, travelType };
            string[] tripFields = new[] { numOfTravelers, country, destination, travelType, tripType };

            foreach (string field in vacationFields)
            {
                if (string.IsNullOrEmpty(field))
                {
                    isEmptyFieldsVacation = true;
                }
            }

            if (cbTravelType.SelectedIndex == 1)
            {
                foreach (string field in tripFields)
                {
                    if (string.IsNullOrEmpty(field))
                    {
                        isEmptyFieldsTrip = true;
                    }
                }
            }

            if (isEmptyFieldsVacation || isEmptyFieldsTrip)
            {
                return false;
            }

            return true;
        }
    }
}
