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
        // TODO add a new background task to the package manifest once something's implemented here

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

        private static async Task UpdateTile()
        {
            // Create a tile update manager
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            try
            {
                Assignment[] assignments = await Assignment.GetUpcomingAssignments(3);

                foreach (var assignment in assignments)
                {
                    XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text03);
                    tileXml.GetElementsByTagName(textElementName)[0].InnerText = "Due soon: " + assignment.name;

                    updater.Update(new TileNotification(tileXml));
                }
            }
            catch
            {
                XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text03);
                tileXml.GetElementsByTagName(textElementName)[0].InnerText = "Login to see assignment updates here.";

                updater.Update(new TileNotification(tileXml));
            }
        }
    }
}
