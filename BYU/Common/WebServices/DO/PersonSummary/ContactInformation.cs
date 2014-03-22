using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class ContactInformation
    {
        [DataMember]
        public string[] mailing_address { get; set; }

        [DataMember]
        public bool mailing_address_unlisted { get; set; }

        [DataMember]
        public string phone_number { get; set; }

        [DataMember]
        public string mailing_phone { get; set; }

        [DataMember]
        public bool mailing_phone_unlisted { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string email_address { get; set; }

        [DataMember]
        public bool email_address_unlisted { get; set; }
    }
}
