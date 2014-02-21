using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class WebServiceSession
    {
        private const string DEFAULT_TIMEOUT = "60";

        private static WebServiceSession curSession;

        [DataMember(Name = "personId")]
        public string personId { get; set; }

        [DataMember(Name = "apiKey")]
        public string apiKey { get; set; }

        [DataMember(Name = "sharedSecret")]
        public string sharedSecret { get; set; }

        [DataMember(Name = "expireDate")]
        private string internalExpireDate;

        public DateTime ExpireDate
        {
            get
            {
                return DateTime.Parse(internalExpireDate);
            }
        }

        public static WebServiceSession GetSession(string netId, string password)
        {
            return GetSession(netId, password, DEFAULT_TIMEOUT);
        }

        public static void LogOut()
        {
            curSession = null;
        }

        public static WebServiceSession GetSession(string netId, string password, string timeout)
        {
            if (sessionIsValid())
            {
                return curSession;
            }
            else
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WebServiceSession));
                using (var responseStream = NonceAuthentication.SendPost(NonceAuthentication.WS_SESSION_URL,
                    "timeout=" + timeout + "&netId=" + netId + "&password=" + password))
                {
                    curSession = (WebServiceSession)serializer.ReadObject(responseStream);
                    return curSession;
                }
            }
        }

        private static bool sessionIsValid()
        {
            DateTime curTime = DateTime.Now;

            return curSession != null && curSession.ExpireDate > curTime;
        }
    }
}
