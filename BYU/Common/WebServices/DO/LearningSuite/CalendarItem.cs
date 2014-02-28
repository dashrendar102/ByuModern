using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.LearningSuite
{
    [DataContract]
    public class CalendarItem
    {
        [DataMember]
        public string id;

        [DataMember]
        public int? beginDate;

        [DataMember]
        public string categoryID;

        [DataMember]
        public string courseID;

        [DataMember]
        public string description;

        [DataMember]
        public int? displayOrder;

        [DataMember]
        public int? endDate;

        [DataMember]
        public bool? extraCredit;

        [DataMember]
        public bool? graded;

        [DataMember]
        public string headingID;

        [DataMember]
        public string title;

        [DataMember]
        public string type;

        public async static Task<CalendarItem[]> GetCalendarItems(string courseId)
        {
            return await BYUWebServiceHelper.GetObjectFromWebService<CalendarItem[]>(BYUWebServiceURLs.GET_CALENDAR_ITEMS + courseId);
        }
    }
}
