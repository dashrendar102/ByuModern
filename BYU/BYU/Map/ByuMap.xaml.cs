using Bing.Maps.VenueMaps;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Map
{
    public sealed partial class ByuMap : UserControl
    {
        private VenueMap ByuVenue;
        Task MapInit = null;

        public ByuMap()
        {
            this.InitializeComponent();

            MapInit = SetupMapAsync();
        }

        async Task SetupMapAsync()
        {
            ByuVenue = await this.BingMap.VenueManager.CreateVenueMapAsync(Constants.ByuVenueId);
            BingMap.VenueManager.ActiveVenue = ByuVenue;

            BingMap.VenueManager.ActiveVenueChanged += VenueManager_ActiveVenueChanged;
        }

        void VenueManager_ActiveVenueChanged(object sender, ActiveVenueChangedEventArgs e)
        {
            BingMap.VenueManager.ActiveVenue = ByuVenue;
        }

        public async Task<IEnumerable<ByuMapEntity>> GetBuildings()
        {
            return await Task<IEnumerable<ByuMapEntity>>.Run(() =>
            {
                if (MapInit.IsCompleted || MapInit.Wait(TimeSpan.FromSeconds(10)))
                {
                    var buildings =
                        from ve in ByuVenue.Floors.SelectMany(x => x.VenueEntities)
                        where !String.IsNullOrWhiteSpace(ve.Name)
                        select new ByuMapEntity(ve.Name, ve.Description, ve);
                    return buildings;
                }
                else throw new TimeoutException("Could not load maps data");
            });
        }

        public void SelectEntity(ByuMapEntity entity)
        {
            entity.BingEntity.Highlight();
        }

    }
}
