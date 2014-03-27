using NodaTime;

namespace Common.Calendar
{
	internal class Constants
	{
        internal static DateTimeZone BYUTimeZone = NodaTime.DateTimeZoneProviders.Tzdb.GetZoneOrNull("US/Mountain");
        
	}
}
