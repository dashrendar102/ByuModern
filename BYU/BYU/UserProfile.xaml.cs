using BYU.Common;
using Common.WebServices.DO;
using Common.WebServices.DO.IdCard;
using Common.WebServices.DO.PersonSummary;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace BYU
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class UserProfile : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private PersonSummaryResponse userResponse;
        private IdCardResponse idResponse;

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public UserProfile()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            
            this.userResponse = e.Parameter as PersonSummaryResponse;
            this.idResponse = await IdCardRoot.GetIdCard();

            if (idResponse.beard && idResponse.dtf)
            {
                SetIdCardTemplate("Assets/idCardBeardDTF.png");
            }
            else if (idResponse.beard && !idResponse.dtf)
            {
                SetIdCardTemplate("Assets/idCardBeard.png");
            }
            else if (!idResponse.beard && idResponse.dtf)
            {
                SetIdCardTemplate("Assets/idCardDTF.png");
            }

            userPicture.Source = new BitmapImage(await PersonPhoto.GetPhotoUriAsync());

            idCardCanvas.DataContext = idResponse;

            PersonalInfoStack.DataContext = userResponse.personal_information;

            completeName.DataContext = userResponse.names;
            netId.DataContext = userResponse.identifiers;

            EmployeeInfo.DataContext = userResponse.employee_information;
            empDate.DataContext = userResponse.employee_information.date_hired;
            qualification.DataContext = userResponse.employee_information.date_hired;

            ContactInfoStack.DataContext = userResponse.contact_information;
            TextBlock[] mailingAddressBoxes = new TextBlock[] { mailingAddress, mailingAddress2, mailingAddress3 };
            int count = 0;
            foreach (string str in userResponse.contact_information.mailing_address){
                mailingAddressBoxes[count].Text = str;
                count++;
            }
            if (userResponse.contact_information.mailing_phone_unlisted)
            {
                mailPhoneUnlist.Text = " (Unlisted)";
            }
            if (userResponse.contact_information.email_address_unlisted)
            {
                emailUnlist.Text = " (Unlisted)";
            }
            if (userResponse.contact_information.mailing_address_unlisted)
            {
                mailingAddressUnlist.Text = " (Unlisted)";
            }
        }

        private void SetIdCardTemplate(string path)
        {
            BitmapImage template = new BitmapImage();
            template.UriSource = new Uri(this.BaseUri, path);
            idCardTemplate.Source = template;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 549)
            {
                idCardCanvas.Margin = new Thickness(-220, 100, 0, 0);
                Logo1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Logo2.Visibility = Windows.UI.Xaml.Visibility.Visible;
                DetailsScrollViewer.VerticalScrollMode = ScrollMode.Enabled;
                DetailsScrollViewer.Width = 450;
                DetailsScrollViewer.Margin = new Thickness(-200, 0, 0, 0);
            }
            else if (e.NewSize.Width < 585)
            {
                idCardCanvas.Margin = new Thickness(-140, 10, 0, 0);
                Logo1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Logo2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                DetailsScrollViewer.VerticalScrollMode = ScrollMode.Disabled;
                DetailsScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                DetailsScrollViewer.Width = 525;
                DetailsScrollViewer.Margin = new Thickness(-225, 0, 0, 0);
            }
            else if (e.NewSize.Width < 675)
            {
                idCardCanvas.Margin = new Thickness(-120, 10, 0, 0);
                Logo1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Logo2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                DetailsScrollViewer.VerticalScrollMode = ScrollMode.Disabled;
                DetailsScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                DetailsScrollViewer.Width = 600;
                DetailsScrollViewer.Margin = new Thickness(-175, 0, 0, 0);
            }
            else if (e.NewSize.Width < 1008)
            {
                idCardCanvas.Margin = new Thickness(-40, 10, 0, 0);
                Logo1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Logo2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                DetailsScrollViewer.VerticalScrollMode = ScrollMode.Disabled;
                DetailsScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                DetailsScrollViewer.Width = 700;
                DetailsScrollViewer.Margin = new Thickness(-125, 0, 0, 0);
            }
            else
            {
                idCardCanvas.Margin = new Thickness(0, 10, 0, 0);
                Logo1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Logo2.Visibility = Windows.UI.Xaml.Visibility.Visible;
                DetailsScrollViewer.VerticalScrollMode = ScrollMode.Disabled;
                DetailsScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                DetailsScrollViewer.Width = Double.NaN;
                DetailsScrollViewer.Margin = new Thickness(0, 0, 0, 0);
            }
        }
    }
}
