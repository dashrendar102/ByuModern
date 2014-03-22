using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class PersonNames
    {
        [DataMember]
        public string preferred_name { get; set; }

        [DataMember]
        public string complete_name { get; set; }
    }
}
