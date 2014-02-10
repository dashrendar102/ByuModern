using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.LearningSuiteCourse
{
    [DataContract]
    public class Section
    {
        [DataMember(Name = "courseID")]
        public string courseID { get; set; }

        [DataMember(Name = "curriculumID")]
        public string curriculumID { get; set; }

        [DataMember(Name = "period")]
        public string period { get; set; }

        [DataMember(Name = "sectionNumber")]
        public string sectionNumber { get; set; }

        [DataMember(Name = "titleCode")]
        public string titleCode { get; set; }
    }
}
