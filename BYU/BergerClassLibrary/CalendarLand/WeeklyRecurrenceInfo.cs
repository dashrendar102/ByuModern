using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ScheduleWidget.Enums;

namespace BergerClassLibrary.CalendarLand
{
    public class WeeklyRecurrenceInfo
    {
        //expects string in the form MWF, TTh, etc
        public WeeklyRecurrenceInfo(string recurrenceStr)
        {
            var matches = Regex.Matches(recurrenceStr, "[SMTWF][ha]?");
            foreach (Match match in matches) {
                switch (match.Value) {
                    case "S":
                        this.OccursOnSunday = true;
                        this.DaysOfWeekOptions |= DayOfWeekEnum.Sun;
                        break;
                    case "M":
                        this.OccursOnMonday = true;
                        this.DaysOfWeekOptions |= DayOfWeekEnum.Mon;
                        break;
                    case "T":
                        this.OccursOnTuesday = true;
                        this.DaysOfWeekOptions |= DayOfWeekEnum.Tue;
                        break;
                    case "W":
                        this.OccursOnWednesday = true;
                        this.DaysOfWeekOptions |= DayOfWeekEnum.Wed;
                        break;
                    case "Th":
                        this.OccursOnThursday = true;
                        this.DaysOfWeekOptions |= DayOfWeekEnum.Thu;
                        break;
                    case "F":
                        this.OccursOnFriday = true;
                        this.DaysOfWeekOptions |= DayOfWeekEnum.Fri;
                        break;
                    case "Sa":
                        this.OccursOnSaturday= true;
                        this.DaysOfWeekOptions |= DayOfWeekEnum.Sat;
                        break;
                }
            }
        }

        public bool OccursOnSunday{ get; set; }
        public bool OccursOnMonday { get; set; }
        public bool OccursOnTuesday { get; set; }
        public bool OccursOnWednesday { get; set; }
        public bool OccursOnThursday { get; set; }
        public bool OccursOnFriday { get; set; }
        public bool OccursOnSaturday { get; set; }

        public DayOfWeekEnum DaysOfWeekOptions { get; private set; }
    }
}
