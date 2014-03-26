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

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Get a deferral, to prevent the task from closing prematurely 
            // while asynchronous code is still running.
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            UpdateTile();

            // Inform the system that the task is finished.
            deferral.Complete();
        }

        private static void UpdateTile()
        {
            // Create a tile update manager
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            // Notification 1
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideText03);
            tileXml.GetElementsByTagName(textElementName)[0].InnerText = "Go Cougars!";

            // Create a new tile notification. 
            updater.Update(new TileNotification(tileXml));
        }
    }
}
