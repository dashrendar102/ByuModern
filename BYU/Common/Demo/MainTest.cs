using System.IO;
//using System;
//using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Common.WebServices.DO.LearningSuite;
using Common.WebServices;
using Common.WebServices.DO;

namespace Common
{
    public class MainTest
    {
        public async static Task<string> MainRun()
        {
            WebServiceSession session = await WebServiceSession.GetSession();
            string nonceHeader = await BYUWebServiceHelper.GetNonceAuthHeader();

            HttpWebRequest request = HttpWebRequest.CreateHttp("https://ws.byu.edu/rest/v1.0/learningsuite/coursebuilder/course/personEnrolled/" + session.personId + "/period/20141");
            request.Headers["Authorization"] = nonceHeader;

            WebResponse response = await request.GetResponseAsync();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LearningSuiteCourse[]));
            LearningSuiteCourse[] courses = (LearningSuiteCourse[])serializer.ReadObject(response.GetResponseStream());

            string toReturn = "";

            foreach (LearningSuiteCourse course in courses)
            {
                Assignment[] assignments = await Assignment.GetAssignments(course.CourseID);
                CalendarItem[] calendarItems = await CalendarItem.GetCalendarItems(course.CourseID);
                ContentPage[] contentItems = await ContentPage.GetContentPages(course.CourseID);
                Announcement[] announcements = await Announcement.GetAnnouncements(course.CourseID);
                Instructor[] instructors = await Instructor.GetInstructors(course.CourseID);
                Material[] materials = await Material.GetMaterials(course.CourseID);
                Syllabus[] syllabi = await Syllabus.GetSyllabi(course.CourseID);
                SystemAnnouncement sysAnnouncement = await SystemAnnouncement.GetSystemAnnouncement();

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
    }
}
