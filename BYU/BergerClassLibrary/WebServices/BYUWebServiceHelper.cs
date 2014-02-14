using BYUAuthentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BergerClassLibrary.WebServices
{
    public sealed class BYUWebServiceHelper
    {
        WebServiceSession session;
        private const int timeout = 5;

        public BYUWebServiceHelper(string netID, string password)
        {
            this.session = NonceAuthentication.GetWsSession(netID, password, timeout);
        }

        internal HttpResponseMessage sendAuthenticatedGETRequest(string url)
        {
            return sendAuthenticatedGETRequest(url, null);
        }

        internal HttpResponseMessage sendAuthenticatedGETRequest(string url, string acceptString)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);

            try
            {
                HttpRequestMessage foo = new HttpRequestMessage(HttpMethod.Get, url);
                foo.Headers.TryAddWithoutValidation("Authorization", nonceHeader);
                if (!string.IsNullOrEmpty(acceptString))
                {
                    foo.Headers.Add("Accept", acceptString);
                }
                HttpClient client = new HttpClient();
                var responseTask = client.SendAsync(foo);
                responseTask.Wait();
                return responseTask.Result;
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                //request.Method = "GET";
                //if (!string.IsNullOrEmpty(acceptString))
                //{
                //    request.Accept = acceptString;
                //}
                //WebHeaderCollection bob;
                //request.Headers = null;
                //request.Headers.Add("Authorization", nonceHeader);
                //var response = request.GetResponse();
                //return response;
            }
            catch (WebException ex)
            {
                Stream errorStream = ex.Response.GetResponseStream();
                StreamReader streamReader = new StreamReader(errorStream);

                //Console.Error.WriteLine(streamReader.ReadToEnd());
                return null;
            }
        }

        public string PersonID
        {
            get
            {
                return session.personId;
            }
        }

    }
}
