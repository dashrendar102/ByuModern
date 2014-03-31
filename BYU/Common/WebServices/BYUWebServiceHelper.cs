using Common.Storage;
using Common.WebServices.DO;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace Common.WebServices
{
    public class BYUWebServiceHelper
    {
        private const string NONCE_HEADER = "Nonce-Encoded-WsSession-Key ";

        public async static Task<string> GetNonceAuthHeader()
        {
            WebServiceSession session = await WebServiceSession.GetSession();

            using (WebResponse response = await SendPost(BYUWebServiceURLs.GET_NONCE_URL + session.apiKey, null))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Nonce));
                Nonce nonce = (Nonce)serializer.ReadObject(response.GetResponseStream());

                string nonceHash = GetHmac(session.sharedSecret, nonce.nonceValue);

                return NONCE_HEADER + session.apiKey + "," + nonce.nonceKey + "," + nonceHash;
            }
        }

        public async static Task<WebResponse> SendGETRequest(string url, bool authenticate = true, string acceptString = null)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp(url);
                if (authenticate)
                {
                    string nonceHeader = await GetNonceAuthHeader();
                    request.Headers["Authorization"] = nonceHeader;
                }

                if (!string.IsNullOrEmpty(acceptString))
                {
                    request.Accept = acceptString;
                }

                return await request.GetResponseAsync();
            }
            catch (WebException)
            {
                //Stream errorStream = ex.Response.GetResponseStream();
                //StreamReader streamReader = new StreamReader(errorStream);

                //Console.Error.WriteLine(streamReader.ReadToEnd());
                return null;
            }
        }

        public async static Task<T> GetObjectFromWebService<T>(string url, bool authenticate = true, bool allowCache = true)
        {
            if (WebServiceSession.GetSession() == null)
            {
                return default(T);
            }
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

            if (allowCache)
            {
                using (Stream dataStream = await WebCache.Instance.GetCachedFileStream(url))
                {
                    if (dataStream != null)
                    {
                        return (T)serializer.ReadObject(dataStream);
                    }
                }
                //use the application/json accept header because we only support JSON parsing in this method
                using (var response = await SendGETRequest(url, authenticate, "application/json"))
                {
                    var file = await WebCache.Instance.Cache(url, response.GetResponseStream());
                    using (Stream dataStream = await FileHelper.OpenReadOnlyFileStream(file))
                    {
                        return (T)serializer.ReadObject(dataStream);
                    }
                }
            }
            else
            {
                //use the application/json accept header because we only support JSON parsing in this method
                using (var response = await SendGETRequest(url, authenticate, "application/json"))
                {
                    return (T)serializer.ReadObject(response.GetResponseStream());
                }
            }

        }

        internal async static Task<WebResponse> SendPost(string url, string parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.Accept = "application/json";

            if (parameters != null)
            {
                using (Stream requestStream = await request.GetRequestStreamAsync())
                {
                    using (StreamWriter requestWriter = new StreamWriter(requestStream))
                    {
                        requestWriter.Write(parameters);
                    }
                }
            }

            return await request.GetResponseAsync();
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

            byte[] hash = CreateHMAC(nonceValue, MacAlgorithmNames.HmacSha512, key);

            //byte[] hash = hasher.ComputeHash(value);
            return Convert.ToBase64String(hash);
        }
    }
}
