using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices
{
    [DataContract]
    internal class Nonce
    {
        [DataMember(Name = "nonceKey")]
        internal string nonceKey { get; set; }

        [DataMember(Name = "nonceValue")]
        internal string nonceValue { get; set; }
    }
}
