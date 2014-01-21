using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    [DataContract]
    public class Nonce
    {
        [DataMember(Name = "nonceKey")]
        public string nonceKey { get; set; }

        [DataMember(Name = "nonceValue")]
        public string nonceValue { get; set; }
    }
}
