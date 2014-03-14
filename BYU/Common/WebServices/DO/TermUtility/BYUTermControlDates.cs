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
        public List<DateList> date_list { get; set; }

        public DateList first_date_list()
        {
            if (date_list.Count > 0)
                return date_list.ElementAt(0);
            else return null;
        }
    }
}
