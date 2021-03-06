﻿using BYU.Common;
using BYU.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Security.Credentials;
using Common.Authentication;
using Common.WebServices.DO.PersonSummary;
using Common.WebServices.DO.ClassSchedule;
using Common.WebServices.DO;
using Common.WebServices;
using Common;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.Storage;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace BYU
{
    public sealed partial class LoginSettingFlyout : SettingsFlyout
    {
        public LoginSettingFlyout()
        {
            this.InitializeComponent();
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            var netID = this.LoginNameTextbox.Text;
            var password = this.LoginPasswordTextbox.Password;

            bool success = false;
            try
            {
                ProgressBar.Visibility = Visibility.Visible;
                SignInButton.IsEnabled = false;
                LoginNameTextbox.IsEnabled = false;
                LoginPasswordTextbox.IsEnabled = false;
                WebServiceSession session = await WebServiceSession.GetSession(netID, password);
                success = session != null;
            }
            catch (Exception) {
                success = false;
            }

            if (!success)
            {
                MessageTextbox.Visibility = Visibility.Visible;
                ProgressBar.Visibility = Visibility.Collapsed;
                SignInButton.IsEnabled = true;
                LoginNameTextbox.IsEnabled = true;
                LoginPasswordTextbox.IsEnabled = true;
                return;
            }

            var vault = new Windows.Security.Credentials.PasswordVault();
            vault.Add(new Windows.Security.Credentials.PasswordCredential(
                "byu.edu", LoginNameTextbox.Text, LoginPasswordTextbox.Password));

            App.GoHome();

            this.Hide();
        }
    }
}
