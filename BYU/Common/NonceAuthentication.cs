using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;

namespace Common
{
    public class NonceAuthentication
    {
        private const string WS_SESSION_URL = "https://ws.byu.edu/authentication/services/rest/v1/ws/session";
        private const string NONCE_URL = "https://ws.byu.edu/authentication/services/rest/v1/hmac/nonce/";
        private const string NONCE_HEADER = "Nonce-Encoded-WsSession-Key ";

        private static Stream responseStream;

        public static string GetNonceAuthHeader(string netId, string password, int timeout)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WebServiceSession));

            Stream responseStream;
            WebServiceSession session;
            using (responseStream = SendPost(WS_SESSION_URL,
                "timeout=" + timeout + "&netId=" + netId + "&password=" + password))
            {
                session = (WebServiceSession)serializer.ReadObject(responseStream);
            }

            serializer = new DataContractJsonSerializer(typeof(Nonce));
            Nonce nonce;
            using (responseStream = SendPost(NONCE_URL + session.apiKey, null))
            {
                nonce = (Nonce)serializer.ReadObject(responseStream);
            }

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
            using (var responseStream = SendPost(WS_SESSION_URL,
                "timeout=" + timeout + "&netId=" + netId + "&password=" + password))
            {
                WebServiceSession session = (WebServiceSession)serializer.ReadObject(responseStream);
                return session;
            }

        }

        //based on from http://msdn.microsoft.com/en-us/library/windows/apps/windows.security.cryptography.core.macalgorithmprovider.aspx
        private static byte[] CreateHMAC(
            String strMsg,
            String strAlgName,
            byte[] key)
        {
            IBuffer buffMsg;
            IBuffer buffHMAC;

            // Create a MacAlgorithmProvider object for the specified algorithm.
            MacAlgorithmProvider objMacProv = MacAlgorithmProvider.OpenAlgorithm(strAlgName);

            // Demonstrate how to retrieve the name of the algorithm used.
            String strNameUsed = objMacProv.AlgorithmName;

            // Create a buffer that contains the message to be signed.
            BinaryStringEncoding encoding = BinaryStringEncoding.Utf8;
            buffMsg = CryptographicBuffer.ConvertStringToBinary(strMsg, encoding);

            // Create a key to be signed with the message.
            IBuffer buffKey = key.AsBuffer();
            //IBuffer buffKeyMaterial = CryptographicBuffer.GenerateRandom(objMacProv.MacLength);
            CryptographicKey hmacKey = objMacProv.CreateKey(buffKey);

            // Sign the key and message together.
            buffHMAC = CryptographicEngine.Sign(hmacKey, buffMsg);

            // Verify that the HMAC length is correct for the selected algorithm
            if (buffHMAC.Length != objMacProv.MacLength)
            {
                throw new Exception("Error computing digest");
            }
            return buffHMAC.ToArray();
        }

        private static string GetHmac(string sharedSecret, string nonceValue)
        {
            byte[] key = Encoding.UTF8.GetBytes(sharedSecret);
            byte[] value = Encoding.UTF8.GetBytes(nonceValue);

            //HMACSHA512 hasher = new HMACSHA512(key);


            var hash = CreateHMAC(nonceValue, MacAlgorithmNames.HmacSha512, key);

            //byte[] hash = hasher.ComputeHash(value);
            return Convert.ToBase64String(hash);
        }

        public static Stream SendPost(string url, string parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            ManualResetEvent waiter = new ManualResetEvent(false);

            request.Method = "POST";
            request.Accept = "application/json";

            if (parameters != null)
            {
                var getReqStreamTask = request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), Tuple.Create(parameters, request, waiter));

                waiter.WaitOne();
            }
            waiter.Reset();
            var respStreamTask = request.BeginGetResponse(GetResponseStreamCallback, Tuple.Create(request, waiter));
            waiter.WaitOne();
            return responseStream;
        }

        private static void GetResponseStreamCallback(IAsyncResult asynchronousResult)
        {
            var paramTuple = (Tuple<HttpWebRequest, ManualResetEvent>)asynchronousResult.AsyncState;
            var request = paramTuple.Item1;
            var waiter = paramTuple.Item2;
            responseStream = request.EndGetResponse(asynchronousResult).GetResponseStream();
            waiter.Set();
        }

        private static void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            var paramTuple = (Tuple<String, HttpWebRequest, ManualResetEvent>)asynchronousResult.AsyncState;
            String parameters = paramTuple.Item1;
            HttpWebRequest request = paramTuple.Item2;
            var waiter = paramTuple.Item3;

            // End the operation
            using (Stream postStream = request.EndGetRequestStream(asynchronousResult))
            {
                using (var writer = new StreamWriter(postStream))
                {
                    writer.Write(parameters);
                }
            }
            waiter.Set();
        }
    }
}
