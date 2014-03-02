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
        private List<DateList> dl;
    
        [DataMember]
        public List<DateList> date_list { get { return dl; } set { dl = value; } }

        public DateList first_date_list()
        {
            if (dl.Count > 0)
                return dl.ElementAt(0);
            else return null;
        }
    }
}
