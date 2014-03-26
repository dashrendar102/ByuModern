using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using NodaTime.Text;

namespace Common.WebServices.DO.TermUtility
{
    [DataContract]
    internal class DateRange
    {
        private string startDateString;

        private string endDateString;

        private const string dateFormat = "yyyyMMdd";
        
        [DataMember]
        public string year_term { get; set; }

        private static LocalDatePattern DatePattern
        {
            get
            {
                return LocalDatePattern.Create(dateFormat, CultureInfo.InvariantCulture);
            }
        }

        [DataMember]
        public string start_date
        {
            get { return startDateString; }
            set
            {
                startDateString = value;
                var startDateStr = startDateString.Split(' ')[0];    //time is midnight, not needed
                StartDate = DatePattern.Parse(startDateStr).Value;
            }
        }

        [DataMember]
        public string end_date
        {
            get { return endDateString; }
            set
            {
                endDateString = value;
                var endDateStr = endDateString.Split(' ')[0];    //time is midnight, not needed
                EndDate = DatePattern.Parse(endDateStr).Value;
            }
        }

        [DataMember]
        public string date_type { get; set; }

        public LocalDate StartDate { get; private set; }
        public LocalDate EndDate { get; private set; }
    }
}
