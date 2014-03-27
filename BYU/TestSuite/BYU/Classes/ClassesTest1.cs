using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Common.WebServices.DO.ClassSchedule;
using System.Threading.Tasks;

namespace TestSuite
{
    [TestClass]
    public class ClassesTest1
    {
        // We should look for a way to run tests while retreiving already saved credentials from the password vault.
        [TestMethod]
        public void TestGetCourses()
        {
            Task<CourseScheduleInformation> getCourseTask = ClassScheduleRoot.GetClassSchedule();
            getCourseTask.Wait();
            CourseScheduleInformation info = getCourseTask.Result;
            CourseInformation[] courses = info.courseList;
            Assert.IsTrue(courses.Length > 0);
        }
    }
}
