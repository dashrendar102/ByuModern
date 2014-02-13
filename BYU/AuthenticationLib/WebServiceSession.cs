using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BYUAuthentication
{
    [DataContract]
    public class WebServiceSession
    {
        [DataMember(Name = "personId")]
        public string personId { get; set; }

        [DataMember(Name = "apiKey")]
        public string apiKey { get; set; }

        [DataMember(Name = "sharedSecret")]
        public string sharedSecret { get; set; }

        [DataMember(Name = "expireDate")]
        public string expireDate { get; set; }
    }
}
