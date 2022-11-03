using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private TravelManager travelManager;
        private UserManager userManager;
        private List<IUser> users;
        private List<User> userUsers = new();
        private Admin admin;
        private User user = new();
        private Travel travelToRemove;
        private User userToGetTravelRemoved;
        private User selectedUser = new();

        public AdminWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.travelManager = travelManager;
            this.userManager = userManager;

            users = userManager.GetUsers();
            admin = userManager.signedInUser as Admin;

            lblUsername.Content = admin.Username;
            btnRemove.IsEnabled = false;

            PopulateUserUsersList();
            PopulateTravelsListView();
            PopulateUserComboBox();
        }

        //Populates the users-combobox
        private void PopulateUserComboBox()
        {
            cbUsers.Items.Add("-All Travels-");

            foreach (IUser user in users)
            {
                if (user is User)
                {
                    cbUsers.Items.Add(user.Username);
                }
            }

            cbUsers.SelectedIndex = 0;
        }

        //Populates the listview with all users' travels
        private void PopulateTravelsListView()
        {
            lvUserTravels.Items.Clear();

            foreach (User user in userUsers)
            {
                foreach (Travel travel in user.travels)
                {
                    ListViewItem item = new();
                    item.Content = $"Traveler: {user.Username} {travel.GetInfo()}";
                    item.Tag = travel;
                    lvUserTravels.Items.Add(item);
                }
            }
        }

        //Populates the listview with the selected user's travels
        private void PopulateSelectedUserTravelsListView()
        {
            lvUserTravels.Items.Clear();

            foreach (Travel travel in selectedUser.travels)
            {
                ListViewItem item = new();
                item.Content = $"Traveler: {selectedUser.Username} {travel.GetInfo()}";
                item.Tag = travel;
                lvUserTravels.Items.Add(item);
            }
        }

        //Populates the userUsers-list with all Users
        private void PopulateUserUsersList()
        {
            foreach (IUser user in users)
            {
                if (user is User)
                {
                    this.user = user as User;
                    userUsers.Add(this.user);
                }
            }
        }

        //Sends the user back to the MainWindow and closes the AdminWindow when clicking the Return-button
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(userManager, travelManager);
            mainWindow.Show();
            Close();    
        }

        //Removes the selected travel in the listview from the TravelManager's travels-list and from the user's travels-list
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvUserTravels.SelectedItem as ListViewItem;
            Travel selectedTravel = selectedItem.Tag as Travel;

            travelManager.RemoveTravel(selectedTravel);

            foreach (User user in userUsers)
            {
                foreach (Travel travel in user.travels)
                {
                    if (selectedTravel == travel)
                    {
                        userToGetTravelRemoved = user;
                        travelToRemove = travel;
                    }
                }
            }

            userToGetTravelRemoved.travels.Remove(travelToRemove);

            if (cbUsers.SelectedIndex == 0)
            {
                PopulateTravelsListView();
            }

            else
            {
                PopulateSelectedUserTravelsListView();
            }

            btnRemove.IsEnabled = false;
        }

        //Displays travels in listview depending on what item is selected in the combobox
        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvUserTravels.Items.Clear();

            if (cbUsers.SelectedIndex != 0)
            {
                foreach (User user in userUsers)
                {
                    if ((string)cbUsers.SelectedItem == user.Username)
                    {
                        selectedUser = user;
                    }
                }

                foreach (Travel travel in selectedUser.travels)
                {
                    ListViewItem item = new();
                    item.Content = $"Traveler: {selectedUser.Username} {travel.GetInfo()}";
                    item.Tag = travel;
                    lvUserTravels.Items.Add(item);
                }
            }

            else
            {
                PopulateTravelsListView();
            }
        }

        //Enables the Remove-Button when an item in the listview is selected
        private void lvUserTravels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                btnRemove.IsEnabled = true;
        }

        //Enables click and drag for the window's position
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //Minimizes the window when clicking "-"
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Closes the program when clicking "X"
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
