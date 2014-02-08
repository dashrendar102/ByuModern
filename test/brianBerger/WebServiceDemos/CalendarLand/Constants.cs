using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceDemos.CalendarLand
{
	public class Constants
	{
		public static DateTimeZone BYUTimeZone = NodaTime.DateTimeZoneProviders.Tzdb.GetZoneOrNull("America/Denver");
	}
}
