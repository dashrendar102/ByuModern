using System.IO;
//using System;
//using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using AuthenticationLib.LearningSuite;
using Common.WebServices.DO.LearningSuite;
using Common.WebServices;

namespace Common
{
    public class MainTest
    {
        public static string MainRun(string netId, string password)
        {
            WebServiceSession session = NonceAuthentication.GetWsSession(netId, password, 5);
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);

            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/coursebuilder/course/personEnrolled/" + session.personId + "/period/20141");
            request.Headers["Authorization"] = nonceHeader;

            Task<WebResponse> responseTask = request.GetResponseAsync();
            responseTask.Wait();
            WebResponse response = responseTask.Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LearningSuiteCourse[]));
            LearningSuiteCourse[] courses = (LearningSuiteCourse[])serializer.ReadObject(response.GetResponseStream());

            string toReturn = "";

            foreach (LearningSuiteCourse course in courses)
            {
                Assignment[] assignments = getAssignments(course.id, session);
                CalendarItem[] calendarItems = getCalendarItems(course.id, session);
                ContentPage[] contentItems = getContent(course.id, session);
                Announcement[] announcements = getAnnouncements(course.id, session);
                Instructor[] instructors = getInstructors(course.id, session);
                Material[] materials = getMaterials(course.id, session);
                Syllabus[] syllabi = getSyllabi(course.id, session);
                SystemAnnouncement sysAnnouncement = getSystemAnnouncement(session);

                /*foreach (Assignment assignment in assignments)
                {
                    toReturn += assignment.id + " ";
                }

                toReturn += "END_ASSIGNMENTS_START_CALENDAR ";
                
                foreach(CalendarItem calItem in calendarItems)
                {
                    toReturn += calItem.id + " ";
                }

                foreach(ContentPage page in contentItems)
                {
                    toReturn += page.id + " ";
                }
                
                foreach(Announcement announcement in announcements)
                {
                    toReturn += announcement.id + " ";
                }
                
                foreach(Instructor instructor in instructors)
                {
                    toReturn += instructor.id + " ";
                }
                
                foreach(Material material in materials)
                {
                    toReturn += material.id + " ";
                }
                
                foreach(Syllabus syllabus in syllabi)
                {
                    toReturn += syllabus.id + " ";
                }
                */
                toReturn += sysAnnouncement.id;
            }

            return toReturn.TrimEnd();
        }

        public static SystemAnnouncement getSystemAnnouncement(WebServiceSession session)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);
            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/announcements/sysannouncement/student-anno");
            request.Headers["Authorization"] = nonceHeader;

            Task<WebResponse> responseTask = request.GetResponseAsync();
            responseTask.Wait();
            WebResponse response = responseTask.Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SystemAnnouncement));
            SystemAnnouncement announcement = (SystemAnnouncement)serializer.ReadObject(response.GetResponseStream());

            return announcement;
        }

        public static Syllabus[] getSyllabi(string courseId, WebServiceSession session)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);
            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/syllabus/syllabus/courseID/" + courseId);
            request.Headers["Authorization"] = nonceHeader;

            Task<WebResponse> responseTask = request.GetResponseAsync();
            responseTask.Wait();
            WebResponse response = responseTask.Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Syllabus[]));
            Syllabus[] syllabi = (Syllabus[])serializer.ReadObject(response.GetResponseStream());

            return syllabi;
        }

        public static Material[] getMaterials(string courseId, WebServiceSession session)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);
            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/syllabus/material/courseID/" + courseId + "/includeBooklist/true");
            request.Headers["Authorization"] = nonceHeader;

            Task<WebResponse> responseTask = request.GetResponseAsync();
            responseTask.Wait();
            WebResponse response = responseTask.Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Material[]));
            Material[] materials = (Material[])serializer.ReadObject(response.GetResponseStream());

            return materials;
        }

        public static Instructor[] getInstructors(string courseId, WebServiceSession session)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);
            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/coursebuilder/instructor-information/courseID/" + courseId);
            request.Headers["Authorization"] = nonceHeader;

            Task<WebResponse> responseTask = request.GetResponseAsync();
            responseTask.Wait();
            WebResponse response = responseTask.Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Instructor[]));
            Instructor[] instructors = (Instructor[])serializer.ReadObject(response.GetResponseStream());

            return instructors;
        }

        public static Announcement[] getAnnouncements(string courseId, WebServiceSession session)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);
            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/announcements/announcement/courseID/" + courseId);
            request.Headers["Authorization"] = nonceHeader;

            Task<WebResponse> responseTask = request.GetResponseAsync();
            responseTask.Wait();
            WebResponse response = responseTask.Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Announcement[]));
            Announcement[] announcements = (Announcement[])serializer.ReadObject(response.GetResponseStream());

            return announcements;
        }

        public static ContentPage[] getContent(string courseId, WebServiceSession session)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);
            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/pages/page-and-content/courseID/" + courseId);
            request.Headers["Authorization"] = nonceHeader;

            Task<WebResponse> responseTask = request.GetResponseAsync();
            responseTask.Wait();
            WebResponse response = responseTask.Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ContentPage[]));
            ContentPage[] contentPages = (ContentPage[])serializer.ReadObject(response.GetResponseStream());

            return contentPages;
        }

        public static Assignment[] getAssignments(string courseId, WebServiceSession session)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);
            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/assignments/assignment/courseID/" + courseId);
            request.Headers["Authorization"] = nonceHeader;

            Task<WebResponse> responseTask = request.GetResponseAsync();
            responseTask.Wait();
            WebResponse response = responseTask.Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Assignment[]));
            Assignment[] assignments = (Assignment[])serializer.ReadObject(response.GetResponseStream());

            return assignments;
        }

        public static CalendarItem[] getCalendarItems(string courseId, WebServiceSession session)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);
            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/calendar/aggregate/courseID/" + courseId);
            request.Headers["Authorization"] = nonceHeader;

            Task<WebResponse> responseTask = request.GetResponseAsync();
            responseTask.Wait();
            WebResponse response = responseTask.Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CalendarItem[]));
            CalendarItem[] calendarItems = (CalendarItem[])serializer.ReadObject(response.GetResponseStream());

            return calendarItems;
        }

