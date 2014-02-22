using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.TermUtility
{
    [DataContract]
    internal class ControldateswsService
    {
        [DataMember]
        private Response resp;

        [DataMember]
        public Response response { get { return resp; } set { resp = value; } }
    }
}
