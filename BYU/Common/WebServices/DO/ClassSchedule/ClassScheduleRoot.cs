using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.ClassSchedule
{
    [DataContract]
    public class ClassScheduleRoot
    {
        [DataMember(Name = "WeeklySchedService")]
        public WeeklySchedService WeeklySchedService { get; set; }

        public static ClassScheduleResponse GetClassSchedule()
        {
            WebServiceSession session = WebServiceSession.GetSession();
            Task<string> termTask = TermUtility.TermUtility.getCurrentTerm();
            termTask.Wait();
            string term = termTask.Result;

            ClassScheduleRoot schedule = BYUWebServiceHelper.GetObjectFromWebService<ClassScheduleRoot>(string.Format(BYUWebServiceURLs.GET_STUDENT_SCHEDULE, session.personId, term));
            return schedule.WeeklySchedService.response;
        }
    }
}
