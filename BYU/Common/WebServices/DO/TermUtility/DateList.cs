using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.TermUtility
{
    [DataContract]
    internal class DateList
    {
        private string sd;

        private string ed;

        private DateTime start;

        private DateTime end;

        [DataMember]
        public string year_term { get; set; }

        [DataMember]
        public string start_date
        {
            get { return sd; }
            set
            {
                sd = value;
                start = DateTime.ParseExact(sd, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }

        [DataMember]
        public string end_date
        {
            get { return ed; }
            set
            {
                ed = value;
                end = DateTime.ParseExact(ed, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }
        public DateTime term_start_date() { return start; }
        public DateTime term_end_date() { return end; }
    }
}
