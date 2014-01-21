using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    [DataContract(Name = "WeeklySchedService")]
    public class WeeklySchedService
    {
        [DataMember(Name = "request")]
        public ClassScheduleRequest request { get; set; }

        [DataMember(Name = "response")]
        public ClassScheduleResponse response { get; set; }
    }
}
