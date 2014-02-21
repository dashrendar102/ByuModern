using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    internal class RootObject
    {
        [DataMember]
        internal PersonSummaryServiceType PersonSummaryService;
    }
}
