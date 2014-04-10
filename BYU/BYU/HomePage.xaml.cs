using BYU.Common;
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

namespace BYU
{
    public sealed partial class HomePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private const string userPhotoName = "userPhoto.jpg";
        //private Uri userPhotoUri;
        PersonSummaryResponse userInfo;

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public HomePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // Restore values stored in session state.
            if (e.PageState != null)
            {
                if (AuthenticationManager.LoggedIn())
                {
                    userInfo = await PersonSummaryResponse.GetPersonSummary();
                    // userPhotoUri = await PersonPhoto.getPhotoUri();
                    await LoadUserPhoto();
                    await PopulateClasses();
                }

                SetElementEnableStatuses();
            }

            // Restore values stored in app data.
            
            //Just leaving an example below to mimic later on if needed
            /*Windows.Storage.ApplicationDataContainer roamingSettings =
                Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Values.ContainsKey("userName"))
            {
                nameInput.Text = roamingSettings.Values["userName"].ToString();
            }*/
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e){ }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


        private void MapButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MapPage));
        }

        private void ParkingButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ParkingPage));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }

        public async void DoLogin()
        {
            var netID = this.LoginNameTextbox.Text;
            var password = this.LoginPasswordTextbox.Password;

            bool success = false;
            //try
            //{
                ProgressBar.Visibility = Visibility.Visible;
                SignInButton.IsEnabled = false;
                LoginNameTextbox.IsEnabled = false;
                LoginPasswordTextbox.IsEnabled = false;
                //AuthenticationManager.Login(netID, password);
                WebServiceSession session = await WebServiceSession.GetSession(netID, password);
                success = session != null;
            //}
            //catch (InvalidCredentialsException){ }

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

            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => );
            WebServiceSession webServiceSession = await Task.Run(() =>
            {
                return WebServiceSession.GetSession(netID, password);
            });
            if (webServiceSession != null)
            {
                this.userInfo = await PersonSummaryResponse.GetPersonSummary();

                //userPhotoUri = await PersonPhoto.getPhotoUri();
                await LoadUserPhoto();
                var vault = new Windows.Security.Credentials.PasswordVault();
                vault.Add(new Windows.Security.Credentials.PasswordCredential(
                    "byu.edu", LoginNameTextbox.Text, LoginPasswordTextbox.Password));
                await PopulateClasses();
                LoginNameTextbox.Text = "";
                LoginPasswordTextbox.Password = "";
            }
            else
            {
                var messageDialog = new MessageDialog("Username and Password are incorrect. Please try again.");
                await messageDialog.ShowAsync();
            }

            SetElementEnableStatuses();
            ProgressBar.Visibility = Visibility.Collapsed;
        }

        private void SetElementEnableStatuses()
        {
            bool loggedIn = AuthenticationManager.LoggedIn();
            var credential = AuthenticationManager.credential;
            if (loggedIn)
            {
                if (userInfo != null)
                {
                    this.UserButton.Text = userInfo.names.preferred_name;
                }
                else
                {
                    this.UserButton.Text = credential.UserName;
                }
            }
            this.LoginNameTextbox.IsEnabled = !loggedIn;
            this.LoginPasswordTextbox.IsEnabled = !loggedIn;
            this.SignInButton.IsEnabled = !loggedIn;
            this.UserStack.Visibility = loggedIn ? Visibility.Visible : Visibility.Collapsed;
            this.LoginSection.Visibility = loggedIn ? Visibility.Collapsed : Visibility.Visible;
            this.ClassesSection.Visibility = loggedIn ? Visibility.Visible : Visibility.Collapsed;
        }

        private async Task LoadUserPhoto()
        {
            if ((await PersonPhoto.getPhotoUri()) != null)
            {
                UserImage.Source = new BitmapImage(await PersonPhoto.getPhotoUri());
            }
            else
            {
                UserImage.Source = null;
            }
        }

        private async Task PopulateClasses()
        {
            CourseScheduleInformation classes = await ClassScheduleRoot.GetClassSchedule();
            ClassesListView.ItemsSource = new ObservableCollection<CourseInformation>(classes.courseList);        
        }

        private void PasswordTextbox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !this.LoginNameTextbox.Text.Equals(""))
            {
                DoLogin();
            }
        }

        private void ClassButton_Click(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(ClassesPage), ((CourseInformation)e.ClickedItem));
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            //Windows.UI.ApplicationSettings.SettingsPane.Show();
            this.Frame.Navigate(typeof(UserProfile), userInfo);
        }

        private async void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (AuthenticationManager.LoggedIn())
            {
                this.userInfo = await PersonSummaryResponse.GetPersonSummary();
                //userPhotoUri = await PersonPhoto.getPhotoUri();
                await LoadUserPhoto();
                await PopulateClasses();
            }

            SetElementEnableStatuses();
        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            /*var vault = new Windows.Security.Credentials.PasswordVault();

            try {
                foreach (PasswordCredential pass in vault.FindAllByResource("byu.edu")){
                    vault.Remove(pass);
                } 
            }
            catch (Exception ex) { }*/
            await AuthenticationManager.Logout();
            SetElementEnableStatuses();
        }
    }
}
