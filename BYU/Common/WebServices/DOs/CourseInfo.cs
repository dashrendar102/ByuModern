using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DOs
{
    [JsonConverter(typeof(CamelCaseNameMatchingConverter))]
    public sealed class CourseInfo
    {
        public string Course { get; set; }
        public string Section { get; set; }
        public string SectionType { get; set; }
        public string Block { get; set; }
        public string Curriculum_Id { get; set; }
        public string TitleCode { get; set; }
        public string LabQuizSection { get; set; }
        public string CreditHours { get; set; }
        public string ClassPeriod { get; set; }
        public string Days { get; set; }
        public string Room { get; set; }
        public string Building { get; set; }
        public string CourseTitle { get; set; }
        public string Instructor { get; set; }
    }
}
