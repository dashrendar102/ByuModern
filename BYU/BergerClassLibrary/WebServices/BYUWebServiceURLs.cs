using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BergerClassLibrary.WebServices
{
    public static class BYUWebServiceURLs
    {
        internal const string GET_ASSIGNMENTS_BY_COURSE_ID = "https://ws.byu.edu/rest/v1.0/learningsuite/assignments/assignment/courseID/<courseID>";
        internal const string GET_STUDENT_SCHEDULE = "https://ws.byu.edu/rest/v1.0/academic/registration/studentschedule/<personID>/<yearTerm>";


        public static string GetFullURL(string baseURL, params string[] argList) {
            string result = baseURL;
            Regex regex = new Regex("<.*?>");
            foreach (string arg  in argList) {
                result = regex.Replace(result, arg, 1);
            }
            return result;
        }
    }
}
