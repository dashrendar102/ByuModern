﻿using Bing.Maps;
using Bing.Maps.VenueMaps;
using Common;
using Common.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
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
    public delegate void MapEntitySelectedEventArgs(object sender, ByuMapEntity selected);

    public sealed partial class ByuMap : UserControl
    {

        private VenueMap ByuVenue;
        Task MapInit = null;
        //This event has a default empty method so that we don't have to null check the event before firing it
        public event MapEntitySelectedEventArgs MapEntitySelected = (a, b) => { };

        public ByuMap()
        {
            this.InitializeComponent();
            this.SizeChanged += ByuMap_SizeChanged;

            MapInit = SetupMapAsync();
        }

        void ByuMap_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Height = e.NewSize.Height;
            this.ResetView();
        }

        async Task SetupMapAsync()
        {
            ByuVenue = await this.MyBingMap.VenueManager.CreateVenueMapAsync(Constants.ByuVenueId);
            this.MyBingMap.VenueManager.ActiveVenue = ByuVenue;

            this.MyBingMap.VenueManager.ActiveVenueChanged += VenueManager_ActiveVenueChanged;
            this.MyBingMap.VenueManager.VenueEntityTapped += VenueManager_VenueEntityTapped;
        }

        void VenueManager_VenueEntityTapped(object sender, VenueEntityEventArgs e)
        {
            var buildings = GetBuildingsSync();
            var building = buildings.SingleOrDefault(b => b.BingEntity == e.VenueEntity);
            MapEntitySelected(this, building);
        }

        void VenueManager_ActiveVenueChanged(object sender, ActiveVenueChangedEventArgs e)
        {
            ResetView();
        }

        public async Task<IEnumerable<ByuMapEntity>> GetBuildings()
        {
            string cacheIdentifier = "buildingList";
            return await Task<IEnumerable<ByuMapEntity>>.Run(() =>
            {
                if (MapInit.IsCompleted || MapInit.Wait(TimeSpan.FromSeconds(10)))
                {
                    if (WebCache.Instance.IsCached(cacheIdentifier).Result)
                    {
                        ByuMapEntity[] buildingArray = WebCache.Instance.RetrieveObjectFromCache<ByuMapEntity[]>(cacheIdentifier).Result;
                        return buildingArray;
                    }
                    else
                    {
                        IEnumerable<ByuMapEntity> buildingEnumerable = GetBuildingsSync();
                        ByuMapEntity[] buildingArray = buildingEnumerable.ToArray<ByuMapEntity>();
                        //var cacheTask = WebCache.Instance.CacheObject(cacheIdentifier, buildingArray);
                        //cacheTask.Wait();
                        return buildingArray;
                    }
                }
                else
                {
                    throw new TimeoutException("Could not load maps data");
                }
            });
        }

        //Only call this if you are certain the Venue has been loaded
        private IEnumerable<ByuMapEntity> GetBuildingsSync()
        {
            var buildings =
                from ve in ByuVenue.Floors.SelectMany(x => x.VenueEntities)
                where !String.IsNullOrWhiteSpace(ve.Name)
                select new ByuMapEntity(ve.Name, ve.Description, ve);
            return buildings;
        }

        private double Zoom
        {
            get
            {
                //Linear formula for figuring out a good starting zoom, based on the height of the control
                //Reduce the denominator on the left to increase the zoom per pixel of height increase
                //Increase the number on the right to increase the baseline zoom
                //Zoom should be at least 16 to make buildings clickable
                return Math.Max(this.Height / 475 + 14.25, 16);
            }
        }

        public void drawPolygon()
        {
            //ParkingLot.getAllLots();
            MapShapeLayer parkingLayer = new MapShapeLayer();
            MapPolygon myPolygon = getPolygon();
            parkingLayer.Shapes.Add(myPolygon);
            this.MyBingMap.ShapeLayers.Add(parkingLayer);

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

            myPolygon.FillColor = Windows.UI.Color.FromArgb(50, 0, 0, 255);
            return myPolygon;
        }

        public async void ResetView()
        {
            this.MyBingMap.VenueManager.ActiveVenue = ByuVenue;
            this.MyBingMap.SetView(new Bing.Maps.Location((double)this.Resources["Latitude"], (double)this.Resources["Longitude"]), Zoom);
            var buildings = await GetBuildings();
            foreach (var building in buildings)
            {
                DeselectEntity(building);
            }
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

            this.MyBingMap.SetView(entity.BingEntity.Location, 18.5);

            lastSelected = entity;
        }

        private void DeselectEntity(ByuMapEntity entity)
        {
            entity.BingEntity.Unhighlight();
            entity.BingEntity.HideOutline();
        }

        private void ResetViewButton_Click(object sender, RoutedEventArgs e)
        {
            ResetView();
        }

    }
}
