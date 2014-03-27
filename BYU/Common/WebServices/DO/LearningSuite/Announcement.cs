using NodaTime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.LearningSuite
{
    [DataContract]
    public class Announcement
    {
        [DataMember (Name = "id")]
        public string id { get; set; }

        [DataMember (Name = "attachment")]
        public string attachment { get; set; } 

        [DataMember]
        public int? availableDate;

        public ZonedDateTime AvailableDateTime
        {
            get
            {
                return LearningSuiteUtils.ConvertTimeStampToDateTime(availableDate);
            }
        }

        [DataMember]
        public string[] courses;

        [DataMember]
        public string createdBy;

        [DataMember (Name="date")]
        public int? date { get; set; }

        public ZonedDateTime DateTime
        {
            get
            {
                return LearningSuiteUtils.ConvertTimeStampToDateTime(date);
            }
        }

        public String DateTimeFormalString
        {
            get
            {
                return String.Format("{0:dddd, MMMM dd, yyyy}", LearningSuiteUtils.ConvertTimeStampToDateTime(date));
            }
        }

        [DataMember]
        public int? expirationDate;

        public ZonedDateTime ExpirationDateTime
        {
            get
            {
                return LearningSuiteUtils.ConvertTimeStampToDateTime(expirationDate);
            }
        }

        [DataMember (Name = "instructorID")]
        public string instructorID { get; set; }

        [DataMember]
        public bool? published;

        [DataMember (Name = "text")]
        public string text { get; set; }

        [DataMember (Name = "title")]
        public string title { get; set; }

        [DataMember]
        public string updatedBy;

        [DataMember]
        public string yearTerm;

        public async static Task<Announcement[]> GetAnnouncements(string courseId)
        {
            return await BYUWebServiceHelper.GetObjectFromWebService<Announcement[]>(BYUWebServiceURLs.GET_ANNOUNCEMENTS + courseId);
        }
    }
}
