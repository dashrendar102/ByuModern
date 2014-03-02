using Common.CalendarLand;
using Common.WebServices.DO.ClassSchedule;
using Common.WebServices.DO.TermUtility;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;

namespace Common.Calendar
{
    public class AppointmentGenerator
    {
        private TimeSpan BYUTimeZoneOffset
        {
            get
            {
                var timeZone = DateTimeZoneProviders.Tzdb["MST"];
                return timeZone.GetUtcOffset(SystemClock.Instance.Now).ToTimeSpan();
            }
        }

        private DateTimeOffset CreateDateTimeOffset(DateTime date, LocalTime time)
        {
            DateTimeOffset result = new DateTimeOffset(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, BYUTimeZoneOffset);
            return result;
        }

        private AppointmentRecurrence GenerateCourseRecurrence(CourseInformation course, DateTimeOffset endDate)
        {
            var recurrence = new AppointmentRecurrence();
            recurrence.Unit = AppointmentRecurrenceUnit.Weekly;
            recurrence.Until = endDate;
            var matches = Regex.Matches(course.days, "[SMTWF][ha]?");
            foreach (Match match in matches)
            {
                switch (match.Value)
                {
                    case "S":
                        recurrence.DaysOfWeek |= AppointmentDaysOfWeek.Sunday;
                        break;
                    case "M":
                        recurrence.DaysOfWeek |= AppointmentDaysOfWeek.Monday;
                        break;
                    case "T":
                        recurrence.DaysOfWeek |= AppointmentDaysOfWeek.Tuesday;
                        break;
                    case "W":
                        recurrence.DaysOfWeek |= AppointmentDaysOfWeek.Wednesday;
                        break;
                    case "Th":
                        recurrence.DaysOfWeek |= AppointmentDaysOfWeek.Thursday;
                        break;
                    case "F":
                        recurrence.DaysOfWeek |= AppointmentDaysOfWeek.Friday;
                        break;
                    case "Sa":
                        recurrence.DaysOfWeek |= AppointmentDaysOfWeek.Saturday;
                        break;
                }
            }
            return recurrence;
        }

        public async Task<Appointment> GenerateAppointment(CourseInformation course)
        {
            Appointment appointment = new Appointment();
            appointment.Subject = course.course + " - " + course.course_title;
            appointment.Location = course.room + " " + course.building;
            //appointment.Details = "Taught by " + course.instructor;
            //TimeZoneInfo.Utc.ge
            //aptmt.

            TimeRange timeRange = new TimeRange(course.class_period);
            var bob = await TermUtility.GetCurrentControlDates();
            var startDate = bob.first_date_list().term_start_date();
            var endDate = bob.first_date_list().term_end_date();

            DateTimeOffset start = CreateDateTimeOffset(startDate, timeRange.StartTime);
            DateTimeOffset end = CreateDateTimeOffset(endDate, timeRange.EndTime);

            appointment.StartTime = start;
            appointment.Duration = timeRange.TimeSpan;

            appointment.BusyStatus = Windows.ApplicationModel.Appointments.AppointmentBusyStatus.Busy;

            var organizer = new Windows.ApplicationModel.Appointments.AppointmentOrganizer();
            organizer.DisplayName = course.instructor;
            organizer.Address = " ";
            appointment.Organizer = organizer;

            appointment.Recurrence = GenerateCourseRecurrence(course, endDate);

            return appointment;
        }
    }
}
