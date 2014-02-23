using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.LearningSuite
{
    [DataContract]
    public class Instructor
    {
        [DataMember]
        public string id;

        [DataMember]
        public string byAppointment;

        [DataMember]
        public string courseID;

        [DataMember]
        public string email;

        [DataMember]
        public string homePhone;

        [DataMember]
        public string mobilePhone;

        [DataMember]
        public string name;

        [DataMember]
        public string officeLocation;

        [DataMember]
        public string officePhone;

        [DataMember]
        public string personID;

        [DataMember]
        public string professionalTitle;

        public static Instructor[] GetInstructors(string courseId)
        {
            return BYUWebServiceHelper.GetObjectFromWebService<Instructor[]>(BYUWebServiceURLs.GET_INSTRUCTORS + courseId);
        }
    }
}
