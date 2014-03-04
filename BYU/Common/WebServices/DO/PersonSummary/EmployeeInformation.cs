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
        public string employee_role;

        [DataMember]
        public string department;

        [DataMember]
        public string job_title;

        [DataMember]
        public EmployeeDateHired date_hired;
    }
}
