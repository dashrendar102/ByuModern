using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace Common
{
    public static class Constants
    {
        public const string ByuVenueId = "hcl-brighamyounguniversity";
        public const string StadiumVenueId = "hcl-lavelledwardsstadium"; 
        public static DateTimeZone BYUTimeZone = NodaTime.DateTimeZoneProviders.Tzdb.GetZoneOrNull("US/Mountain");
    }
}
