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
        public string[] mailing_address;

        [DataMember]
        public bool mailing_address_unlisted;

        [DataMember]
        public string phone_number;

        [DataMember]
        public string mailing_phone;

        [DataMember]
        public bool mailing_phone_unlisted;

        [DataMember]
        public string email;

        [DataMember]
        public string email_address;

        [DataMember]
        public bool email_address_unlisted;
    }
}
