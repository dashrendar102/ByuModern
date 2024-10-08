﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class PersonIdentifiers
    {
        [DataMember]
        public string person_id { get; set; }

        [DataMember]
        public string byu_id { get; set; }

        [DataMember]
        public string byu_id_issue_number { get; set; }

        [DataMember]
        public string net_id { get; set; }

        // We're ignoring this data field for privacy reasons.
        /*[DataMember]
        public string ssn { get; set; }*/
    }
}
