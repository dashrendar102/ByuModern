using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class Relationship
    {
        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string person_id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public bool deceased { get; set; }

        [DataMember]
        public string employee { get; set; }

        [DataMember]
        public string student { get; set; }

        [DataMember]
        public string affiliation { get; set; }
    }
}
