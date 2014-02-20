using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleWidget.ScheduledEvents;
using ScheduleWidget.Enums;

namespace Common.CalendarLand
{
	public class RecurringScheduledEvent : ScheduledEvent
	{
		public NodaDateRange DateRange { get; set; }
		public WeeklyRecurrenceInfo WeeklyRecurrenceInfo { get; set; }

		public void PrintOccurrences()
		{
			var testEvent = new Event()
			{
				Title = this.Name,
				FrequencyTypeOptions = FrequencyTypeEnum.Weekly,
				DaysOfWeekOptions = WeeklyRecurrenceInfo.DaysOfWeekOptions,
			};
			var schedule = new Schedule(testEvent);
			var range = new DateRange()
			{
				StartDateTime = DateRange.StartDate.ToDateTimeUtc(),
				EndDateTime = DateRange.EndDate.ToDateTimeUtc()
			};
			foreach (var foo in schedule.Occurrences(range)) {
				Instant dayInstant = new Instant(foo.Ticks);
				ZonedDateTime zdt = dayInstant.InUtc();
				var startTime = zdt.LocalDateTime.Plus(Period.Between(LocalTime.Midnight, TimeRange.StartTime));
				ZonedDateTime startDateTime = Constants.BYUTimeZone.AtStrictly(startTime);
				var endTime = startTime.Plus(TimeRange.NodaPeriod);
				ZonedDateTime endDateTime = Constants.BYUTimeZone.AtStrictly(endTime);
				
                //Console.WriteLine(startDateTime.ToString());
			}
		}
	}
}
