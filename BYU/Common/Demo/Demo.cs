using Common.Calendar;
using Common.Extensions;
using Common.WebServices;
using Common.WebServices.DO;
using Common.WebServices.DO.ClassSchedule;
using Common.WebServices.DO.LearningSuite;
using Common.WebServices.DO.TermUtility;
using Microsoft.Live;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public sealed class BergerDemo
    {
        private const string netID = "";
        private const string pwd = "";
        private const string yearTerm = "20141";

        public void doDemoStuff()
        {
            //const string courseID = "D3pGY5aU0FWK"; //cs428, found by inspecting my learning suite page
            //getStudentSchedule();
            //jsonTest();
            //getAssignmentsByCourseID(courseID);

            //return "demo done";
        }

        public async Task<LiveConnectSessionStatus> getCalendarPermissions()
        {
            try
            {
                LiveAuthClient auth = new LiveAuthClient();
                LiveLoginResult loginResult = await auth.LoginAsync(new string[] { "wl.calendars_update" });
                if (loginResult.Status == LiveConnectSessionStatus.Connected)
                {
                    //this.infoTextBlock.Text = "Signed in.";
                }
                return loginResult.Status;
            }
            catch (LiveAuthException)
            {
                //this.infoTextBlock.Text = "Error signing in: " + exception.Message;
                return LiveConnectSessionStatus.NotConnected;
            }
        }

        private async void getAssignmentsByCourseID(string courseID)
        {
            Assignment[] assignments = await Assignment.GetAssignments(courseID);
            assignments.ToString();
        }

        private async Task<CourseScheduleInformation> getStudentSchedule()
        {
            WebServiceSession session = await WebServiceSession.GetSession();
            Task<string> termTask = TermUtility.getCurrentTerm();
            termTask.Wait();
            string term = termTask.Result;

            return await ClassScheduleRoot.GetClassSchedule();
        }

        //private async void jsonTest()
        //{
        //    ClassScheduleResponse schedule = await getStudentSchedule();
        //    CourseInformation sampleCourse = schedule.courseList[0];
        //    BYUCalendar cal = new BYUCalendar("Course Schedule", "My Current Courses");
        //    cal.AddCourse(sampleCourse);
        //}

    }
}
