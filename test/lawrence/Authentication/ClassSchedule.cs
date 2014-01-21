using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    [DataContract]
    public class ClassSchedule
    {
        [DataMember(Name = "WeeklySchedService")]
        public WeeklySchedService WeeklySchedService { get; set; }
    }
}
