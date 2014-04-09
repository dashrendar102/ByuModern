using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common;
using Bing.Maps;
using BYU.Common;
using BYU.Data;
using System.Threading.Tasks;
using Common.WebServices.DO.ParkingLots;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BYU
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ParkingPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private static MapShapeLayer parkingLayer;

        string[] ParkingLotTypes = new string[] {"All Lots", "Student (Y)", "Graduate (G)", "Faculty/Staff (A)", "Helaman Halls (C)","Restricted Visitor (R)", 
                 "Visitor (V)","Timed (T)", "Motorcycle (MOTOR)","Bike (BIKE)","Construction (CON)","Heritage Halls (B)"};

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public ParkingPage()
        {
            this.InitializeComponent();

            //set up the navigation
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            // Setup the logical page navigation components that allow
            // the page to only show one pane at a time.
            this.navigationHelper.GoBackCommand = new RelayCommand(() => this.GoBack(), () => this.CanGoBack());
            this.ParkingListView.SelectionChanged += ParkingListViewSelectionChanged;

            // Start listening for Window size changes 
            // to change from showing two panes to showing a single pane
            Window.Current.SizeChanged += Window_SizeChanged;
            this.InvalidateVisualState();
            
            //parkingLayer = new MapShapeLayer();
            //map.addShapeLayer(parkingLayer);

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
            var ParkingLots = await map.GetParkingLotsAsync();
            this.DefaultViewModel["Items"] = GetParkingLotTypes();
            map.DrawAllParkingLots();
            if (e.PageState == null)
            {
                this.ParkingListView.SelectedItem = null;
                // When this is a new page, select the first item automatically unless logical page
                // navigation is being used (see the logical page navigation #region below.)
                /*if (!this.UsingLogicalPageNavigation() && this.itemsViewSource.View != null)
                {
                    this.itemsViewSource.View.MoveCurrentToFirst();
                }*/
            }
            else
            {
                // Restore the previously saved state associated with this page
                if (e.PageState.ContainsKey("SelectedItem") && this.itemsViewSource.View != null)
                {
                    //var selectedItem = await SampleDataSource.GetItemAsync((String)e.PageState["SelectedItem"]);
                    //this.itemsViewSource.View.MoveCurrentTo(selectedItem);
                }
            }
        }

        private object GetParkingLotTypes()
        {
            List<ParkingData> myData = new List<ParkingData>();
            int typeID = 0;
            foreach (string lotType in ParkingLotTypes)
            {
                myData.Add(new ParkingData(lotType,typeID));
                typeID++;
            }
            return myData;
        }

        public class ParkingData
        {
            public string Name { get; set; }
            public int typeID { get; set; }
            public ParkingData(string name, int TypeID)
            {
                Name = name;
                typeID = TypeID;
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            if (this.itemsViewSource.View != null)
            {
                //var selectedItem = (Data.SampleDataItem)this.itemsViewSource.View.CurrentItem;
                //if (selectedItem != null) e.PageState["SelectedItem"] = selectedItem.UniqueId;
            }
        }


        #region Logical page navigation

        // The split page isdesigned so that when the Window does have enough space to show
        // both the list and the dteails, only one pane will be shown at at time.
        //
        // This is all implemented with a single physical page that can represent two logical
        // pages.  The code below achieves this goal without making the user aware of the
        // distinction.

        private const int MinimumWidthForSupportingTwoPanes = 768;

        /// <summary>
        /// Invoked to determine whether the page should act as one logical page or two.
        /// </summary>
        /// <returns>True if the window should show act as one logical page, false
        /// otherwise.</returns>
        private bool UsingLogicalPageNavigation()
        {
            return Window.Current.Bounds.Width < MinimumWidthForSupportingTwoPanes;
        }

        /// <summary>
        /// Invoked with the Window changes size
        /// </summary>
        /// <param name="sender">The current Window</param>
        /// <param name="e">Event data that describes the new size of the Window</param>
        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.InvalidateVisualState();
        }

        /// <summary>
        /// Invoked when an item within the list is selected.
        /// </summary>
        /// <param name="sender">The GridView displaying the selected item.</param>
        /// <param name="e">Event data that describes how the selection was changed.</param>
        private void ParkingListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList<object> results = ((ListView)sender).SelectedItems;
            if (results.Count != 0)
            {
                ParkingData lotType = (ParkingData)results[0];
                map.DrawParkingLotType(lotType.typeID);
            }
            else map.ResetView();
        }

        private bool CanGoBack()
        {
            if (this.UsingLogicalPageNavigation() && this.ParkingListView.SelectedItem != null)
            {
                return true;
            }
            else
            {
                return this.navigationHelper.CanGoBack();
            }
        }
        private void GoBack()
        {
            map.HideAllParkingLots();
            if (this.UsingLogicalPageNavigation() && this.ParkingListView.SelectedItem != null)
            {
                // When logical page navigation is in effect and there's a selected item that
                // item's details are currently displayed.  Clearing the selection will return to
                // the item list.  From the user's point of view this is a logical backward
                // navigation.
                this.ParkingListView.SelectedItem = null;
            }
            else
            {
                this.navigationHelper.GoBack();
            }
        }

        private void InvalidateVisualState()
        {
            var visualState = DetermineVisualState();
            VisualStateManager.GoToState(this, visualState, false);
            this.navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Invoked to determine the name of the visual state that corresponds to an application
        /// view state.
        /// </summary>
        /// <returns>The name of the desired visual state.  This is the same as the name of the
        /// view state except when there is a selected item in portrait and snapped views where
        /// this additional logical page is represented by adding a suffix of _Detail.</returns>
        private string DetermineVisualState()
        {
            if (!UsingLogicalPageNavigation())
                return "PrimaryView";

            // Update the back button's enabled state when the view state changes
            var logicalPageBack = this.UsingLogicalPageNavigation() && this.ParkingListView.SelectedItem != null;

            return logicalPageBack ? "SinglePane_Detail" : "SinglePane";
        }

        #endregion


        #region NavigationHelper registration

         ///The methods provided in this section are simply used to allow
         ///NavigationHelper to respond to the page's navigation methods.
         
         ///Page specific logic should be placed in event handlers for the  
         ///<see cref="GridCS.Common.NavigationHelper.LoadState"/>
         ///and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
         ///The navigation parameter is available in the LoadState method 
         ///in addition to page state preserved during an earlier session.

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is string)
            {
                string ParkingName = (string)e.Parameter;
                await SelectParkingByType(ParkingName);
            }
        }

        private async Task SelectParkingByType(string ParkingType)
        {
            var ParkingLots = await map.GetParkingLotsAsync();
            map.DrawParkingLotType(Array.IndexOf(this.ParkingLotTypes, ParkingType));
            this.ParkingListView.SelectedItem = ParkingType;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
