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

        public async static Task<ClassScheduleResponse> GetClassSchedule()
        {
            WebServiceSession session = await WebServiceSession.GetSession();
            Task<string> termTask = TermUtility.TermUtility.getCurrentTerm();
            termTask.Wait();
            string term = termTask.Result;

            ClassScheduleRoot schedule = await BYUWebServiceHelper.GetObjectFromWebService<ClassScheduleRoot>(string.Format(BYUWebServiceURLs.GET_STUDENT_SCHEDULE, session.personId, term));
            return schedule.WeeklySchedService.response;
        }
    }
}
