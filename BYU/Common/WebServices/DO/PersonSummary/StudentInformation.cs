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
        public string student_role;

        [DataMember]
        public string year_term;

        [DataMember]
        public string credit_hours;

        [DataMember]
        public PersonClass[] classes;
    }
}
