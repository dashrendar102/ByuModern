using NodaTime;
using NodaTime.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.Web.Syndication;

using Common.WebServices.DO.LearningSuite;

namespace BackgroundTask
{
    public sealed class LiveTileBackgroundTask : IBackgroundTask
    {
        static string textElementName = "text";

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // Get a deferral, to prevent the task from closing prematurely 
            // while asynchronous code is still running.
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            await UpdateTile();

            // Inform the system that the task is finished.
            deferral.Complete();
        }

        private static int CompareAssignments(Assignment x, Assignment y)
        {
            return x.DueDateTime.CompareTo(y.DueDateTime); // TODO need to make these comparisons by day only and not the time
        }

        private static string FormatAssignment(Assignment assign)
        {
            DateTime today = DateTime.Today;
            if (assign.DueDateTime.Month == today.Month && assign.DueDateTime.Day == today.Day) 
            {
                return assign.DueDateTime.Month + "/" + assign.DueDateTime.Day + " (Today) " + assign.name;
            }

            return assign.DueDateTime.Month + "/" + assign.DueDateTime.Day + " " + assign.name;
        }

        private static async Task UpdateTile()
        {
            // Create a tile update manager
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            try
            {
                Assignment[] assignments = await Assignment.GetUpcomingAssignments(Int16.MaxValue);
                Dictionary<string, List<Assignment>> sortedAssigns = SortAssignments(assignments);

                // create course id to shorttitle map
                LearningSuiteCourse[] courses = await LearningSuiteCourse.GetCourses();
                Dictionary<string, string[]> courseShortTitles = new Dictionary<string, string[]>();
                foreach (var course in courses)
                {
                    try
                    {
                        string[] courseInfo = new string[2];
                        courseInfo[0] = course.shortTitle;
                        courseInfo[1] = course.title;
                        courseShortTitles[course.CourseID] = courseInfo;
                    }
                    catch (ArgumentException)
                    {
                        // skip if for some reason the course is already in the dictionary
                    }
                }

                foreach (KeyValuePair<string, List<Assignment>> kvp in sortedAssigns)
                {
                    XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text01);
                    XmlNodeList textAttrs = tileXml.GetElementsByTagName(textElementName);

                    string[] courseTitle;
                    if (courseShortTitles.TryGetValue(kvp.Key, out courseTitle))
                    {
                        textAttrs[0].InnerText = courseTitle[0] + ": " + courseTitle[1];
                    }

                    // only 4 text fields in TileWide310x150Text01, must use them wisely.
                    // line 0: course
                    // line 1: if no assignments, "0 assignments", otherwise, one of the assignments
                    // line 2: another assignment
                    // line 3: the last assignment if there are only 3 assignments total, or a string indicating how many more there are "+6 more"
                    if (kvp.Value.Count > 0)
                    {
                        textAttrs[1].InnerText = FormatAssignment(kvp.Value[0]);
                        if (kvp.Value.Count > 1) 
                        {
                            textAttrs[2].InnerText = FormatAssignment(kvp.Value[1]);
                            if (kvp.Value.Count > 2) 
                            {
                                if (kvp.Value.Count == 3)
                                {
                                    textAttrs[3].InnerText = FormatAssignment(kvp.Value[2]);
                                }
                                else
                                {
                                    textAttrs[3].InnerText = "+" + (kvp.Value.Count - 2) + " more";
                                }
                            }
                        }
                    }
                    else
                    {
                        textAttrs[1].InnerText = "0 assignments";
                    }

                    updater.Update(new TileNotification(tileXml));
                }
            }
            catch
            {
                // exception gets thrown if nobody is signed in
                XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text03);
                tileXml.GetElementsByTagName(textElementName)[0].InnerText = "Login to see assignment updates here.";

                updater.Update(new TileNotification(tileXml));
            }
        }

        private static Dictionary<string,List<Assignment>> SortAssignments(Assignment[] assignments)
        {
            Dictionary<string, List<Assignment>> buckets = new Dictionary<string, List<Assignment>>();

            foreach (var assignment in assignments)
            {
                try
                {
                    List<Assignment> assignsList = new List<Assignment>();
                    assignsList.Add(assignment);
                    buckets.Add(assignment.courseID, assignsList);
                }
                catch (ArgumentException)
                {
                    // bucket already exists
                    buckets[assignment.courseID].Add(assignment);
                    buckets[assignment.courseID].Sort(CompareAssignments);
                }
            }

            return buckets;
        }
    }
}
