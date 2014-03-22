using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class StudentInformation
    {
        [DataMember]
        public string student_role { get; set; }

        [DataMember]
        public string year_term { get; set; }

        [DataMember]
        public string credit_hours { get; set; }

        [DataMember]
        public PersonClass[] classes;
    }
}
