using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BergerClassLibrary.WebServices.DOs
{
    [JsonConverter(typeof(CamelCaseNameMatchingConverter))]
    internal class Response
    {
        internal string SortName { get; set; }
        internal string EmailAddress { get; set; }
        internal string YearTerm { get; set; }
        internal string DisplayYearTerm { get; set; }
        internal string StartYearTerm { get; set; }
        internal string EndYearTerm { get; set; }
        internal object enrolled { get; set; }

        [JsonProperty(PropertyName = "schedule_table")]
        internal List<CourseInfo> CourseList { get; set; }
    }

    [JsonConverter(typeof(CamelCaseNameMatchingConverter))]
    internal class WeeklySchedService
    {
        internal Response Response { get; set; }
    }

    [JsonConverter(typeof(CamelCaseNameMatchingConverter))]
    internal class RootObject
    {
        internal WeeklySchedService WeeklySchedService { get; set; }
    }
}
