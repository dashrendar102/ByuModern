using Common.Calendar;
using Common.WebServices;
using Common.WebServices.DO.ClassSchedule;
using Common.WebServices.DO.TermUtility;
using NodaTime;
using NodaTime.Text;
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
                Instant now = Instant.FromDateTimeUtc(DateTime.UtcNow);
                var dt = now.InZone(Constants.BYUTimeZone);
                var ts = dt.Offset.ToTimeSpan();
                Instant otherNow = SystemClock.Instance.Now;
                var offset = Constants.BYUTimeZone.GetUtcOffset(SystemClock.Instance.Now);
                return Constants.BYUTimeZone.GetUtcOffset(SystemClock.Instance.Now).ToTimeSpan();
            }
        }

        private DateTimeOffset CreateDateTimeOffset(LocalDate date, LocalTime time)
        {
            var localDateTime = date + time;
            var zonedDateTime = Constants.BYUTimeZone.AtStrictly(localDateTime);
            return zonedDateTime.ToDateTimeOffset();
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

        public async Task<Appointment> GenerateAppointment(CourseScheduleInformation scheduleInfo, CourseInformation course)
        {
            Appointment appointment = new Appointment();
            appointment.Subject = course.course + " - " + course.course_title;
            appointment.Location = course.room + " " + course.building;
            appointment.Details = "Taught by " + course.instructor;

            TimeRange timeRange = new TimeRange(course.class_period);
            string yearTerm = scheduleInfo.year_term;

            var semesterControlDates = await TermUtility.GetControlDatesByYearTerm(yearTerm, DateType.Semester);
            var semesterDateRange = semesterControlDates.GetDateRangeByType(DateType.Semester);
            var startDate = semesterDateRange.StartDate;
            var endDate = semesterDateRange.EndDate;

            DateTimeOffset start = CreateDateTimeOffset(startDate, timeRange.StartTime);
            DateTimeOffset end = CreateDateTimeOffset(endDate, timeRange.EndTime);

            appointment.StartTime = start;
            appointment.Duration = timeRange.TimeSpan;
            appointment.BusyStatus = AppointmentBusyStatus.Busy;

            //this might be desirable, but it is not as apparent to the user as the details field is
            //var organizer = new AppointmentOrganizer();
            //organizer.DisplayName = course.instructor;
            //organizer.Address = " ";
            //appointment.Organizer = organizer;

            appointment.Recurrence = GenerateCourseRecurrence(course, end);

            return appointment;
        }
    }
}
