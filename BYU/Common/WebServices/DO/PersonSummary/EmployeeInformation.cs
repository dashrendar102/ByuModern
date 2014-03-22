using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class EmployeeInformation
    {
        [DataMember]
        public string employee_role { get; set; }

        [DataMember]
        public string department { get; set; }

        [DataMember]
        public string job_title { get; set; }

        [DataMember]
        public EmployeeDateHired date_hired { get; set; }
    }
}
