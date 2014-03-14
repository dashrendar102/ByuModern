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
    public class ParkingLot
    {
        [DataMember(Name = "ParkingService")]
        public ParkingService ParkingService { get; set; }

        public static Polygon getPolygon()
        {
            //ParkingLotResponse myRoot = ParkingLotRoot.getParkingLot();
            //System.Diagnostics.Debug.WriteLine("Parking Lot Name: " + myRoot.Name);

            return null;
        }

        public async static Task<ParkingLotResponse> getAllLots()
        {
            //string term = await TermUtility.TermUtility.getCurrentTerm();
            //WebServiceSession session = await WebServiceSession.GetSession();

            //ClassScheduleRoot schedule = await BYUWebServiceHelper.GetObjectFromWebService<ClassScheduleRoot>(string.Format(BYUWebServiceURLs.GET_STUDENT_SCHEDULE, session.personId, term));
            //return schedule.WeeklySchedService.response;

            WebServiceSession session = await WebServiceSession.GetSession();

            ParkingLot parkingLots = await BYUWebServiceHelper.GetObjectFromWebService<ParkingLot>(string.Format(BYUWebServiceURLs.GET_PARKING_LOTS));
            return parkingLots.ParkingService.response;
            
        }

        //public async Task<IEnumerable<ParkingLot>> GetParkingLots()
        //{
        //    return await Task<IEnumerable<ParkingLot>>.Run(() =>
        //    {
        //        if (MapInit.IsCompleted || MapInit.Wait(TimeSpan.FromSeconds(10)))
        //        {
        //            var parkingLots = ParkingLot.getAllLots();
        //            return parkingLots;
        //        }
        //        else throw new TimeoutException("Could not load parking lot data");
        //    });

        //}
    }
}
