using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.LearningSuite
{
    [DataContract]
    public class Syllabus
    {
        [DataMember]
        public string id;

        [DataMember]
        public string curriculumID;

        [DataMember]
        public string titleCode;

        [DataMember]
        public string period;

        [DataMember]
        public string owner;

        [DataMember]
        public string courseID;

        public static Syllabus[] GetSyllabi(string courseId)
        {
            return BYUWebServiceHelper.GetObjectFromWebService<Syllabus[]>(BYUWebServiceURLs.GET_SYLLABI + courseId);
        }
    }
}
