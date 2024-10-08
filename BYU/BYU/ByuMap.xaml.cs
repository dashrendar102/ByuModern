﻿using Bing.Maps;
using Bing.Maps.VenueMaps;
using Common.Buildings;
using Common.WebServices.DO.ParkingLots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Common
{
    public delegate void MapEntitySelectedEventArgs(object sender, ByuMapEntity selected);

    public sealed partial class ByuMap : UserControl
    {
        private static IEnumerable<ByuMapEntity> Buildings;
        private static IEnumerable<ParkingLot> ParkingLots;
        private static VenueMap ByuVenue;
        private Task MapInit = null;
        //This event has a default empty method so that we don't have to null check the event before firing it
        public event MapEntitySelectedEventArgs MapEntitySelected = (a, b) => { };
        private static MapShapeLayer parkingLayer;

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
            await LoadBuildingsAndVenue();
            await LoadParkingLots();
            MyBingMap.VenueManager.ActiveVenue = ByuVenue;
            MyBingMap.VenueManager.ActiveVenueChanged += VenueManager_ActiveVenueChanged;
            MyBingMap.VenueManager.VenueEntityTapped += VenueManager_VenueEntityTapped;
            this.ResetView();
        }

        private void LoadBuildingAcronyms(ByuBuilding[] webServiceBuildings, ByuMapEntity[] buildings)
        {
            #region Somewhat tricky code for mapping Bing Venue Entity objects to BYU Building abbreviations

            //Catalog all of the BYU Buildings by the words of their names
            var webBuildingsDict = new Dictionary<string[], ByuBuilding>();
            foreach (var wb in webServiceBuildings)
            {
                var nameTerms = StringUtils.GetWordsToLower(wb.Name);
                webBuildingsDict.Add(nameTerms, wb);
            }

            var usedMatches = new HashSet<ByuBuilding>();
            //Iterate through each Bing Venue entity to find the best BYU Building match
            foreach (var building in buildings)
            {
                var nameTerms = StringUtils.GetWordsToLower(building.Name);

                float bestScore = 0f;
                ByuBuilding bestMatch = null;

                foreach (var kvp in webBuildingsDict)
                {
                    //Intersect the words of the Bing Map Entity name with the BYU Building name to see if any match
                    var intersect = nameTerms.Intersect(kvp.Key);
                    //Score a building using (matched terms) - 0.5(unmatched terms).
                    //If there were any matches, the score should be at least 1.
                    //This algorithm helps with cases such as:
                    // - Joseph Smith Building vs. the Joseph F. Smith Building.
                    // - Auxiliary Services Maintenance Building vs. Laundry Building,Auxiliary Services
                    float score = 0f;
                    if (intersect.Any())
                    {
                        score = intersect.Count() - 0.5f * (kvp.Key.Length - intersect.Count());
                        score = Math.Max(score, 1);
                    }
                    //If the words in the name have more matches than our last best, use this building
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMatch = kvp.Value;
                    }
                }

                if (bestMatch != null && !usedMatches.Contains(bestMatch))
                {
                    usedMatches.Add(bestMatch);
                    //And now finally do what we're here for - set the acronym
                    if (!String.IsNullOrWhiteSpace(bestMatch.Acronym))
                        building.Acronym = bestMatch.Acronym;
                }
            }

            #endregion
        }

        private async Task LoadBuildingsAndVenue()
        {
            var venueTask = this.MyBingMap.VenueManager.CreateVenueMapAsync(Constants.ByuVenueId);
            if (Buildings == null)
            {
                Task<ByuBuilding[]> webServiceBuildingTask = BuildingRoot.GetAllBuildings();

                var webServiceBuildings = await webServiceBuildingTask;
                ByuVenue = await venueTask;

                ByuMapEntity[] buildings =
                    (from ve in ByuVenue.Floors.SelectMany(x => x.VenueEntities)
                        where !String.IsNullOrWhiteSpace(ve.Name)
                        select new ByuMapEntity(ve.Name, ve.Description, ve)).ToArray();

                LoadBuildingAcronyms(webServiceBuildings, buildings);
                Buildings = buildings;
            }
            else
            {
                ByuVenue = await venueTask;
            }
        }

        void VenueManager_VenueEntityTapped(object sender, VenueEntityEventArgs e)
        {
            var buildings = Buildings;
            var building = buildings.SingleOrDefault(b => b.BingEntity.Id == e.VenueEntity.Id);
            MapEntitySelected(this, building);
            OnBuildingSelected(new BuildingSelectedEventArgs {Entity = building});
        }

        void VenueManager_ActiveVenueChanged(object sender, ActiveVenueChangedEventArgs e)
        {
            ResetView();
        }

        public async Task<IEnumerable<ByuMapEntity>> GetBuildingsAsync()
        {
            return await Task.Run(() =>
            {
                if (MapInit.IsCompleted || MapInit.Wait(TimeSpan.FromSeconds(10)))
                {
                    IEnumerable<ByuMapEntity> buildingEnumerable = Buildings;
                    ByuMapEntity[] buildingArray = buildingEnumerable.ToArray<ByuMapEntity>();
                    return buildingArray;
                }
                else
                {
                    return null;
                }
            });
        }

        private double Zoom
        {
            get
            {
                //Linear slope-intercept formula for figuring out a good starting zoom, based on the height of the control
                //Orignal values 1/475 and 14.25 found using linear regression
                //Reduce the denominator on the left to increase the zoom per pixel of height increase
                //Increase the number on the right to increase the baseline zoom
                //Max is used because certain zoom levels are required for certain functionality:
                // - Zoom should be at least 16 to make buildings clickable
                // - Zoom should be at least 17 for buildings to have 3D outlines
                return Math.Max(this.Height / 475 + 14.25, 17);
            }
        }

        public void DrawAllParkingLots()
        {
            foreach (ParkingLot Lot in ParkingLots)
            {
                Lot.SetVisible(true);
            }
        }

        public void DrawParkingLotType(int parkingLotType)
        {
            if (parkingLotType == 0)
            {
                DrawAllParkingLots();
                return;
            }
            foreach (ParkingLot lot in ParkingLots)
            {
                lot.SetVisible(parkingLotType);
            }
        }

        public void HideAllParkingLots()
        {
            foreach (ParkingLot lot in ParkingLots)
            {
                lot.SetVisible(false);
            }
        }

        private async Task LoadParkingLots()
        {
            if (ParkingLots == null)
            {
                parkingLayer = new MapShapeLayer();
                MyBingMap.ShapeLayers.Add(parkingLayer);
                //MyBingMap.Children.Add(Infobox);
                ParkingLotResponse[] parkingLots = await ParkingLotRoot.getAllLots();

                List<ParkingLot> myLots = new List<ParkingLot>();
                foreach (ParkingLotResponse Lot in parkingLots)
                {
                    ParkingLot newLot = new ParkingLot(Lot);
                    parkingLayer.Shapes.Add(newLot.getParkingPolygon());
                    parkingLayer.Shapes.Add(newLot.getParkingOutline());
                    myLots.Add(newLot);
                }

                ParkingLots = myLots;
            }
            else MyBingMap.ShapeLayers.Add(parkingLayer);
        }

        public async Task<IEnumerable<ParkingLot>> GetParkingLotsAsync()
        {
            return await Task.Run(() =>
            {
                if (MapInit.IsCompleted || MapInit.Wait(TimeSpan.FromSeconds(10)))
                {
                    IEnumerable<ParkingLot> parkingEnumerable = ParkingLots;
                    ParkingLot[] ParkingLotArray = parkingEnumerable.ToArray<ParkingLot>();
                    return ParkingLotArray;
                }
                else
                {
                    throw new TimeoutException("Could not load parking Data");
                }
            });
        }

        public void OpenInfobox(MapPolygon myLot)
        {
            //Infobox.DataContext = myLot.GetValue();
            Infobox.DataContext = "hello";
            Infobox.Visibility = Visibility.Visible;
            MapLayer.SetPosition(Infobox, MapLayer.GetPosition(myLot.Locations[1]));
        }

        private void CloseInfoboxTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Infobox.Visibility = Visibility.Collapsed;
        }

        public async void ResetView()
        {
            if (ByuVenue != null)
            {
                this.MyBingMap.VenueManager.ActiveVenue = ByuVenue;
                this.MyBingMap.SetView
                (
                    center: new Location(
                        (double)(this.Resources["Latitude"] ?? ByuVenue.Location.Latitude),
                        (double)(this.Resources["Longitude"] ?? ByuVenue.Location.Longitude)),
                    zoomLevel: Zoom
                );

                var buildings = await GetBuildingsAsync();
                if (buildings != null)
                {
                    foreach (var building in buildings)
                    {
                        DeselectEntity(building);
                    }
                }
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

        public event EventHandler<BuildingSelectedEventArgs> BuildingSelected;

        private void OnBuildingSelected(BuildingSelectedEventArgs e)
        {
            EventHandler<BuildingSelectedEventArgs> handler = BuildingSelected;
            if (handler != null) handler(this, e);
        }
    }
    public class BuildingSelectedEventArgs : EventArgs
    {
        public ByuMapEntity Entity { get; set; }
    }
}
