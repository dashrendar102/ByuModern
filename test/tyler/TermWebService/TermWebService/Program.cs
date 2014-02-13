using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace TermWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date = DateTime.Now;
            String base_url = "https://ws.byu.edu/rest/v1/academic/controls/controldatesws/asofdate/";
            base_url += date.Year.ToString("0000")
                        + date.Month.ToString("00")
                        + date.Day.ToString("00");
            base_url += "/current_yyt";

            string result = MakeWebRequest(base_url).Result;
            Console.WriteLine(result);

            RootObject root = JsonConvert.DeserializeObject<RootObject>(result);

            String term = root.ControldateswsService.response.first_date_list().year_term;
            Console.WriteLine(term);

            ConsoleKeyInfo key = Console.ReadKey();
        }

        public static async Task<string> MakeWebRequest(string url)
        {
            HttpClient http = new System.Net.Http.HttpClient();
            HttpResponseMessage response = await http.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }

    class DateList
    {
        private string yt;
        private string sd;
        private string ed;
        private DateTime start;
        private DateTime end;
        
        public string year_term { get { return yt; } set { yt = value; } }
        public string start_date { get { return sd; } 
            set { 
                sd = value; 
                start = DateTime.ParseExact(sd, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture); 
            } 
        }
        public string end_date { get { return ed; } 
            set { ed = value; 
                end = DateTime.ParseExact(ed, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture); 
            } 
        }
        public DateTime term_start_date() { return start; }
        public DateTime term_end_date() { return end; }
    }

    class Response
    {
        private List<DateList> dl;
        public List<DateList> date_list { get { return dl; } set { dl = value; } }
        public DateList first_date_list()
        {
            if (dl.Count > 0)
                return dl.ElementAt(0);
            else return null;
        }
    }

    class ControldateswsService
    {
        private Response resp;
        public Response response { get { return resp; } set { resp = value; } }
    }

    class RootObject
    {
        private ControldateswsService service;
        public ControldateswsService ControldateswsService { get { return service; } set { service = value; } }
    }
}
