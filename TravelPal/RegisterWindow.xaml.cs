﻿using System;
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

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            foreach (Countries country in Enum.GetValues(typeof(Countries)))
            {
                cbCountries.Items.Add(country);
            }
        }

        private void chbxShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            tbxPasswordBox.Text = pabxPasswordBox.Password;
            tbxPasswordBox2.Text = pabxPasswordBox2.Password;
            pabxPasswordBox.Visibility = Visibility.Collapsed;
            pabxPasswordBox2.Visibility = Visibility.Collapsed;
            tbxPasswordBox.Visibility = Visibility.Visible;
            tbxPasswordBox2.Visibility = Visibility.Visible;
        }

        private void chbxShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            pabxPasswordBox.Password = tbxPasswordBox.Text;
            pabxPasswordBox2.Password = tbxPasswordBox2.Text;
            tbxPasswordBox.Visibility = Visibility.Collapsed;
            tbxPasswordBox2.Visibility = Visibility.Collapsed;
            pabxPasswordBox.Visibility = Visibility.Visible;
            pabxPasswordBox2.Visibility = Visibility.Visible;
        }
    }
}
