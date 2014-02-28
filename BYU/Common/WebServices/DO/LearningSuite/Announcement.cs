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
        [DataMember]
        public string id;

        [DataMember]
        public string attachment;

        [DataMember]
        public int? availableDate;

        [DataMember]
        public string[] courses;

        [DataMember]
        public string createdBy;

        [DataMember]
        public int? date;

        [DataMember]
        public int? expirationDate;

        [DataMember]
        public string instructorID;

        [DataMember]
        public bool? published;

        [DataMember]
        public string text;

        [DataMember]
        public string title;

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
