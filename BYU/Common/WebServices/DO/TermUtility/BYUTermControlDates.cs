using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.TermUtility
{
    [DataContract]
    internal class BYUTermControlDates
    {
        [DataMember]
        public List<DateRange> date_list { get; set; }

        public DateRange GetDateRangeByType(DateType dateType)
        {
            return date_list.Where(dr => dr.date_type.Equals(dateType.ToString(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}
