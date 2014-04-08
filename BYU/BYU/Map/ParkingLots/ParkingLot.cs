using Bing.Maps;
using Common.WebServices;
using Common.WebServices.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;


namespace Common.WebServices.DO.ParkingLots
{
    [DataContract]
    public class ParkingLot
    {
        private ParkingLotResponse Lot;
        private MapPolygon parkingPolygon;
        private MapPolyline parkingOutline;

        public static readonly DependencyProperty TagProp = DependencyProperty.Register
            ("Tag", typeof(object), typeof(MapShape), new PropertyMetadata(null));
        
        
        public ParkingLot(ParkingLotResponse Lot)
        {
            this.Lot = Lot;
            parkingPolygon = new MapPolygon();
            parkingPolygon.PointerEntered += ParkingLotEntered;
            parkingPolygon.PointerExited += ParkingLotExited;
            parkingPolygon.Tapped += ParkingLotTapped;
            parkingPolygon.Visible = false;
            parkingOutline = new MapPolyline();
            parkingOutline.Width = 2;
            parkingOutline.Visible = false;
            parkingPolygon.SetValue(TagProp, GetDescription());
            setColors();
            setPoints();
        }

        private void setPoints()
        {
            Location myLocation;
            string[] myPointStrings = Lot.PolygonPoints.Split(',');

            for (int i = 0; i < myPointStrings.Length - 1; i++)
            {
                myLocation = new Location(Convert.ToDouble(myPointStrings[i]), Convert.ToDouble(myPointStrings[i+1]));
                parkingOutline.Locations.Add(myLocation);
                parkingPolygon.Locations.Add(myLocation);
                i++;
            }
            parkingOutline.Locations.Add(new Location(Convert.ToDouble(myPointStrings[0]), Convert.ToDouble(myPointStrings[1])));
        }

        private void setColors()
        {
            byte[] myColor = ParkingLotRoot.getColor(Lot.TypeID);
            parkingPolygon.FillColor = Windows.UI.Color.FromArgb(50, myColor[0], myColor[1], myColor[2]);
            parkingOutline.Color = Windows.UI.Color.FromArgb(255, myColor[0], myColor[1], myColor[2]);
        }
       
        public MapPolygon getParkingPolygon()
        {
            return parkingPolygon;
        }

        public MapPolyline getParkingOutline()
        {
            return parkingOutline;
        }

        public ParkingData GetDescription()
        {
            ParkingData myData = new ParkingData();
            myData.Title = ParkingLotRoot.GetTitle(Lot.TypeID);

            if(String.IsNullOrEmpty(Lot.Description))
            {
                myData.Description = "No information is available for this lot.";
                return myData;
            }

            myData.Description = Regex.Replace(Lot.Description, "<.*?>", string.Empty);
            return myData;
        }



        private async void ParkingLotTapped(object sender, TappedRoutedEventArgs e)
        {
            if(sender is MapShape)
            {
                var poly = sender as MapPolygon;
                if(poly.Visible == false)
                {
                    return;
                }
                ParkingData tag = (ParkingData)poly.GetValue(TagProp);

                if(!String.IsNullOrEmpty(tag.Title) || !String.IsNullOrEmpty(tag.Description))
                {
                    //ByuMap.OpenInfobox(poly as MapPolygon);
                    //map.infobox
                    var msg = new MessageDialog(tag.Title + '\n' +tag.Description);
                    try
                    {
                        await msg.ShowAsync();
                    }
                    catch (UnauthorizedAccessException)
                    {
                        return;
                    }
                }
            }
        }

        private void ParkingLotEntered(object sender, PointerRoutedEventArgs e)
        {
            //send string to map
            //map.OpenInfobox(this.parkingPolygon);
            this.parkingOutline.Width = 3;
        }

        private void ParkingLotExited(object sender, PointerRoutedEventArgs e)
        {
            //Infobox.Visibility = Visibility.Collapsed;
            this.parkingOutline.Width = 2;
        }


        internal DependencyObject GetPosition()
        {
            return this.parkingPolygon.Locations[1];
        }

        internal void SetVisible(bool p)
        {
            parkingPolygon.Visible = p;
            parkingOutline.Visible = p;
        }

        internal void SetVisible(int lotType)
        {
            if(this.Lot.TypeID == lotType)
            {
                this.parkingOutline.Visible = true;
                this.parkingPolygon.Visible = true;
            }
            else
            {
                this.parkingOutline.Visible = false;
                this.parkingPolygon.Visible = false;
            }
        }
    }
    public class ParkingData
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
