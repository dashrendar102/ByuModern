using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class PersonSummaryLine
    {
        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string suffix { get; set; }

        [DataMember]
        public string net_id { get; set; }

        [DataMember]
        public string byu_id { get; set; }

        [DataMember]
        public string person_id { get; set; }

        [DataMember]
        public string date_of_birth { get; set; }

        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public string student_role { get; set; }

        [DataMember]
        public string employee_role { get; set; }

        [DataMember]
        public bool academic_record { get; set; }

        [DataMember]
        public bool is_employee { get; set; }

        [DataMember]
        public bool non_person_organization { get; set; }

        [DataMember]
        public bool restricted { get; set; }

        [DataMember]
        public bool deceased { get; set; }

        [DataMember]
        public bool merge_pending { get; set; }

        [DataMember]
        public string new_byu_id { get; set; }
    }
}
