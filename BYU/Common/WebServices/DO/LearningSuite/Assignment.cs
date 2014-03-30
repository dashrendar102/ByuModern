﻿using Common.Calendar;
using NodaTime;
using NodaTime.Text;
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
        public string id { get; set; }

        [DataMember]
        public bool allowLateSubmission { get; set; }

        [DataMember]
        public bool allowScoreDrop { get; set; }

        [DataMember]
        public string attachment { get; set; }

        [DataMember]
        public int? beginDate { get; set; }

        public ZonedDateTime BeginDateTime
        {
            get
            {
                return LearningSuiteUtils.ConvertTimeStampToDateTime(beginDate);
            }
        }

        [DataMember]
        public string categoryID { get; set; }

        [DataMember]
        public string courseID { get; set; }

        [DataMember]
        public string description { get; set; }

        public string DescriptionText
        {
            get
            {
                return StringUtils.RetrieveTextFromHTML(description);
            }
        }

        [DataMember]
        public int displayOrder { get; set; }

        [DataMember]
        public int displayOrderCalendar { get; set; }

        [DataMember]
        public int dueDate { get; set; }

        public ZonedDateTime DueDateTime
        {
            get
            {
                return LearningSuiteUtils.ConvertTimeStampToDateTime(dueDate);
            }
        }

        public string FormattedDueDate
        {
            get
            {
                ZonedDateTimePattern datePattern = ZonedDateTimePattern.CreateWithInvariantCulture("MMMM dd, yyyy", DateTimeZoneProviders.Tzdb);
                ZonedDateTimePattern timePattern = ZonedDateTimePattern.CreateWithInvariantCulture("hh:mm tt", DateTimeZoneProviders.Tzdb);
                return datePattern.Format(DueDateTime) + " at " + timePattern.Format(DueDateTime);
            }
        }

        [DataMember]
        public bool extraCredit { get; set; }

        [DataMember]
        public int flagScoresBelowPassing { get; set; }

        [DataMember]
        public string gbAssignmentID { get; set; }

        [DataMember]
        public string gradebookID { get; set; }

        [DataMember]
        public bool graded { get; set; }

        [DataMember]
        public string gradingScale { get; set; }

        [DataMember]
        public bool includeScoreInFinalGrade { get; set; }

        [DataMember]
        public int minScore { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string onlineSubmission { get; set; }

        [DataMember]
        public bool passScoreRequired { get; set; }

        [DataMember]
        public int passingScore { get; set; }

        [DataMember]
        public bool plagiarismCheck { get; set; }

        [DataMember]
        public bool plagiarismERaterEnabled { get; set; }

        [DataMember]
        public bool plagiarismExcludeBiblio { get; set; }

        [DataMember]
        public bool plagiarismExcludeQuoted { get; set; }

        [DataMember]
        public bool plagiarismStudViewOrig { get; set; }

        [DataMember]
        public int points { get; set; }

        [DataMember]
        public string rubric { get; set; }

        [DataMember]
        public string scoreEntry { get; set; }

        [DataMember]
        public int scoreMultiplier { get; set; }

        [DataMember]
        public string scoreVisibleDate { get; set; }

        [DataMember]
        public string shortName { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string typeID { get; set; }

        [DataMember]
        public string visibleDate { get; set; }

        [DataMember]
        public double weight { get; set; }

        [DataMember]
        public string zeroScoresDate { get; set; }

        public async static Task<Assignment[]> GetAssignments(string courseId)
        {
            return await BYUWebServiceHelper.GetObjectFromWebService<Assignment[]>(BYUWebServiceURLs.GET_ASSIGNMENTS_BY_COURSE_ID + courseId);
        }

        public async static Task<Assignment[]> GetUpcomingAssignments(string courseId)
        {
            Assignment[] allAssignments = await GetAssignments(courseId);
            ZonedDateTime currentTime = new ZonedDateTime(SystemClock.Instance.Now, Constants.BYUTimeZone);
            var ordereredUpcomingAssignments =
                from a in allAssignments
                where a.DueDateTime >= currentTime
                orderby a.DueDateTime
                select a;
            return allAssignments.ToArray();
        }
    }
}
