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
    public class Assignment
    {
        [DataMember]
        public string id;

        [DataMember]
        public bool allowLateSubmission;

        [DataMember]
        public bool allowScoreDrop;

        [DataMember]
        public string attachment;

        [DataMember]
        public int? beginDate;

        [DataMember]
        public string categoryID;

        [DataMember]
        public string courseID;

        [DataMember]
        public string description;

        [DataMember]
        public int displayOrder;

        [DataMember]
        public int displayOrderCalendar;

        [DataMember]
        public int dueDate;

        [DataMember]
        public bool extraCredit;

        [DataMember]
        public int flagScoresBelowPassing;

        [DataMember]
        public string gbAssignmentID;

        [DataMember]
        public string gradebookID;

        [DataMember]
        public bool graded;

        [DataMember]
        public string gradingScale;

        [DataMember]
        public bool includeScoreInFinalGrade;

        [DataMember]
        public int minScore;

        [DataMember]
        public string name;

        [DataMember]
        public string onlineSubmission;

        [DataMember]
        public bool passScoreRequired;

        [DataMember]
        public int passingScore;

        [DataMember]
        public bool plagiarismCheck;

        [DataMember]
        public bool plagiarismERaterEnabled;

        [DataMember]
        public bool plagiarismExcludeBiblio;

        [DataMember]
        public bool plagiarismExcludeQuoted;

        [DataMember]
        public bool plagiarismStudViewOrig;

        [DataMember]
        public int points;

        [DataMember]
        public string rubric;

        [DataMember]
        public string scoreEntry;

        [DataMember]
        public int scoreMultiplier;

        [DataMember]
        public string scoreVisibleDate;

        [DataMember]
        public string shortName;

        [DataMember]
        public string type;

        [DataMember]
        public string typeID;

        [DataMember]
        public string visibleDate;

        [DataMember]
        public double weight;

        [DataMember]
        public string zeroScoresDate;

        public static Assignment[] GetAssignments(string courseId)
        {
            return BYUWebServiceHelper.GetObjectFromWebService<Assignment[]>(BYUWebServiceURLs.GET_ASSIGNMENTS_BY_COURSE_ID + courseId);
        }
    }
}
