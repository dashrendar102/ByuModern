using Common.WebServices;
using Common.WebServices.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;



namespace Common.WebServices.DO.ParkingLots
{
    [DataContract]
    public class ParkingLotRoot
    {
        enum lotType { Y = 1, G, A, C, R, V, T, MOTOR, BIKE, CON, B};        
        //            {1, "Y"},               Y (student)           Lot Type = 1
        //            {2, "G"},               G (Graduate)          Lot Type = 2
        //            {3, "A"},               A (Faculty/Staff)     Lot Type = 3
        //            {4, "C"},               C (Helaman Halls)     Lot Type = 4
        //            {5, "R"},               R (Restricted Visitor)Lot Type = 5
        //            {6, "V"},               V (Visitor)           Lot Type = 6
        //            {7, "T"},               T (Timed)             Lot Type = 7
        //            {8, "Motorcycle"},      Motorcycle            Lot type = 8
        //            {9, "Bike"},            Bike Rack             Lot type = 9
        //            {10, "Construction"},   construction          Lot type = 10
        //            {11, "B"}               B (Heritage Halls)    Lot type = 11
        
        [DataMember(Name = "ParkingService")]
        public ParkingService ParkingService { get; set; }

        public static Polygon getPolygon()
        {
            return null;
        }

        public async static Task<ParkingLotResponse[]> getAllLots()
        {
            
            WebServiceSession session = await WebServiceSession.GetSession();

            ParkingLotResponse[] parkingLots = await BYUWebServiceHelper.GetObjectFromWebService<ParkingLotResponse[]>(string.Format(BYUWebServiceURLs.GET_PARKING_LOTS), authenticate: false, allowCache: true);
            return parkingLots;
            
        }

        /// <summary>
        /// return a set of 3 bytes representing the RGB values for the color in question
        /// This is required for a map polygon if we want transparency for parking lots.
        /// Currently only Fill color is avaliable for parkingLots as showin in documentation.
        /// for future reference, here is the documentation: http://msdn.microsoft.com/en-us/library/hh846506.aspx
        /// -Brian Black 3/18/2014
        /// </summary>
        /// <param name="parkingLotType"></param>
        /// <returns></returns>
        public static byte[] getColor(int parkingLotType)
        {
            // Bytes to return
            byte[] myColor = new byte[3] { 0, 0, 255};
            switch(parkingLotType)
            {
                case (int)lotType.Y:
                    myColor = new byte[3] { 255, 255, 0 };
                    break;
                case (int)lotType.G:
                    myColor = new byte[3] { 255, 51, 51 };
                    break;
                case (int)lotType.A:
                    break;
                case (int)lotType.C:
                    myColor = new byte[3] { 0, 102, 51 };
                    break;
                case (int)lotType.R:
                    myColor = new byte[3] { 238, 165, 40 };
                    break;
                case (int)lotType.V:
                    myColor = new byte[3] { 102, 51, 0 };
                    break;
                case (int)lotType.T:
                    myColor = new byte[3] { 64, 64, 64 };
                    break;
                case (int)lotType.MOTOR:
                    myColor = new byte[3] { 0, 204, 204 };
                    break;
                case (int)lotType.BIKE:
                    myColor = new byte[3] { 204, 0, 102 };
                    break;
                case (int)lotType.CON:
                    myColor = new byte[3] { 0, 255, 128 };
                    break;
                case (int)lotType.B:
                    myColor = new byte[3] { 153, 51, 255 };
                    break;
            }

            return myColor;
        }


        internal static string GetTitle(int parkingLotType)
        {
            string Title = "Lot type: ";
            switch (parkingLotType)
            {
                case (int)lotType.Y:
                    Title += "Student (Y) Lot";
                    break;
                case (int)lotType.G:
                    Title += "Graduate (G) Lot";
                    break;
                case (int)lotType.A:
                    Title += "Faculty/Staff (A) Lot";
                    break;
                case (int)lotType.C:
                    Title += "Helaman Halls (C) Lot";
                    break;
                case (int)lotType.R:
                    Title += "Restricted Visitor (R) Lot";
                    break;
                case (int)lotType.V:
                    Title += "Visitor (V) Lot";
                    break;
                case (int)lotType.T:
                    Title += "Timed (T) Lot";
                    break;
                case (int)lotType.MOTOR:
                    Title += "Motorcycle (Motorcycle) Lot";
                    break;
                case (int)lotType.BIKE:
                    Title += "Bike (Bike Rack)";
                    break;
                case (int)lotType.CON:
                    Title += "Construction Area";
                    break;
                case (int)lotType.B:
                    Title += "Heritage Halls (B) Lot";
                    break;
            }
            return Title;
        }

    }
}
