using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.TermUtility
{
    public class TermUtility
    {


        public static async Task<String> getCurrentTerm()
        {
            return await getTerm(DateTime.Now);
        }

        private static string getDateStr(DateTime date)
        {
            return date.Year.ToString("0000")
                        + date.Month.ToString("00")
                        + date.Day.ToString("00");
        }

        public static async Task<String> getTerm(DateTime date)
        {
            string url = String.Format(BYUWebServiceURLs.GET_CONTROL_DATES, getDateStr(date));

            WebServiceSession session = await WebServiceSession.GetSession();
            string personId = session.personId;

            RootObject root = await BYUWebServiceHelper.GetObjectFromWebService<RootObject>(url);
            return root.ControldateswsService.response.first_date_list().year_term.ToString();
        }

        internal static async Task<BYUTermControlDates> GetCurrentControlDates()
        {
            DateTime date = DateTime.Now;
            string url = String.Format(BYUWebServiceURLs.GET_CONTROL_DATES, getDateStr(date));

            WebServiceSession session = await WebServiceSession.GetSession();
            string personId = session.personId;

            var root = await BYUWebServiceHelper.GetObjectFromWebService<RootObject>(url);
            return root.ControldateswsService.response;
        }
    }
}
