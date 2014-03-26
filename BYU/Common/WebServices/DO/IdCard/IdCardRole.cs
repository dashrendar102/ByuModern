using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.IdCard
{
    [DataContract]
    public class IdCardRole
    {
        [DataMember]
        public string primary_role { get; set; }

        [DataMember]
        public string secondary_role { get; set; }
    }
}
