using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Common.WebServices
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

        public static WebServiceSession GetSession()
        {
            if (sessionIsValid())
            {
                return curSession;
            }
            else
            {
                PasswordVault vault = new PasswordVault();
                try
                {
                    IReadOnlyList<PasswordCredential> credentialList = vault.FindAllByResource("byu.edu");
                    PasswordCredential passwordCredential = credentialList.FirstOrDefault();
                    passwordCredential.RetrievePassword();

                    return GetSession(passwordCredential.UserName, passwordCredential.Password, DEFAULT_TIMEOUT);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

#if DEBUG

        public static WebServiceSession GetSession(string netId, string password)
        {
            if (sessionIsValid())
            {
                return curSession;
            }
            else
            {
                return GetSession(netId, password, DEFAULT_TIMEOUT);
            }
        }
#endif

        public static void LogOut()
        {
            curSession = null;
        }

        private static WebServiceSession GetSession(string netId, string password, string timeout)
        {
            if (sessionIsValid())
            {
                return curSession;
            }
            else
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WebServiceSession));

                using (Stream responseStream = BYUWebServiceHelper.SendPost(BYUWebServiceURLs.GET_WS_SESSION,
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
