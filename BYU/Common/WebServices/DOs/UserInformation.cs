using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DOs.UserInformation
{
    internal class Request
    {
        public string method { get; set; }
        public string resource { get; set; }
        public object attributes { get; set; }
        public int status { get; set; }
        public string statusMessage { get; set; }
    }

    public class PersonSummaryLine
    {
        public string email { get; set; }
        public string name { get; set; }
        public object suffix { get; set; }
        public string net_id { get; set; }
        public string byu_id { get; set; }
        public string person_id { get; set; }
        public string date_of_birth { get; set; }
        public string gender { get; set; }
        public string student_role { get; set; }
        public string employee_role { get; set; }
        public bool academic_record { get; set; }
        public bool is_employee { get; set; }
        public bool non_person_organization { get; set; }
        public bool restricted { get; set; }
        public bool deceased { get; set; }
        public bool merge_pending { get; set; }
        public object new_byu_id { get; set; }
    }

    public class Identifiers
    {
        public string person_id { get; set; }
        public string byu_id { get; set; }
        public string byu_id_issue_number { get; set; }
        public string net_id { get; set; }
        //public string ssn { get; set; } we don't need this and there's no reason to store it
    }

    public class Names
    {
        public string preferred_name { get; set; }
        public string complete_name { get; set; }
    }

    public class PersonalInformation
    {
        public string date_of_birth { get; set; }
        public int age { get; set; }
        public string ethnicity { get; set; }
        public string gender { get; set; }
        public string marital_status { get; set; }
        public string citizenship { get; set; }
        public string home_town { get; set; }
        public string religion { get; set; }
        public bool deceased { get; set; }
        public bool restricted_record { get; set; }
    }

    public class DateHired
    {
        public string qualification { get; set; }
    }

    public class EmployeeInformation
    {
        public string employee_role { get; set; }
        public DateHired date_hired { get; set; }
        public string termination_date { get; set; }
    }

    public class ContactInformation
    {
        public List<string> mailing_address { get; set; }
        public bool mailing_address_unlisted { get; set; }
        public string phone_number { get; set; }
        public string mailing_phone { get; set; }
        public bool mailing_phone_unlisted { get; set; }
        public string email { get; set; }
        public string email_address { get; set; }
        public bool email_address_unlisted { get; set; }
    }

    public class Class
    {
        public string teaching_area { get; set; }
        public string catalog_entry { get; set; }
        public string instructor { get; set; }
    }

    public class StudentInformation
    {
        public string student_role { get; set; }
        public string year_term { get; set; }
        public string credit_hours { get; set; }
        public List<Class> classes { get; set; }
    }

    public class UserInformation
    {
        public PersonSummaryLine person_summary_line { get; set; }
        public Identifiers identifiers { get; set; }
        public Names names { get; set; }
        public PersonalInformation personal_information { get; set; }
        public EmployeeInformation employee_information { get; set; }
        public ContactInformation contact_information { get; set; }
        public StudentInformation student_information { get; set; }
    }

    public class PersonSummaryService
    {
        internal Request request { get; set; }
        public UserInformation response { get; set; }
    }

    internal class RootObject
    {
        public PersonSummaryService PersonSummaryService { get; set; }
    }
}
