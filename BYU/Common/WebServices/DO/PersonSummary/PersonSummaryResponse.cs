using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.PersonSummary
{
    [DataContract]
    public class PersonSummaryResponse
    {
        [DataMember]
        public PersonSummaryLine person_summary_line;

        [DataMember]
        public PersonIdentifiers identifiers;

        [DataMember]
        public PersonNames names;

        [DataMember]
        public PersonalInformation personal_information;

        [DataMember]
        public EmployeeInformation employee_information;

        [DataMember]
        public ContactInformation contact_information;

        [DataMember]
        public StudentInformation student_information;

        [DataMember]
        public Relationship[] relationships;

        public static PersonSummaryResponse GetPersonSummary()
        {
            WebServiceSession session = WebServiceSession.GetSession();
            string personId = session.personId;

            RootObject root = BYUWebServiceHelper.GetObjectFromWebService<RootObject>(BYUWebServiceURLs.GET_PERSONAL_INFO + WebServiceSession.GetSession().personId);
            return root.PersonSummaryService.response;
        }
    }
}
