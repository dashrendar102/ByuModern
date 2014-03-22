using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.IdCard
{
    [DataContract]
    public class IdCardResponse
    {
        [DataMember]
        public string card_status { get; set; }

        [DataMember]
        public string ID_card_name { get; set; }

        [DataMember]
        public string ID_card_role { get; set; }

        [DataMember]
        public string byu_id { get; set; }

        [DataMember]
        public string issue_number { get; set; }

        [DataMember]
        public bool dtf { get; set; }

        [DataMember]
        public bool beard { get; set; }

        [DataMember]
        public string expiration_date { get; set; }

        [DataMember]
        public bool unlisted { get; set; }

        [DataMember]
        public bool lost_stolen { get; set; }
    }
}
