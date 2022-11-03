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

        //Clears and populates the travel-listview with the user's travels
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

        //Disables the Detaisl- and Remove-buttons
        private void DisableDetailsAndRemoveButtons()
        {
            btnDetails.IsEnabled = false;
            btnRemove.IsEnabled = false;
        }

        //Sends the user back to the MainWindow when clicking the Return-button and closes the TravelsWindow
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(userManager, travelManager);
            mainWindow.Show();
            Close();
        }

        //Sends the user to the UserDetailsWindow and closes the TravelsWindow when clicking the EditAccount-button
        private void btnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new(userManager, travelManager);
            userDetailsWindow.Show();
            Close();
        }

        //Sends the user to the AddTravelWindow and closes the TravelWindow when clicking the AddTravel-button
        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new(userManager, travelManager);
            addTravelWindow.Show();
            Close();
        }

        //Sends the user and the clicked object to the TravelDetailsWindow and closes the TravelsWindow when clicking the Details-button
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvTravels.SelectedItem as ListViewItem;
            Travel selectedTravel = selectedItem.Tag as Travel;

            TravelDetailsWindow travelDetailsWindow = new(userManager, selectedTravel, travelManager);
            travelDetailsWindow.Show();
            Close();
        }

        //Removes the selected listview-travel from the user's travelslist and the TravelManagers' travel-list
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvTravels.SelectedItem as ListViewItem;
            Travel selectedTravel = selectedItem.Tag as Travel;

            user.travels.Remove(selectedTravel);
            travelManager.RemoveTravel(selectedTravel);
            PopulateUserListView();
        }

        //Displays a messagebox with info about the TravelPal agency
        private void btnAboutUs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your go-to traveling agency, making dreams come true since 1992!", "About Us", MessageBoxButton.OK);   
        }

        //Displays a messagebox with info about how to use the application
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("-Use the Add-button for adding new travels.\n" +
                "-Use the Details-button to show detailed info about a selected travel in the list.\n" +
                "-Use the Remove-button to remove a selected travel from the list.\n" +
                "-Use the Edit Account-button to edit profile settings (username, password etc.).", "Help", MessageBoxButton.OK);
        }

        //Enables Details- and Remove-buttons when an item in the listview is selected
        private void lvTravels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDetails.IsEnabled = true;
            btnRemove.IsEnabled = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
