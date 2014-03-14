using Bing.Maps;
using Bing.Maps.VenueMaps;
using Common;
using Common.WebServices;
using Common.WebServices.DO;
using Common.WebServices.DO.ParkingLots;
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

namespace Common
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

        public void drawPolygon()
        {
            //ParkingLot.getAllLots();
            MapShapeLayer parkingLayer = new MapShapeLayer();
            MapPolygon myPolygon = getPolygon();
            parkingLayer.Shapes.Add(myPolygon);
            BingMap.ShapeLayers.Add(parkingLayer);
          
        }


        private static MapPolygon getPolygon()
        {
            MapPolygon myPolygon = new MapPolygon();
            myPolygon.Locations = new LocationCollection() {
                new Location(40.244156769,-111.654940121),
                new Location(40.244102515,-111.654940121),
                new Location(40.244111728,-111.653578900),
                new Location(40.244186455,-111.653580241),
                new Location(40.244183384,-111.653790794),
                new Location(40.244181337,-111.654010735),
                new Location(40.244184408,-111.654210560),
                new Location(40.244181337,-111.654379539),
                new Location(40.244170076,-111.654595457),
                new Location(40.244156769,-111.654911958),
                new Location(40.244156769,-111.654911958),
                new Location(40.244156769,-111.654911958),
                new Location(40.244156769,-111.654911958)

                //new Location(40.252299832,-111.650151585),
                //new Location(40.251298802,-111.650130127),
                //new Location(40.251288567,-111.649658058),
                //new Location(40.252267078,-111.649692927),
                //new Location(40.252267078,-111.649692927),
                //new Location(40.252267078,-111.649692927),
                //new Location(40.252267078,-111.649692927),
                //new Location(40.252267078,-111.649692927),
                //new Location(40.252267078,-111.649692927)
            };
          
            myPolygon.FillColor = Windows.UI.Color.FromArgb(50, 0, 0,255);
            return myPolygon;
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
