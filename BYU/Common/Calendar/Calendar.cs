using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using Common.WebServices.DO;
using Microsoft.Live;

namespace Common.CalendarLand
{
    public class BYUCalendar
    {
        private List<ScheduledEvent> Events {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        //private string ID;

		public BYUCalendar(string name, string description)
		{
            this.Name = name;
            this.Description = description;
            this.Events = new List<ScheduledEvent>();
        }

        private void CreateWin8Calendar()
        {
            //try
            //{
            //    LiveConnectClient liveClient = new LiveConnectClient(this.session);
            //    var calendar = new Dictionary<string, object>();
            //    calendar.Add("name", this.Name);
            //    calendar.Add("description", this.Description);
            //    LiveOperationResult operationResult = await liveClient.PostAsync("me/calendars", calendar);
            //    dynamic result = operationResult.Result;
            //    this.infoTextBlock.Text = string.Join(" ", "Created calendar:", result.name, "ID:", result.id);
            //}
            //catch (LiveConnectException exception)
            //{
            //    this.infoTextBlock.Text = "Error creating calendar: " + exception.Message;
            //}
            //this.ID = "invalid_id";
        }

        public void ExportToWin8Calendar()
        {
            CreateWin8Calendar();

            foreach (var calEvent in Events)
            {
                //try
                //{
                var calEventDict = new Dictionary<string, object>();
                calEventDict.Add("name", calEvent.Name);
                calEventDict.Add("description", calEvent.Description);
                calEventDict.Add("start_time", calEvent.TimeRange.StartTime.ToString());
                calEventDict.Add("end_time", calEvent.TimeRange.EndTime.ToString());
                calEventDict.Add("location", calEvent.Location);
                calEventDict.Add("is_all_day_event", false);
                calEventDict.Add("availability", "busy");
                calEventDict.Add("visibility", "public");

                //LiveConnectClient liveClient = new LiveConnectClient(this.session);
                //LiveOperationResult operationResult = await liveClient.PostAsync("me/events", calEvent);
                //dynamic result = operationResult.Result;
                //this.infoTextBlock.Text = string.Join(" ", "Created event:", result.name, "ID:", result.id);
                calEvent.ID = "invalidID";
                // }
                //catch (LiveConnectException exception)
                //{
                //    this.infoTextBlock.Text = "Error creating event: " + exception.Message;
                //}
            }
        }

        public void AddEvent(ScheduledEvent calEvent)
        {
            this.Events.Add(calEvent);
        }

        //does not currently handle block classes very well
        public void AddCourse(CourseInfo course)
        {
            TimeRange timeRange = new TimeRange(course.ClassPeriod);
            NodaDateRange courseDates = new NodaDateRange();
			LocalDate localStart = new LocalDate(2014, 1, 6);
			LocalDate localEnd = new LocalDate(2014, 4, 27);
			courseDates.StartDate = localStart.AtMidnight().InUtc();
			courseDates.EndDate = localEnd.AtMidnight().InUtc();

            RecurringScheduledEvent courseEvent = new RecurringScheduledEvent();
            courseEvent.Name = course.Course + " - " +  course.CourseTitle;
            courseEvent.Description = "insert description here";
            courseEvent.Location = course.Room;
            courseEvent.TimeRange = timeRange;
            courseEvent.DateRange = courseDates;
            courseEvent.WeeklyRecurrenceInfo = new WeeklyRecurrenceInfo(course.Days);

			courseEvent.PrintOccurrences();
        }

    }
}