//            Console.Write("Enter NetID: ");
//            string username = Console.ReadLine();

//            Console.Write("Enter password: ");
//            string password = Console.ReadLine();

//            WebServiceSession session = NonceAuthentication.GetWsSession(username, password, 5);

//            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);

//            try
//            {
//                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ws.byu.edu/rest/v1.0/academic/registration/studentschedule/" + session.personId + "/20141");
//                request.Method = "GET";
//                //request.Accept = "application/json";
//                request.Headers.Add("Authorization", nonceHeader);
//                //Stream requestStream = request.GetRequestStream();
//                Stream response = request.GetResponse().GetResponseStream();
//                //string testResult = new StreamReader(response).ReadToEnd();

//                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ClassSchedule));
//                ClassSchedule schedule = (ClassSchedule)serializer.ReadObject(response);
//                Console.WriteLine("Email:" + schedule.WeeklySchedService.response.email_address);

                
//            }
//            catch (WebException ex)
//            {
//                Stream errorStream = ex.Response.GetResponseStream();
//                StreamReader streamReader = new StreamReader(errorStream);

//                Console.WriteLine(streamReader.ReadToEnd());
//            }
//            Console.ReadLine();
            
//            /*RecMainServiceType myTest = new RecMainServiceType();
//            myTest.request = new requestType();
//            myTest.request.method = "Courses";
//            myTest.request.attributes = "";
//            myTest.request.resource = username;
//            myTest.request.status = "1";
//            myTest.request.statusMessage = "OK";

//            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RecMainServiceType));
//            //serializer.WriteObject(requestStream, myTest);
//            //requestStream.Close();
            



//            try
//            {
//                Console.WriteLine("result: " + new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd());
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("An error ocurred: " + ex.Message);
//            }*/
//      }
    }
}
