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
        private List<Travel> travels;
        private UserManager userManager;
        private List<IUser> users;
        private List<User> userUsers = new();
        private Admin admin;
        private User user = new();

        public AdminWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.travelManager = travelManager;
            this.travels = travelManager.GetTravels();

            this.userManager = userManager;
            users = userManager.GetUsers();
            admin = userManager.signedInUser as Admin;

            lblUsername.Content = admin.Username;

            foreach (IUser user in users)
            {
                if (user is User)
                {
                    this.user = user as User;
                    userUsers.Add(this.user);
                }
            }

            foreach (User user in userUsers)
            {
                foreach (Travel travel in user.travels)
                {
                    ListViewItem item = new();
                    item.Content = travel.GetInfo();
                    item.Tag = travel;
                    lvUserTravels.Items.Add(item);
                }
            }

            foreach (IUser user in users)
            {
                if (user is User)
                {
                    cbUsers.Items.Add(user.Username);
                }
            }

            //foreach (Travel travel in travels)
            //{
            //    ListViewItem item = new();
            //    item.Content = travel.GetInfo();
            //    item.Tag = travel;
            //    lvUserTravels.Items.Add(item);
            //}
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(userManager, travelManager);
            mainWindow.Show();
            Close();    
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
