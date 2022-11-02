using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TravelPal.Interfaces;
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
        private List<IPackingListItem> packingList = new();

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

        //Populates all comboboxes in the window
        private void PopulateComboBoxes()
        {
            string[] countries = Enum.GetNames(typeof(Countries));
            cbCountries.ItemsSource = countries;
            string[] travelTypes = Enum.GetNames(typeof(TravelType));
            cbTravelType.ItemsSource = travelTypes;
            string[] tripTypes = Enum.GetNames(typeof(TripTypes));
            cbTripType.ItemsSource = tripTypes;
        }

        //Collapses UI
        private void CollapseTextBoxesAndLabels()
        {
            lvPackingList.IsEnabled = false;
            cbxAllInclusive.Visibility = Visibility.Collapsed;
            lblAllInclusive.Visibility = Visibility.Collapsed;
            cbTripType.Visibility = Visibility.Collapsed;
            lblTripType.Visibility = Visibility.Collapsed;
            chbxRequired.Visibility = Visibility.Collapsed;
            lblRequired.Visibility = Visibility.Collapsed;
        }

        //Sends the user back to the TravelsWindow and closes the AddTravelWindow when clicking the Return-button
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(userManager, travelManager);
            travelsWindow.Show();
            Close();
        }

        //Shows/collapses UI depending on what TravelType is selected
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

        //Adds a travel if all input is correct and will show warnings if input is missing or incorrect
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

                        Vacation newVacation = new(isAllInclusive, destination, numOfTravelers, countryEnum, DateTime.Now, DateTime.Now); //TO-DO Tillfällig lösning med DateTime
                        newVacation.PackingList = this.packingList;
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

                        Trip newTrip = new(tripEnum, destination, numOfTravelers, countryEnum, DateTime.Now, DateTime.Now); //TO-DO Tillfällig lösning med DateTime
                        newTrip.PackingList = this.packingList;
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

        //Tries to parse user input and returns an int. Shows warnings if the input doesn't parse correctly.
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

        //Checks that the user has entered input in all fields and returns a bool
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

        private void btnAdditem_Click(object sender, RoutedEventArgs e)
        {
            int itemQuantity;
            bool isRequiredItem = false;
            string itemName = tbxAddItem.Text;

            if (tbxQuantity.Text.Trim().Length > 0 && !(bool)chbxDocument.IsChecked)
            {
                itemQuantity = int.Parse(tbxQuantity.Text);

                OtherItem newOtherItem = new(itemName, itemQuantity);
                packingList.Add(newOtherItem);

                ListViewItem item = new();
                item.Content = newOtherItem.ToString();
                item.Tag = newOtherItem;

                lvPackingList.Items.Add(item);
                tbxAddItem.Clear();
                tbxQuantity.Clear();
            }

            else if ((bool)chbxDocument.IsChecked)
            {
                if ((bool)chbxRequired.IsChecked)
                {
                    isRequiredItem = true;
                }

                TravelDocument newTravelDocument = new(itemName, isRequiredItem);
                packingList.Add(newTravelDocument);

                ListViewItem item = new();
                item.Content = newTravelDocument.ToString();
                item.Tag = newTravelDocument;

                lvPackingList.Items.Add(item);
                tbxAddItem.Clear();
                chbxDocument.IsChecked = false;
                chbxRequired.IsChecked = false;
                lblRequired.Visibility = Visibility.Collapsed;
                chbxRequired.Visibility = Visibility.Collapsed;
            }

            else
            {
                MessageBox.Show("Please enter a quantity of your item or check the Document checkbox to add a new item...\n", "Warning!", MessageBoxButton.OK);
            }
        }

        private void chbxDocument_Checked(object sender, RoutedEventArgs e)
        {
            lblQuantity.Visibility = Visibility.Collapsed;
            tbxQuantity.Visibility = Visibility.Collapsed;
            lblRequired.Visibility = Visibility.Visible;
            chbxRequired.Visibility = Visibility.Visible;
        }

        private void chbxDocument_Unchecked(object sender, RoutedEventArgs e)
        {
            lblQuantity.Visibility = Visibility.Visible;
            tbxQuantity.Visibility = Visibility.Visible;
            lblRequired.Visibility = Visibility.Collapsed;
            chbxRequired.Visibility = Visibility.Collapsed;
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isEuropeanUser = false;
            bool isEuropeanCountry = false;
            bool isPassportAdded = false;
            string country = cbCountries.SelectedItem as string;

            foreach (EuropeanCountries europeanCountry in Enum.GetValues(typeof(EuropeanCountries)))
            {
                if (user.Location.ToString() == europeanCountry.ToString())
                {
                    isEuropeanUser = true;
                }
            }

            foreach (EuropeanCountries europeanCountry in Enum.GetValues(typeof(EuropeanCountries)))
            {
                if (country == europeanCountry.ToString())
                {
                    isEuropeanCountry = true;
                }
            }

            foreach (IPackingListItem packingListItem in packingList)
            {
                if (packingListItem.Name == "Passport")
                {
                    isPassportAdded = true;
                }
            }

            if (isEuropeanCountry && isEuropeanUser)
            {
                if (!isPassportAdded)
                {
                    TravelDocument newTravelDocument = new("Passport", false);
                    packingList.Add(newTravelDocument);
                }

                lvPackingList.Items.Clear();

                foreach (IPackingListItem packingListItem in packingList)
                {
                    ListViewItem item = new();
                    item.Content = packingListItem.ToString();
                    item.Tag = packingListItem;
                    lvPackingList.Items.Add(item);
                }
            }

            else if (!isEuropeanCountry && isEuropeanUser)
            {
                if (!isPassportAdded)
                {
                    TravelDocument newTravelDocument = new("Passport", true);
                    packingList.Add(newTravelDocument);
                }

                lvPackingList.Items.Clear();

                foreach (IPackingListItem packingListItem in packingList)
                {
                    ListViewItem item = new();
                    item.Content = packingListItem.ToString();
                    item.Tag = packingListItem;
                    lvPackingList.Items.Add(item);
                }
            }

            else if (!isEuropeanUser)
            {
                if (!isPassportAdded)
                {
                    TravelDocument newTravelDocument = new("Passport", true);
                    packingList.Add(newTravelDocument);
                }

                lvPackingList.Items.Clear();

                foreach (IPackingListItem packingListItem in packingList)
                {
                    ListViewItem item = new();
                    item.Content = packingListItem.ToString();
                    item.Tag = packingListItem;
                    lvPackingList.Items.Add(item);
                }
            }
        }
    }
}
