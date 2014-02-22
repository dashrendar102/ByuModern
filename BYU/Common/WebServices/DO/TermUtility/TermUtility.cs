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
        internal static String base_url = "https://ws.byu.edu/rest/v1/academic/controls/controldatesws/asofdate/";
            
        public static async Task<String> getCurrentTerm()
        {
            return await getTerm(DateTime.Now);
        }

        public static async Task<String> getTerm(DateTime date)
        {
            String url = base_url + date.Year.ToString("0000")
                        + date.Month.ToString("00")
                        + date.Day.ToString("00");
            url += "/current_yyt";

            String result = await MakeWebRequest(url);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RootObject));
            byte[] byteArray = Encoding.Unicode.GetBytes(result);
            MemoryStream stream = new MemoryStream(byteArray);

            RootObject root = (RootObject)serializer.ReadObject(stream);
            return root.ControldateswsService.response.first_date_list().year_term.ToString();
        }

        internal static async Task<String> MakeWebRequest(String url)
        {
            HttpClient http = new System.Net.Http.HttpClient();
            HttpResponseMessage response = await http.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
