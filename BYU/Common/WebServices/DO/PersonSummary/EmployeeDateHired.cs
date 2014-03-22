using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class EmployeeDateHired
    {
        [DataMember]
        public string date { get; set; }

        [DataMember]
        public string qualification { get; set; }

        [DataMember]
        public string years_of_service { get; set; }
    }
}
