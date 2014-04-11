﻿using BYU.Common;
using Common.Calendar;
using Common.WebServices;
using Common.WebServices.DO.ClassSchedule;
using Common.WebServices.DO.LearningSuite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Linq;


// The Item Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace BYU
{
    /// <summary>
    /// A page that displays details for a single item within a group.
    /// </summary>
    public sealed partial class ClassesPage : Page
    {
        private const string SELECTED_COURSE_KEY = "selected_course";

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private CourseInformation selectedCourse = null;

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
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Requires authentication. Obtains class list from web service and loads menu.
        /// </summary>
        private async void LoadClasses()
        {
            this.ScheduleInformation = await ClassScheduleRoot.GetClassSchedule();
            ObservableCollection<CourseInformation> courses = new ObservableCollection<CourseInformation>(this.ScheduleInformation.courseList);
            ClassesListView.ItemsSource = courses;
            ClassesListView.SelectedItem = selectedCourse;
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
            if (e.PageState != null && e.PageState.ContainsKey(SELECTED_COURSE_KEY))
            {
                await this.SetSelectedCourse((CourseInformation)e.PageState[SELECTED_COURSE_KEY]);
            }

            // TODO: Create an appropriate data model for your problem domain to replace the sample data
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            selectedCourse = ClassesListView.SelectedItem as CourseInformation;
            e.PageState.Add(SELECTED_COURSE_KEY, ClassesListView.SelectedItem);
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

            if (e.NavigationMode != NavigationMode.Back)
            {
                await SetSelectedCourse(e.Parameter as CourseInformation);
            }

            LoadClasses();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            selectedCourse = ClassesListView.SelectedItem as CourseInformation;
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ClassButton_Click(object sender, SelectionChangedEventArgs e)
        {
            await this.SetSelectedCourse((CourseInformation)e.AddedItems[0]);
        }

        /// <summary>
        /// Sets the currently selected course and loads course information into summary.
        /// </summary>
        /// <param name="newCourse"></param>
        private async Task SetSelectedCourse(CourseInformation newCourse)
        {
            selectedCourse = newCourse;
            SelectedClassContent.DataContext = selectedCourse;
            SelectedClassSummary.DataContext = selectedCourse;
            await setSelectedAnnouncements();
            await loadAssignmentInfo();
        }

        private async Task setSelectedAnnouncements()
        {
            if (selectedCourse.LearningSuiteCourseInformation == null)
            {
                AnnouncementsList.ItemsSource = null;
            }
            else
            {
                String courseID = selectedCourse.LearningSuiteCourseInformation.CourseID;
                Announcement[] announcements = await Announcement.GetAnnouncements(courseID);
                foreach (Announcement announcement in announcements)
                {
                    announcement.text = StringUtils.ExtractAndPrettifyHTMLText(announcement.text);
                }
                AnnouncementsList.ItemsSource = new ObservableCollection<Announcement>(announcements);
            }
        }

        private async Task loadAssignmentInfo()
        {
            if (selectedCourse.LearningSuiteCourseInformation == null)
            {
                UpcomingAssignmentsList.ItemsSource = null;
            }
            else
            {
                string courseID = selectedCourse.LearningSuiteCourseInformation.CourseID;
                Assignment[] assignments = await Assignment.GetUpcomingAssignments(courseID);
                UpcomingAssignmentsList.ItemsSource = new ObservableCollection<Assignment>(assignments); ;
            }
        }

        /// <summary>
        /// Adds class to Windows 8 Calendar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void UpcomingAssignmentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var parameters = Tuple.Create(selectedCourse, UpcomingAssignmentsList.SelectedValue as Assignment);
            this.Frame.Navigate(typeof(AssignmentDetail), parameters);
        }

        private void ShowOnMapButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            CourseInformation course = (CourseInformation)b.DataContext;
            Frame.Navigate(typeof(MapPage), course.building);
        }
    }
}