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
using Common.WebServices.DO.PersonSummary;
using Common.WebServices.DO.ClassSchedule;
using Common.WebServices.DO;
using Common.WebServices;
using Common;
using BYU.BergerDemos;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace BYU
{
    public sealed partial class HubPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private const string userPhotoName = "userPhoto.jpg";
        private Uri userPhotoUri;
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

        public HubPage()
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
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            //var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-6");
            //this.DefaultViewModel["Section3Items"] = sampleDataGroup;

            // Restore values stored in session state.
            if (e.PageState != null){
                if (e.PageState.ContainsKey("UserObject"))
                    userInfo = (PersonSummaryResponse)e.PageState["UserObject"];
                if (e.PageState.ContainsKey("UserPhoto"))
                {
                    userPhotoUri = (Uri)e.PageState["UserPhoto"];
                    LoadUserPhoto();
                    SetElementEnableStatuses();
                }
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
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            e.PageState["UserObject"] = userInfo;
            e.PageState["UserPhoto"] = userPhotoUri;
        }

        /// <summary>
        /// Invoked when a HubSection header is clicked.
        /// </summary>
        /// <param name="sender">The Hub that contains the HubSection whose header was clicked.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            HubSection section = e.Section;
            var group = section.DataContext;
            this.Frame.Navigate(typeof(SectionPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a section is clicked.
        /// </summary>
        /// <param name="sender">The GridView or ListView
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ClassesPage), itemId);
        }
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


        private void ClassButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ClassesPage));
        }

        private void MapButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MapPage));
        }

        private void BergerHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            //Demo demo = new Demo();
            //demo.doDemoStuff();
            this.Frame.Navigate(typeof(BergerDemoLand));
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            SignInButton.IsEnabled = false;
            LoginNameTextbox.IsEnabled = false;
            LoginPasswordTextbox.IsEnabled = false;

            var userName = LoginNameTextbox.Text;
            var password = LoginPasswordTextbox.Password;

            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => );
            WebServiceSession webServiceSession = await Task.Run(() =>
            {
                return WebServiceSession.GetSession(userName, password);
            });
            if (webServiceSession != null)
            {
                this.userInfo = PersonSummaryResponse.GetPersonSummary();

                userPhotoUri = PersonPhoto.getPhotoUri();
                LoadUserPhoto();
                var vault = new Windows.Security.Credentials.PasswordVault();
                vault.Add(new Windows.Security.Credentials.PasswordCredential(
                    "byu.edu", LoginNameTextbox.Text, LoginPasswordTextbox.Password));
                PopulateClasses();
            }
            else
            {
                var messageDialog = new MessageDialog("Username and Password are incorrect. Please try again.");
                await messageDialog.ShowAsync();
            }

            SetElementEnableStatuses();
        }

        private PasswordCredential GetBYUCredentials()
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            try
            {
                var credentialList = vault.FindAllByResource("byu.edu");
                return credentialList.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void SetElementEnableStatuses()
        {
            
            var credential = GetBYUCredentials();
            bool loggedIn = (credential != null);
            if (loggedIn)
            {
                if (userInfo != null)
                {
                    this.UserButton.Content = userInfo.names.preferred_name;
                }
                else
                {
                    this.UserButton.Content = credential.UserName;
                }
            }
            this.LoginNameTextbox.IsEnabled = loggedIn;
            this.LoginPasswordTextbox.IsEnabled = loggedIn;
            this.SignInButton.IsEnabled = loggedIn;
            this.UserButton.Visibility = loggedIn ? Visibility.Visible : Visibility.Collapsed;
            this.UserImage.Visibility = loggedIn ? Visibility.Visible : Visibility.Collapsed;
            this.LoginSection.Visibility = loggedIn ? Visibility.Collapsed : Visibility.Visible;
            this.ClassesSection.Visibility = loggedIn ? Visibility.Visible : Visibility.Collapsed;
            //this.LogoutButton.IsEnabled = !loggedIn;
        }

        private void LoadUserPhoto()
        {
            if (userPhotoUri != null)
            {
                UserImage.Source = new BitmapImage(userPhotoUri);
            }
            else
            {
                UserImage.Source = null;
            }
        }

        private void PopulateClasses()
        {
            //ClassScheduleResponse classes = ClassScheduleRoot.GetClassSchedule();
            
            //ClassesSection.ItemsSource = classes.courseList;
        }
    }
}
