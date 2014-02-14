using BergerClassLibrary.CalendarLand;
using BergerClassLibrary.Extensions;
using BergerClassLibrary.WebServices;
using BergerClassLibrary.WebServices.DOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerClassLibrary
{
    public sealed class Demo
    {
        private const string netID = "";
        private const string pwd = "";
        private const string yearTerm = "20141";

        public string doDemoStuff()
        {
            const string courseID = "D3pGY5aU0FWK"; //cs428, found by inspecting my learning suite page
            getStudentSchedule();
            jsonTest();
            getAssignmentsByCourseID(courseID);

            return "demo done";
        }

        private void getAssignmentsByCourseID(string courseID)
        {
            string url = BYUWebServiceURLs.GetFullURL(BYUWebServiceURLs.GET_ASSIGNMENTS_BY_COURSE_ID, courseID);
            BYUWebServiceHelper wsHelper = new BYUWebServiceHelper(netID, pwd);
            var response = wsHelper.sendAuthenticatedGETRequest(url);
            string prettyJson = JsonUtils.prettifyJson(response);
            prettyJson.ToString();
        }

        private string getStudentSchedule()
        {
            BYUWebServiceHelper wsHelper = new BYUWebServiceHelper(netID, pwd);
            
            string url = BYUWebServiceURLs.GetFullURL(BYUWebServiceURLs.GET_STUDENT_SCHEDULE, wsHelper.PersonID, yearTerm);
            var response = wsHelper.sendAuthenticatedGETRequest(url);
            return response.GetContentAsString();
        }

        private void jsonTest()
        {
            string json = getStudentSchedule();
            var foo = JsonConvert.DeserializeObject<RootObject>(json);

            CourseInfo sampleCourse = foo.WeeklySchedService.Response.CourseList[0];
            Calendar cal = new Calendar("Course Schedule", "My Current Courses");
            cal.AddCourse(sampleCourse);
        }

    }
}
