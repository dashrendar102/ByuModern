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
        internal string method;

        [DataMember]
        internal string resource;

        [DataMember]
        internal string attributes;

        [DataMember]
        internal int status;

        [DataMember]
        internal string statusMessage;
    }
}
