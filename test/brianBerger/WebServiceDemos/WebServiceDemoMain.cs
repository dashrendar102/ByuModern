using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            getAssignmentsByCourseID(courseID);
            getStudentSchedule();

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
            string url = BYUWebServiceURLs.GetFullURL(BYUWebServiceURLs.GET_STUDENT_SCHEDULE, wsHelper.PersonID, yearTerm);
            var response = wsHelper.sendAuthenticatedGETRequest(url);
            Console.WriteLine(JsonUtils.prettifyJson(response));
        }
    }
}
