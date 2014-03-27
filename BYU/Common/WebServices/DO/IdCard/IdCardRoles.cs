using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.IdCard
{
    [DataContract]
    public class IdCardRoles
    {
        [DataMember]
        public IdCardRole current { get; set; }

        [DataMember]
        public IdCardRole when_issued { get; set; }
    }
}
