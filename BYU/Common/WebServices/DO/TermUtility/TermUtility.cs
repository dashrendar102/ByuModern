using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using NodaTime.Text;

namespace Common.WebServices.DO.TermUtility
{
    public class TermUtility
    {
        public static async Task<String> getCurrentTerm()
        {
            return await getYearTerm(DateTime.Now);
        }

        private static string getDateStr(DateTime date)
        {
            return date.Year.ToString("0000")
                        + date.Month.ToString("00")
                        + date.Day.ToString("00");
        }

        public static async Task<String> getYearTerm(DateTime date)
        {
            string url = String.Format(BYUWebServiceURLs.GET_CONTROL_DATES_BY_DATE, getDateStr(date), DateType.Current_YYT.ToString());

            RootObject root = await BYUWebServiceHelper.GetObjectFromWebService<RootObject>(url, authenticate: false);
            DateRange dateRange = root.ControldateswsService.response.GetDateRangeByType(DateType.Current_YYT);
            return dateRange.year_term;
        }

        private static string getDateTypesCSV(DateType[] dateTypes)
        {
            string[] dateTypeNames = dateTypes.Select(dt => dt.ToString()).ToArray();
            return string.Join(",", dateTypeNames);
        }

        internal static async Task<BYUTermControlDates> GetCurrentControlDates(params DateType[] dateTypes)
        {
            DateTime date = DateTime.Now;
            string url = String.Format(BYUWebServiceURLs.GET_CONTROL_DATES_BY_DATE, getDateStr(date), getDateTypesCSV(dateTypes));

            var root = await BYUWebServiceHelper.GetObjectFromWebService<RootObject>(url);
            return root.ControldateswsService.response;
        }

        internal static async Task<BYUTermControlDates> GetControlDatesByYearTerm(string yearTerm, params DateType[] dateTypes)
        {
            string url = String.Format(BYUWebServiceURLs.GET_CONTROL_DATES_BY_YEAR_TERM, yearTerm, getDateTypesCSV(dateTypes));
            var root = await BYUWebServiceHelper.GetObjectFromWebService<RootObject>(url);
            return root.ControldateswsService.response;
        }
    }
}

