using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        private TravelManager travelManager;
        private User user;

        public TravelsWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.user = userManager.signedInUser as User;
            this.userManager = userManager;
            this.travelManager = travelManager;

            lblUsername.Content = user.Username;

            DisableDetailsAndRemoveButtons();
            PopulateUserListView();
        }

        private void PopulateUserListView()
        {
            lvTravels.Items.Clear();

            foreach (Travel travel in user.travels)
            {
                ListViewItem item = new();
                item.Content = travel.GetInfo();
                item.Tag = travel;
                lvTravels.Items.Add(item);
            }
        }

        private void DisableDetailsAndRemoveButtons()
        {
            btnDetails.IsEnabled = false;
            btnRemove.IsEnabled = false;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(userManager, travelManager);
            mainWindow.Show();
            Close();
        }

        private void btnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new(userManager, travelManager);
            userDetailsWindow.Show();
            Close();
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new(userManager, travelManager);
            addTravelWindow.Show();
            Close();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvTravels.SelectedItem as ListViewItem;
            Travel selectedTravel = selectedItem.Tag as Travel;

            TravelDetailsWindow travelDetailsWindow = new(userManager, selectedTravel, travelManager);
            travelDetailsWindow.Show();
            Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvTravels.SelectedItem as ListViewItem;
            Travel selectedTravel = selectedItem.Tag as Travel;

            user.travels.Remove(selectedTravel);
            travelManager.RemoveTravel(selectedTravel);
            PopulateUserListView();
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

        private void lvTravels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDetails.IsEnabled = true;
            btnRemove.IsEnabled = true;
        }
    }
}
