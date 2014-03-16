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
using Common.Calendar;


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
        private CourseInformation selectedCourse = null;
        private ObservableCollection<CourseInformation> selected_class_list =
                    new ObservableCollection<CourseInformation>();

        public CourseScheduleInformation ScheduleInformation { get; private set; }

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
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;

        }

        private void SetSelectedCourse(CourseInformation newCourse)
        {
            selectedCourse = newCourse;
            SelectedClassContent.DataContext = selectedCourse;
        }

        /// <summary>
        /// Requires authentication. Obtains class list from web service and loads menu.
        /// </summary>
        private async void LoadClasses()
        {
            this.ScheduleInformation = await ClassScheduleRoot.GetClassSchedule();
            ObservableCollection<CourseInformation> courses = new ObservableCollection<CourseInformation>(this.ScheduleInformation.courseList);
            ClassesListView.ItemsSource = courses;
            foreach (CourseInformation course in courses)
            {
                if (course.curriculum_id == selectedCourse.curriculum_id)
                {
                    ClassesListView.SelectedItem = course;
                }
            }
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
            SetSelectedCourse(e.Parameter as CourseInformation);
            LoadClasses();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ClassButton_Click(object sender, SelectionChangedEventArgs e)
        {
            SetSelectedCourse((CourseInformation)e.AddedItems[0]);
        }

        private async void btnAddClass_Click(object sender, RoutedEventArgs e)
        {
            AppointmentGenerator generator = new AppointmentGenerator();
            var appointment = await generator.GenerateAppointment(ScheduleInformation, selectedCourse);

            var rect = GetElementRect(sender as FrameworkElement);
            //TODO - store this id in some way so we know which classes have already been exported
            String appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(appointment, rect, Windows.UI.Popups.Placement.Default);
        }

        //taken from http://code.msdn.microsoft.com/windowsapps/Appointments-API-sample-2b55c76e
        private Windows.Foundation.Rect GetElementRect(FrameworkElement element)
        {
            Windows.UI.Xaml.Media.GeneralTransform buttonTransform = element.TransformToVisual(null);
            Windows.Foundation.Point point = buttonTransform.TransformPoint(new Windows.Foundation.Point());
            return new Windows.Foundation.Rect(point, new Windows.Foundation.Size(element.ActualWidth, element.ActualHeight));
        }
    }
}