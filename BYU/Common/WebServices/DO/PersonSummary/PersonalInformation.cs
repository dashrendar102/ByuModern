using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class PersonalInformation
    {
        [DataMember]
        public string date_of_birth { get; set; }

        [DataMember]
        public int age { get; set; }

        [DataMember]
        public string ethnicity { get; set; }

        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public string marital_status { get; set; }

        [DataMember]
        public string citizenship { get; set; }

        [DataMember]
        public string home_town { get; set; }

        [DataMember]
        public string religion { get; set; }

        [DataMember]
        public bool deceased { get; set; }

        [DataMember]
        public bool restricted_record { get; set; }
    }
}
