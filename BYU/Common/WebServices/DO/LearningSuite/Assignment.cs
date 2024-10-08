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
                if (description == null)
                {
                    return "";
                }
                return StringUtils.ExtractAndPrettifyHTMLText(description);
            }
        }

        public string ShortDescription
        {
            get
            {
                int maxLength = 300;
                return StringUtils.LimitToLengthOnWordBoundaries(DescriptionText, maxLength);
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

        public string ExtraCreditYesNo
        {
            get
            {
                return extraCredit ? "Yes" : "No";
            }
        }

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
            TimeSpan timeout = TimeSpan.FromDays(2);
            return await BYUWebServiceHelper.GetObjectFromWebService<Assignment[]>(
                BYUWebServiceURLs.GET_ASSIGNMENTS_BY_COURSE_ID + courseId, timeout: timeout);
        }

        public AssignmentCategory Category { get; set; }

        public string CategoryName
        {
            get
            {
                if (Category == null)
                {
                    return "";
                }
                return Category.title;
            }
        }

        public async static Task<Assignment[]> GetUpcomingAssignments(string courseId)
        {
            Assignment[] allAssignments = await GetAssignments(courseId);
            
            var ordereredUpcomingAssignments =
                from a in allAssignments
                where a.DueDateTime.ToInstant() >= SystemClock.Instance.Now
                orderby a.DueDateTime
                select a;
            return ordereredUpcomingAssignments.ToArray();
        }

        public async Task RetrieveCategoryInformation()
        {
            if (Category == null)
            {
                Category = await AssignmentCategory.GetCategoryByID(this.categoryID);
            }
        }

        public static async Task<List<Assignment>> GetAllSemesterAssignments()
        {
            //note: I don't know of any way to grab all assignments in one web service call
            //if one is found, this should be rewritten
            var courses = await LearningSuiteCourse.GetCourses();
            IEnumerable<string> courseIDs =
                from course in courses
                select course.CourseID;
            List<Assignment> allAssignments = new List<Assignment>();
            foreach (string courseID in courseIDs)
            {
                Assignment[] courseAssignments = await GetAssignments(courseID);
                allAssignments.AddRange(courseAssignments);
            }

            return allAssignments;
        }

        public static async Task<Assignment[]> GetUpcomingAssignments(int numberOfResults)
        {
            List<Assignment> allAssignments = await GetAllSemesterAssignments();
            var upcoming = (
                from assignment in allAssignments
                where assignment.DueDateTime.ToInstant() >= SystemClock.Instance.Now
                orderby assignment.DueDateTime
                select assignment
                ).Take(numberOfResults);

            return upcoming.ToArray();
        }
    }
}
