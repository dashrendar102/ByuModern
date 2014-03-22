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
        public string type;

        [DataMember]
        public string person_id;

        [DataMember]
        public string name;

        [DataMember]
        public bool deceased;

        [DataMember]
        public string employee;

        [DataMember]
        public string student;

        [DataMember]
        public string affiliation;
    }
}
