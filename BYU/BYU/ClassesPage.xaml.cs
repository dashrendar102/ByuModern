using BYU.Common;
using BYU.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.UI;
using Common.WebServices.DO;
using Common.WebServices.DO.ClassSchedule;
using Common.WebServices.DO.TermUtility;
using Common.WebServices;
using System.Threading.Tasks;


// The Item Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace BYU
{
    /// <summary>
    /// A page that displays details for a single item within a group.
    /// </summary>
    public sealed partial class ClassesPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private ClassScheduleResponse user_classes = null;

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

        /// <summary>
        /// Initializes the page
        /// </summary>
        public ClassesPage()
        {
            Init();
            selectedClassContent.Visibility = Visibility.Collapsed; 
        }

        public ClassesPage(String className){
            Init();
            selectedClassTitle.Text = className;
        }

        private void Init(){
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.LoadClasses();  
        }

        /// <summary>
        /// Requires authentication. Obtains class list from web service and loads menu.
        /// </summary>
        private async void LoadClasses()
        {
            ObservableCollection<Button> class_buttons = new ObservableCollection<Button>();
            
            user_classes = await Task.Run(() => {
                return ClassScheduleRoot.GetClassSchedule();
            });

            for (int i = 0; i < user_classes.courseList.Count(); i++)
            {
                Button button = new Button();
                button.Content = user_classes.courseList.ElementAt(i).course;
                button.Click += ClassClick;
                button.Height = 70;
                button.Width = 455;
                button.FontSize = 24;
                button.Margin = new Thickness(0);
                button.Foreground = new SolidColorBrush(Colors.White);
                button.Background = new SolidColorBrush(Color.FromArgb(255, 00, 34, 85));
                class_buttons.Add(button);
            }
            classList.DataContext = class_buttons;

            // show overview panel
        }
       
        /// <summary>
        /// Retrieves course information from class list data structure.
        /// </summary>
        /// <param name="i"></param>
        private void loadCourseInfo(int i)
        {

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
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var item = await SampleDataSource.GetItemAsync((String)e.NavigationParameter);
            this.DefaultViewModel["Item"] = item;
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
            var title = e.Parameter as String;
            selectedClassTitle.Text = title;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ClassClick(object sender, RoutedEventArgs e)
        {
            Button test = (Button)sender;
            if (selectedClassTitle.Text.Equals((String)test.Content))
            {
                selectedClassContent.Visibility = Visibility.Collapsed;
            } else selectedClassContent.Visibility = Visibility.Visible;

            selectedClassTitle.Text = (String)test.Content;
        }
    }
}