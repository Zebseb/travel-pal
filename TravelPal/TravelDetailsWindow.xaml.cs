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
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        private User user;
        private UserManager userManager;
        private TravelManager travelManager;
        private Travel travel;
        private Vacation vacation;
        private Trip trip;

        public TravelDetailsWindow(UserManager userManager, Travel selectedTravel, TravelManager travelManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.travelManager = travelManager;
            this.travel = selectedTravel;
            this.userManager = userManager;
            this.user = userManager.signedInUser as User;

            lblUsername.Content = user.Username;

            PopulatePackingListListView();
            DisableTextBoxes();
            CollapseTextBoxesAndLabels();
            SetTextboxesToTravelDetails();
            SetTextBoxInfo();
        }

        private void PopulatePackingListListView()
        {
            foreach (IPackingListItem packingListItem in travel.PackingList)
            {
                ListViewItem item = new();
                item.Content = packingListItem.ToString();
                item.Tag = packingListItem;
                lvPackingList.Items.Add(packingListItem);
                lvPackingList.IsEnabled = false;
            }
        }

        //Sets and displays UI depending on what kind of Travel the selectedTravel is
        private void SetTextBoxInfo()
        {
            if (travel is Vacation)
            {
                vacation = travel as Vacation;
                lblAllInclusive.Visibility = Visibility.Visible;
                tbxAllInclusive.Visibility = Visibility.Visible;
                tbxAllInclusive.Text = vacation.AllInclusive.ToString();
                tbxTravelType.Text = "Vacation";
            }

            else if (travel is Trip)
            {
                trip = travel as Trip;
                lblTripType.Visibility = Visibility.Visible;
                tbxTripType.Visibility = Visibility.Visible;
                tbxTripType.Text = trip.Type.ToString();
                tbxTravelType.Text = "Trip";
            }
        }

        //Sets textboxes to the variables all Travels's have
        private void SetTextboxesToTravelDetails()
        {
            tbxNumOfTravelers.Text = travel.Travelers.ToString();
            tbxDeparture.Text = travel.Country.ToString();
            tbxDestination.Text = travel.Destination;
            tbxStartDate.Text = travel.GetFormattedStartDate();
            tbxEndDate.Text = travel.GetFormattedEndDate();
            tbxDays.Text = travel.TravelDays.ToString();
        }

        //Collapses UI (before getting information about what kind of Travel the selectedTravel is)
        private void CollapseTextBoxesAndLabels()
        {
            lblTripType.Visibility = Visibility.Collapsed;
            tbxTripType.Visibility = Visibility.Collapsed;
            lblAllInclusive.Visibility = Visibility.Collapsed;
            tbxAllInclusive.Visibility = Visibility.Collapsed;
        }

        //Disables all textboxes
        private void DisableTextBoxes()
        {
            tbxNumOfTravelers.IsEnabled = false;
            tbxDeparture.IsEnabled = false;
            tbxDestination.IsEnabled = false;
            tbxTravelType.IsEnabled = false;
            tbxAllInclusive.IsEnabled = false;
            tbxTripType.IsEnabled = false;
            tbxStartDate.IsEnabled = false;
            tbxEndDate.IsEnabled = false;
            tbxDays.IsEnabled = false;
        }

        //Sends the user back to the TravelsWindow and closes TravelDetailsWindow when clicking the Return-button
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(userManager, travelManager);
            travelsWindow.Show();
            Close();
        }
    }
}
