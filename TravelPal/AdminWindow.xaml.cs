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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private UserManager userManager;
        private List<IUser> users;
        private Admin admin;
        public AdminWindow(UserManager userManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.userManager = userManager;
            users = userManager.GetUsers();
            admin = userManager.signedInUser as Admin;

            lblUsername.Content = admin.Username;

            foreach (IUser user in users)
            {
                if (user is User)
                {
                    cbUsers.Items.Add(user.Username);
                }
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(userManager);
            mainWindow.Show();
            Close();    
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
