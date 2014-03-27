using System.Runtime.Serialization;

namespace Common.WebServices.DO
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
