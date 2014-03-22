using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.IdCard
{
    [DataContract]
    public class IdCardRoot
    {
        [DataMember(Name = "ID Card Service")]
        public IDCardService idCardService { get; set; }

        public async static Task<IdCardResponse> GetIdCard()
        {
            IdCardRoot root = await BYUWebServiceHelper.GetObjectFromWebService<IdCardRoot>(BYUWebServiceURLs.GET_ID_CARD + (await WebServiceSession.GetSession()).personId);
            return root.idCardService.response;
        }
    }
}
