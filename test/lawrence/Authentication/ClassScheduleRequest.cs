using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    [DataContract(Name = "request")]
    public class ClassScheduleRequest
    {
        [DataMember(Name = "method")]
        public string method { get; set; }

        [DataMember(Name = "resource")]
        public string resource { get; set; }

        [DataMember(Name = "attributes")]
        public string attributes { get; set; }

        [DataMember(Name = "status")]
        public int status { get; set; }

        [DataMember(Name = "statusMessage")]
        public string statusMessage { get; set; }
    }
}
