using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class PersonClass
    {
        [DataMember]
        public string teaching_area;

        [DataMember]
        public string catalog_entry;

        [DataMember]
        public string instructor;
    }
}
