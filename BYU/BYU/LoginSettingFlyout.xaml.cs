using Common.WebServices.DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace BYU
{
    public sealed partial class LoginSettingFlyout : SettingsFlyout
    {
        public LoginSettingFlyout()
        {
            this.InitializeComponent();
        }

        private async void LoginPasswordTextbox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                await DoLogin();
            }
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            await DoLogin();
        }

        private async Task DoLogin()
        {
            WebServiceSession session = await WebServiceSession.GetSession(LoginNameTextbox.Text, LoginPasswordTextbox.Password);

            if (session == null)
            {
                MessageDialog messageDialog = new MessageDialog("Username and Password are incorrect. Please try again.");
                await messageDialog.ShowAsync();
            }
            else
            {
                var vault = new Windows.Security.Credentials.PasswordVault();
                vault.Add(new Windows.Security.Credentials.PasswordCredential(
                    "byu.edu", LoginNameTextbox.Text, LoginPasswordTextbox.Password));
                
                this.Hide();
                ((App)App.Current).GoHome();
            }
        }
    }
}
