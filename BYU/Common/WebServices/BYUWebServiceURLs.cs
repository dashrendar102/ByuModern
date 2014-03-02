using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.WebServices
{
    public static class BYUWebServiceURLs
    {
        internal const string GET_ASSIGNMENTS_BY_COURSE_ID = "https://ws.byu.edu/rest/v1.0/learningsuite/assignments/assignment/courseID/";
        internal const string GET_STUDENT_SCHEDULE = "https://ws.byu.edu/rest/v1.0/academic/registration/studentschedule/{0}/{1}";
        internal const string GET_PERSONAL_INFO = "https://ws.byu.edu/rest/v2.0/identity/person/PRO/personSummary.cgi/";
        internal const string GET_USER_PHOTO_BY_PERSON_ID = "https://ws.byu.edu/rest/v1.0/identity/person/idphoto/?p=";
        internal const string GET_WS_SESSION = "https://ws.byu.edu/authentication/services/rest/v1/ws/session";
        internal const string GET_NONCE_URL = "https://ws.byu.edu/authentication/services/rest/v1/hmac/nonce/";
        internal const string GET_ANNOUNCEMENTS = "https://ws.byu.edu/rest/v1.0/learningsuite/announcements/announcement/courseID/";
        internal const string GET_LEARNINGSUITE_COURSES = "https://ws.byu.edu/rest/v1.0/learningsuite/coursebuilder/course/personEnrolled/{0}/period/{1}";
        internal const string GET_CALENDAR_ITEMS = "https://ws.byu.edu/rest/v1.0/learningsuite/calendar/aggregate/courseID/";
        internal const string GET_CONTENT_PAGES = "https://ws.byu.edu/rest/v1.0/learningsuite/pages/page-and-content/courseID/";
        internal const string GET_INSTRUCTORS = "https://ws.byu.edu/rest/v1.0/learningsuite/coursebuilder/instructor-information/courseID/";
        internal const string GET_MATERIALS = "https://ws.byu.edu/rest/v1.0/learningsuite/syllabus/material/courseID/{0}/includeBooklist/true";
        internal const string GET_SYLLABI = "https://ws.byu.edu/rest/v1.0/learningsuite/syllabus/syllabus/courseID/";
        internal const string GET_SYSTEM_ANNOUNCEMENT = "https://ws.byu.edu/rest/v1.0/learningsuite/announcements/sysannouncement/student-anno";
        internal const string GET_CONTROL_DATES = "https://ws.byu.edu/rest/v1/academic/controls/controldatesws/asofdate/{0}/current_yyt";

        // This functionality is replaced by String.Format(string toFormat, string param1, string param2, ...)
        // where each parameter after the first replaces indexed sections in the string like this: "...{0}...{1}...{2}...".
        // For example: string.Format("Hello {0}, how are you {1}", "Bob", "today?") returns "Hello Bob, how are you today?".
        /*public static string GetFullURL(string baseURL, params string[] argList) {
            string result = baseURL;
            Regex regex = new Regex("<.*?>");
            foreach (string arg  in argList) {
                result = regex.Replace(result, arg, 1);
            }
            return result;
        }*/
    }
}
