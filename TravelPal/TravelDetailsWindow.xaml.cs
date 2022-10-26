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
        public TravelDetailsWindow(UserManager userManager)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.userManager = userManager;
            this.user = userManager.signedInUser as User;
            lblUsername.Content = user.Username;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(userManager);
            travelsWindow.Show();
            Close();
        }
    }
}
