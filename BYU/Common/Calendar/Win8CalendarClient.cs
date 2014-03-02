//using Common.CalendarLand;
//using Microsoft.Live;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Common.Calendar
//{
//    public class Win8CalendarClient
//    {
//        private string calendarID;
//        public Win8CalendarClient(string calendarID)
//        {
//            this.calendarID = calendarID;
//        }

//        public Win8CalendarClient()
//        {

//        }

//        //exports all scheduled events in cal to the calendar specified by calendarID, creating a new calendar if calendarID is empty
//        public async Task exportCalendar(BYUCalendar cal)
//        {
//            if (string.IsNullOrEmpty(this.calendarID))
//            {
//                this.calendarID = await initNewCalendar(cal);
//            }
//        }

//        private async Task<string> initNewCalendar(BYUCalendar cal)
//        {
//            //try
//            //{
//                LiveConnectClient liveClient = new LiveConnectClient(Session);
//                var calendar = new Dictionary<string, object>();
//                calendar.Add("name", cal.Name);
//                calendar.Add("description", cal.Description);
//                LiveOperationResult operationResult = await liveClient.PostAsync("me/calendars", calendar);
//                dynamic result = operationResult.Result;
//                return result.id;
//            //}
//            //catch (LiveConnectException exception)
//            //{
//            //    this.infoTextBlock.Text = "Error creating calendar: " + exception.Message;
//            //}
//        }

//        private async Task AddEvents(BYUCalendar cal)
//        {
//            foreach (var calEvent in Events)
//            {
//                try
//                {
//                var calEventDict = new Dictionary<string, object>();
//                calEventDict.Add("name", calEvent.Name);
//                calEventDict.Add("description", calEvent.Description);
//                calEventDict.Add("start_time", calEvent.TimeRange.StartTime.ToString());
//                calEventDict.Add("end_time", calEvent.TimeRange.EndTime.ToString());
//                calEventDict.Add("location", calEvent.Location);
//                calEventDict.Add("is_all_day_event", false);
//                calEventDict.Add("availability", "busy");
//                calEventDict.Add("visibility", "public");

//                LiveConnectClient liveClient = new LiveConnectClient(Session);
//                //LiveOperationResult operationResult = await liveClient.PostAsync("me/events", calEvent);
//                //dynamic result = operationResult.Result;
//                //this.infoTextBlock.Text = string.Join(" ", "Created event:", result.name, "ID:", result.id);
//                calEvent.ID = "invalidID";
//                // }
//                //catch (LiveConnectException exception)
//                //{
//                //    this.infoTextBlock.Text = "Error creating event: " + exception.Message;
//                //}
//            }
//        }


//        //static members
//        private static LiveConnectSession session;
//        private static LiveConnectSession Session
//        {
//            get
//            {
//                if (session == null)
//                {
//                    session = getCalendarSession();  
//                }
//                if (session == null)
//                {
//                    throw new Exception("Could not establish a calendar connection");
//                }
//                return session;
//            }
//        }

//        private static LiveConnectSession getCalendarSession()
//        {
//            string[] requiredScopes = new string[] { "wl.calendars_update" };
//            LiveConnectSession result = null;
//            try
//            {
//                LiveAuthClient auth = new LiveAuthClient();
//                Task<LiveLoginResult> loginTask = auth.LoginAsync(requiredScopes);
//                loginTask.Wait();
//                LiveLoginResult loginResult = loginTask.Result;
//                if (loginResult.Status == LiveConnectSessionStatus.Connected)
//                {
//                    result = loginResult.Session;
//                }
//            }
//            catch (LiveAuthException)
//            {
//                //this.infoTextBlock.Text = "Error signing in: " + exception.Message;
//            }
//            return result;
//        }

//    }
//}
