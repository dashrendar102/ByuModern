using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BYUAuthentication
{
    [DataContract]
    public class ScheduleEntry
    {
        [DataMember(Name = "sequence")]
        public string sequence { get; set; }

        [DataMember(Name = "course")]
        public string course { get; set; }

        [DataMember(Name = "section")]
        public string section { get; set; }

        [DataMember(Name = "section_type")]
        public string section_type { get; set; }

        [DataMember(Name = "block")]
        public string block { get; set; }

        [DataMember(Name = "curriculum_id")]
        public string curriculum_id { get; set; }

        [DataMember(Name = "title_code")]
        public string title_code { get; set; }

        [DataMember(Name = "lab_quiz_section")]
        public string lab_quiz_section { get; set; }

        [DataMember(Name = "credit_hours")]
        public string credit_hours { get; set; }

        [DataMember(Name = "class_period")]
        public string class_period { get; set; }

        [DataMember(Name = "days")]
        public string days { get; set; }

        [DataMember(Name = "room")]
        public string room { get; set; }

        [DataMember(Name = "building")]
        public string building { get; set; }

        [DataMember(Name = "course_title")]
        public string course_title { get; set; }

        [DataMember(Name = "instructor")]
        public string instructor { get; set; }
    }
}
