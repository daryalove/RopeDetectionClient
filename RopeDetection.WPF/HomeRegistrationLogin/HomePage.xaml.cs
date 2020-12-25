﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing.Drawing2D;
using System.Windows.Shapes;
using RopeDetection.WPF;
using RopeDetection.WPF.HomeRegistrationLogin;

namespace RopeDetection.WPF.HomeRegistrationLogin
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public void BtnAuthenticationClick(object sender, RoutedEventArgs e)
        {
            AuthenticationPage authentication = new AuthenticationPage();
            this.NavigationService.Navigate(authentication);
        }
        public void BtnRegistrationClick(object sender, RoutedEventArgs e)
        {
            RegistrationPage registration = new RegistrationPage();
            this.NavigationService.Navigate(registration);
        }

    }
}
