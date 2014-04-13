using Common.WebServices.DO;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace BYU
{
    public sealed partial class LoginSettingFlyout : SettingsFlyout
    {
        public LoginSettingFlyout()
        {
            this.InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }

        private async void DoLogin()
        {
            var netID = this.LoginNameTextbox.Text;
            var password = this.LoginPasswordTextbox.Password;

            
            ProgressBar.Visibility = Visibility.Visible;
            SignInButton.IsEnabled = false;
            LoginNameTextbox.IsEnabled = false;
            LoginPasswordTextbox.IsEnabled = false;
            WebServiceSession session = await WebServiceSession.GetSession(netID, password);
            bool success = session != null;

            if (!success)
            {
                var messageDialog = new MessageDialog("Username and Password are incorrect. Please try again.");
                await messageDialog.ShowAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                SignInButton.IsEnabled = true;
                LoginNameTextbox.IsEnabled = true;
                LoginPasswordTextbox.IsEnabled = true;
                return;
            }

            if (session == null)
            {
                var messageDialog = new MessageDialog("Username and Password are incorrect. Please try again.");
                await messageDialog.ShowAsync();
            }
            else
            {
                this.Hide();

                // revert back to start screen ?
                var vault = new Windows.Security.Credentials.PasswordVault();
                vault.Add(new Windows.Security.Credentials.PasswordCredential(
                    "byu.edu", netID, password));
                App.RootFrame.Navigate(typeof(HomePage));

            }
        }

        private void PasswordTextbox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !this.LoginNameTextbox.Text.Equals(""))
            {
                DoLogin();
            }
        }
    }
}
