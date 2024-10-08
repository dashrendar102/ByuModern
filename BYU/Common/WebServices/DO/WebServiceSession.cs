﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Common.WebServices.DO
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
        private string internalExpireDate { get; set; }

        public DateTime ExpireDate
        {
            get
            {
                return DateTime.Parse(internalExpireDate);
            }
        }

        public async static Task<WebServiceSession> GetSession()
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

                    return await GetSession(passwordCredential.UserName, passwordCredential.Password, DEFAULT_TIMEOUT);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async static Task<WebServiceSession> GetSession(string netId, string password)
        {
            return await GetSession(netId, password, DEFAULT_TIMEOUT);
        }

        public static void LogOut()
        {
            curSession = null;
        }

        private async static Task<WebServiceSession> GetSession(string netId, string password, string timeout)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WebServiceSession));

            try
            {
                string url = BYUWebServiceURLs.GET_WS_SESSION;
                string parameters = "timeout=" + timeout + "&netId=" + Uri.EscapeDataString(netId) + "&password=" + Uri.EscapeDataString(password);
                using (WebResponse response = await BYUWebServiceHelper.SendPost(url, parameters))
                {
                    curSession = (WebServiceSession)serializer.ReadObject(response.GetResponseStream());
                    return curSession;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        private static bool sessionIsValid()
        {
            DateTime curTime = DateTime.Now;

            return curSession != null && curSession.ExpireDate > curTime;
        }
    }
}
