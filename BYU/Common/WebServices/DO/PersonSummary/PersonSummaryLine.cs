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
        public string email;

        [DataMember]
        public string name;

        [DataMember]
        public string suffix;

        [DataMember]
        public string net_id;

        [DataMember]
        public string byu_id;

        [DataMember]
        public string person_id;

        [DataMember]
        public string date_of_birth;

        [DataMember]
        public string gender;

        [DataMember]
        public string student_role;

        [DataMember]
        public string employee_role;

        [DataMember]
        public bool academic_record;

        [DataMember]
        public bool is_employee;

        [DataMember]
        public bool non_person_organization;

        [DataMember]
        public bool restricted;

        [DataMember]
        public bool deceased;

        [DataMember]
        public bool merge_pending;

        [DataMember]
        public string new_byu_id;
    }
}
