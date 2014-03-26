using Common.WebServices.DO.LearningSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.ClassSchedule
{
    [DataContract]
    public class ClassScheduleRoot
    {
        [DataMember(Name = "WeeklySchedService")]
        public WeeklySchedService WeeklySchedService { get; set; }

        public async static Task<CourseScheduleInformation> GetClassSchedule()
        {
            string term = await TermUtility.TermUtility.getCurrentTerm();
            WebServiceSession session = await WebServiceSession.GetSession();

            ClassScheduleRoot schedule = await BYUWebServiceHelper.GetObjectFromWebService<ClassScheduleRoot>(string.Format(BYUWebServiceURLs.GET_STUDENT_SCHEDULE, session.personId, term));
            await incorporateLearningSuiteCourseInformation(schedule);
            return schedule.WeeklySchedService.response;
        }

        private static async Task incorporateLearningSuiteCourseInformation(ClassScheduleRoot schedule)
        {
            LearningSuiteCourse[] learningSuiteCourses = await LearningSuiteCourse.GetCourses();
            var courses = schedule.WeeklySchedService.response.courseList;
            foreach (var course in courses)
            {
                LearningSuiteCourse matchingLSCourse = learningSuiteCourses.Where(lsCourse => lsCourse.curriculumID.Equals(course.curriculum_id)).SingleOrDefault();
                if (matchingLSCourse == null)
                {
                    throw new Exception("there appears to be a course in the schedule with no learning suite equivalent");
                }
                course.LearningSuiteCourseInformation = matchingLSCourse;
            }
        }
    }
}
