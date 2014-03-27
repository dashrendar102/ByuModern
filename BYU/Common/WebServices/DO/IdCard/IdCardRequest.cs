using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.IdCard
{
    [DataContract]
    public class IdCardRequest
    {
        [DataMember]
        public string method { get; set; }

        [DataMember]
        public string resource { get; set; }

        [DataMember]
        public string attributes { get; set; }

        [DataMember]
        public int status { get; set; }

        [DataMember]
        public string statusMessage { get; set; }
    }
}
