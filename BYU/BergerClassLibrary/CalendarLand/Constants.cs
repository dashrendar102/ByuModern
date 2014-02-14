using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerClassLibrary.CalendarLand
{
	internal class Constants
	{
        internal static DateTimeZone BYUTimeZone = NodaTime.DateTimeZoneProviders.Tzdb.GetZoneOrNull("America/Denver");
	}
}
