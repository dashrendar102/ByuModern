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
        public string date_of_birth;

        [DataMember]
        public int age;

        [DataMember]
        public string ethnicity;

        [DataMember]
        public string gender;

        [DataMember]
        public string marital_status;

        [DataMember]
        public string citizenship;

        [DataMember]
        public string home_town;

        [DataMember]
        public string religion;

        [DataMember]
        public bool deceased;

        [DataMember]
        public bool restricted_record;
    }
}
