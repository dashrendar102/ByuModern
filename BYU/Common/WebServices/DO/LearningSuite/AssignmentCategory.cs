using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.LearningSuite
{
    public class AssignmentCategory
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public bool calendarOnly { get; set; }

        [DataMember]
        public string courseID { get; set; }

        [DataMember]
        public int displayOrder { get; set; }

        [DataMember]
        public bool equalAssignWeight { get; set; }

        [DataMember]
        public bool extraCredit { get; set; }

        [DataMember]
        public string gbCategoryID { get; set; }

        [DataMember]
        public bool graded { get; set; }

        [DataMember]
        public object headingID { get; set; }

        [DataMember]
        public int lowScoresToDrop { get; set; }

        [DataMember]
        public object lsCategoryID { get; set; }

        [DataMember]
        public int percentDecimals { get; set; }

        [DataMember]
        public int pointsDecimals { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public bool viewLetterGrade { get; set; }

        [DataMember]
        public bool viewPercent { get; set; }

        [DataMember]
        public bool viewPoints { get; set; }

        [DataMember]
        public int weight { get; set; }

        public static async Task<AssignmentCategory> GetCategoryByID(string categoryID)
        {
            return await BYUWebServiceHelper.GetObjectFromWebService<AssignmentCategory>(string.Format(BYUWebServiceURLs.GET_LEARNING_SUITE_CATEGORY_BY_ID, categoryID));
        }
    }
}
