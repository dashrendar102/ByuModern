using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleWidget.ScheduledEvents;
using ScheduleWidget.Enums;

namespace WebServiceDemos
{
    public class RecurringScheduledEvent : ScheduledEvent
    {
        public NodaDateRange DateRange { get; set; }
        public WeeklyRecurrenceInfo WeeklyRecurrenceInfo { get; set; }

        private void foo()
        {
            var testEvent = new Event()
            {
                Title = this.Name,
                FrequencyTypeOptions = FrequencyTypeEnum.Weekly,
                DaysOfWeekOptions = WeeklyRecurrenceInfo.DaysOfWeekOptions
            };
            var schedule = new Schedule(testEvent);
            //var range = new DateRange() {
            //    StartDateTime = DateRange.StartDate.to
            //}
            //foreach (var foo in schedule.Occurrences) {

            //}
        }
    }
}
