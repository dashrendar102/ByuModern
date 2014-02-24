using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    internal class PersonSummaryRequest
    {
        [DataMember]
        internal string method { get; set; }

        [DataMember]
        internal string resource { get; set; }

        [DataMember]
        internal string attributes { get; set; }

        [DataMember]
        internal int status { get; set; }

        [DataMember]
        internal string statusMessage { get; set; }
    }
}
