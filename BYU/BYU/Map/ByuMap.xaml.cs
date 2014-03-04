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
            ResetView();
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

        public async void ResetView()
        {
            BingMap.VenueManager.ActiveVenue = ByuVenue;
            BingMap.SetView(new Bing.Maps.Location(40.2525, -111.6494), 16);
            var buildings = await GetBuildings();
            foreach (var building in buildings)
                DeselectEntity(building);
        }

        ByuMapEntity lastSelected = null;

        public void SelectEntity(ByuMapEntity entity)
        {
            if (lastSelected != null)
            {
                DeselectEntity(lastSelected);
            }
            
            entity.BingEntity.Highlight();
            entity.BingEntity.ShowOutline();

            BingMap.SetView(entity.BingEntity.Location, 18.5);

            lastSelected = entity;
        }

        private void DeselectEntity(ByuMapEntity entity)
        {
            entity.BingEntity.Unhighlight();
            entity.BingEntity.HideOutline();
        }

    }
}
