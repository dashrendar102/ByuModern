using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class NonceAuthentication
    {
        private const string WS_SESSION_URL = "https://ws.byu.edu/authentication/services/rest/v1/ws/session";
        private const string NONCE_URL = "https://ws.byu.edu/authentication/services/rest/v1/hmac/nonce/";
        private const string NONCE_HEADER = "Nonce-Encoded-WsSession-Key ";

        public static string GetNonceAuthHeader(string netId, string password, int timeout)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WebServiceSession));
            Stream responseStream = SendPost(WS_SESSION_URL,
                "timeout=" + timeout + "&netId=" + netId + "&password=" + password);
            WebServiceSession session = (WebServiceSession)serializer.ReadObject(responseStream);
            responseStream.Close();

            serializer = new DataContractJsonSerializer(typeof(Nonce));
            responseStream = SendPost(NONCE_URL + session.apiKey, null);
            Nonce nonce = (Nonce)serializer.ReadObject(responseStream);

            string nonceHash = GetHmac(session.sharedSecret, nonce.nonceValue);

            return NONCE_HEADER + session.apiKey + "," + nonce.nonceKey + "," + nonceHash;
        }

        public static string GetNonceAuthHeader(WebServiceSession session)
        {
            Stream responseStream = SendPost(NONCE_URL + session.apiKey, null);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Nonce));
            Nonce nonce = (Nonce)serializer.ReadObject(responseStream);

            string nonceHash = GetHmac(session.sharedSecret, nonce.nonceValue);

            return NONCE_HEADER + session.apiKey + "," + nonce.nonceKey + "," + nonceHash;
        }

        public static WebServiceSession GetWsSession(string netId, string password, int timeout)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WebServiceSession));
            Stream responseStream = SendPost(WS_SESSION_URL,
                "timeout=" + timeout + "&netId=" + netId + "&password=" + password);
            WebServiceSession session = (WebServiceSession)serializer.ReadObject(responseStream);
            responseStream.Close();

            return session;
        }

        private static string GetHmac(string sharedSecret, string nonceValue)
        {
            byte[] key = Encoding.UTF8.GetBytes(sharedSecret);
            byte[] value = Encoding.UTF8.GetBytes(nonceValue);
            
            HMACSHA512 hasher = new HMACSHA512(key);
            byte[] hash = hasher.ComputeHash(value);
            return Convert.ToBase64String(hash);
        }

        public static Stream SendPost(string url, string parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "application/json";

            if (parameters != null)
            {
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(parameters);
                writer.Close();
            }

            return request.GetResponse().GetResponseStream();
        }
    }
}
