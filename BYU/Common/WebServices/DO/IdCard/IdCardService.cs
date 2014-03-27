using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.IdCard
{
    [DataContract(Name = "ID Card Service")]
    public class IDCardService
    {
        [DataMember(Name = "request")]
        public IdCardRequest request { get; set; }

        [DataMember(Name = "response")]
        public IdCardResponse response { get; set; }
       
    }
}
