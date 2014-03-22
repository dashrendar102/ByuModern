using NodaTime;
using NodaTime.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.CalendarLand
{
    public class TimeRange
    {
        public LocalTime StartTime { get; set; }
        public LocalTime EndTime { get; set; }

        //expected string format like "3:15a - 4:00p"
        public TimeRange(string rangeStr)
        {
            var pattern = LocalTimePattern.CreateWithInvariantCulture("h:mmt");
            string[] split = Regex.Split(rangeStr, " - ");
            StartTime = pattern.Parse(split[0]).Value;
            EndTime = pattern.Parse(split[1]).Value;

        }

        public Period NodaPeriod
        {
            get
            {
                return Period.Between(StartTime, EndTime);
            }
        }

        public TimeSpan TimeSpan
        {
            get
            {
                return NodaPeriod.ToDuration().ToTimeSpan();
            }
        }
    }
}
