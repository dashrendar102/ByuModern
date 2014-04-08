using Common;
using Common.Calendar;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.LearningSuite
{
    public class LearningSuiteUtils
    {
        public static ZonedDateTime ConvertTimeStampToDateTime(int? secondsSinceEpoch)
        {
            if (secondsSinceEpoch == null)
            {
                return default(ZonedDateTime);
            }
            int seconds = (int) secondsSinceEpoch;
            Instant instant = Instant.FromSecondsSinceUnixEpoch(seconds);
            ZonedDateTime zdt = new ZonedDateTime(instant, Constants.BYUTimeZone);
            return zdt;
        }
    }
}
