//using Common.WebServices.DO.LearningSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.Web.Syndication;

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

            // Notification 1
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text03);
            tileXml.GetElementsByTagName(textElementName)[0].InnerText = "Go Cougars!";

            updater.Update(new TileNotification(tileXml));

            // Notification 2
            XmlDocument tileXml2 = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text03);
            tileXml2.GetElementsByTagName(textElementName)[0].InnerText = "Don't fail!";

            updater.Update(new TileNotification(tileXml2));

            // Notification 3
            XmlDocument tileXml3 = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text03);
            tileXml3.GetElementsByTagName(textElementName)[0].InnerText = "Keep up the good work!";

            updater.Update(new TileNotification(tileXml3));
        }
    }
}
