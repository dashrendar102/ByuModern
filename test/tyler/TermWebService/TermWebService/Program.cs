using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TermWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime current_date = DateTime.Now;
            String base_url = "https://ws.byu.edu/rest/v1/academic/controls/controldatesws/asofdate/";
            base_url += current_date.Year.ToString("0000")
                        + current_date.Month.ToString("00") 
                        + current_date.Day.ToString("00");
            base_url += "/current_yyt";

            WebClient client = new WebClient();
            String result = client.DownloadString(base_url);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            RootObject test = ser.Deserialize<RootObject>(result);
            String term = test.ControldateswsService.response.first_date_list().year_term;
            Console.WriteLine(term);

            ConsoleKeyInfo key = Console.ReadKey();
        }
    }

    class DateList
    {
        private string yt;
        public string year_term { get { return yt; } set { yt = value; } }
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
