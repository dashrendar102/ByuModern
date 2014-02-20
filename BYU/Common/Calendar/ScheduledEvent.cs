using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Calendar
{
    public class ScheduledEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public TimeRange TimeRange { get; set; }
        public string ID { get; set; }
    }
}
