using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebServiceDemos.DOs;

namespace WebServiceDemos
{
    class WebServiceDemoMain
    {
        private const string netID = "";
        private const string pwd = "";
        private const string yearTerm = "20141";

        static void Main(string[] args)
        {
            const string courseID = "D3pGY5aU0FWK"; //cs428, found by inspecting my learning suite page
            getStudentSchedule();
            jsonTest();
            getAssignmentsByCourseID(courseID);

            Console.ReadKey();
        }

        private static void getAssignmentsByCourseID(string courseID)
        {
            string url = BYUWebServiceURLs.GetFullURL(BYUWebServiceURLs.GET_ASSIGNMENTS_BY_COURSE_ID, courseID);
            Console.WriteLine(url);
            BYUWebServiceHelper wsHelper = new BYUWebServiceHelper(netID, pwd);
            var response = wsHelper.sendAuthenticatedGETRequest(url);
            string prettyJson = JsonUtils.prettifyJson(response);
            Console.WriteLine(prettyJson);
        }

        private static void getStudentSchedule()
        {
            BYUWebServiceHelper wsHelper = new BYUWebServiceHelper(netID, pwd);
            var resp = wsHelper.sendAuthenticatedGETRequest("https://ws.byu.edu/rest/v1/academic/controls/controldatesws/asofdate/20140201/current_yyt,semester", "application/json");
            var foo2 = JsonUtils.prettifyJson(resp);
            
            string url = BYUWebServiceURLs.GetFullURL(BYUWebServiceURLs.GET_STUDENT_SCHEDULE, wsHelper.PersonID, yearTerm);
            var response = wsHelper.sendAuthenticatedGETRequest(url);
            string prettyJson = JsonUtils.prettifyJson(response);
            Console.WriteLine(prettyJson);
            System.IO.File.WriteAllText("output.txt", prettyJson);
        }

        [JsonConverter(typeof(CamelCaseNameMatchingConverter))]
        public class Response
        {
            public string SortName { get; set; }
            public string EmailAddress { get; set; }
            public string YearTerm { get; set; }
            public string DisplayYearTerm{ get; set; }
            public string StartYearTerm { get; set; }
            public string EndYearTerm { get; set; }
            public object enrolled { get; set; }

            [JsonProperty(PropertyName = "schedule_table")]
            public List<CourseInfo> CourseList { get; set; }
        }

        [JsonConverter(typeof(CamelCaseNameMatchingConverter))]
        public class WeeklySchedService
        {
            public Response Response { get; set; }
        }

        [JsonConverter(typeof(CamelCaseNameMatchingConverter))]
        public class RootObject
        {
            public WeeklySchedService WeeklySchedService { get; set; }
        }

        private static void jsonTest()
        {
            string json;
            var reader = new StreamReader("output.txt");
            json = reader.ReadToEnd();
            var foo = JsonConvert.DeserializeObject < RootObject>(json);

            CourseInfo sampleCourse = foo.WeeklySchedService.Response.CourseList[0];
            Calendar cal = new Calendar("Course Schedule", "My Current Courses");
            cal.AddCourse(sampleCourse);
        }

    }
}
