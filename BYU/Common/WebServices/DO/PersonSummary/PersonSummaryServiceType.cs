using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    internal class PersonSummaryServiceType
    {
        [DataMember]
        internal PersonSummaryRequest request { get; set; }

        [DataMember]
        internal PersonSummaryResponse response { get; set; }
    }
}
