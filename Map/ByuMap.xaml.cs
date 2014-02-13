using Bing.Maps.VenueMaps;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Map
{
    public sealed partial class ByuMap : UserControl
    {
        private const string ByuVenueId = "hcl-brighamyounguniversity";
        private VenueMap ByuVenue;

        private const string StadiumId = "hcl-lavelledwardsstadium";

        public ByuMap()
        {
            this.InitializeComponent();

            SetupMapAsync();
        }

        public async void SetupMapAsync()
        {
            ByuVenue = await this.BingMap.VenueManager.CreateVenueMapAsync(ByuVenueId);
            BingMap.VenueManager.ActiveVenue = ByuVenue;

            BingMap.VenueManager.ActiveVenueChanged += VenueManager_ActiveVenueChanged;
        }

        void VenueManager_ActiveVenueChanged(object sender, ActiveVenueChangedEventArgs e)
        {
            BingMap.VenueManager.ActiveVenue = ByuVenue;
        }
    }
}
