﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    [DataContract(Name = "response")]
    public class ClassScheduleResponse
    {
        [DataMember(Name = "sort_name")]
        public string sort_name { get; set; }

        [DataMember(Name = "email_address")]
        public string email_address { get; set; }

        [DataMember(Name = "year_term")]
        public string year_term { get; set; }

        [DataMember(Name = "display_year_term")]
        public string display_year_term { get; set; }

        [DataMember(Name = "start_year_term")]
        public string start_year_term { get; set; }

        [DataMember(Name = "end_year_term")]
        public string end_year_term { get; set; }

        [DataMember(Name = "enrolled")]
        public string enrolled { get; set; }

        [DataMember(Name = "schedule_table")]
        public ScheduleEntry[] schedule_table { get; set; }
    }
}
