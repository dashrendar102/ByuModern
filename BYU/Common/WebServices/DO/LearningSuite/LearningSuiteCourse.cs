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
    public class LearningSuiteCourse
    {
        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "allowAllCopy")]
        public string allowAllCopy { get; set; }

        [DataMember(Name = "allowExamsCopy")]
        public bool allowExamsCopy { get; set; }

        [DataMember(Name = "createdBy")]
        public string createdBy { get; set; }

        [DataMember(Name = "creationDate")]
        public int creationDate { get; set; }

        [DataMember(Name = "curriculumID")]
        public string curriculumID { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "devCourse")]
        public bool devCourse { get; set; }

        [DataMember(Name = "externalURL")]
        public string externalURL { get; set; }

        [DataMember(Name = "owner")]
        public string owner { get; set; }

        [DataMember(Name = "period")]
        public string period { get; set; }

        [DataMember(Name = "setup")]
        public bool setup { get; set; }

        [DataMember(Name = "shortTitle")]
        public string shortTitle { get; set; }

        [DataMember(Name = "title")]
        public string title { get; set; }

        [DataMember(Name = "titleCode")]
        public string titleCode { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }

        [DataMember(Name = "updatedBy")]
        public string updatedBy { get; set; }

        [DataMember(Name = "updatedDate")]
        public int updatedDate { get; set; }

        [DataMember(Name = "useAssignmentCategoryWeights")]
        public bool useAssignmentCategoryWeights { get; set; }

        [DataMember(Name = "visible")]
        public bool visible { get; set; }

        [DataMember(Name = "sections")]
        public LearningSuiteSection[] sections { get; set; }

        public static LearningSuiteCourse[] GetCourses()
        {
            WebServiceSession session = WebServiceSession.GetSession();
            
            Task<string> termTask = TermUtility.TermUtility.getCurrentTerm();
            termTask.Wait();
            string curTerm = termTask.Result;

            return BYUWebServiceHelper.GetObjectFromWebService<LearningSuiteCourse[]>(string.Format(BYUWebServiceURLs.GET_LEARNINGSUITE_COURSES, session.personId, curTerm));
        }
    }
}
