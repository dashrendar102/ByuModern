using Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceDemos
{
    public class BYUWebServiceHelper
    {
        WebServiceSession session;

        public BYUWebServiceHelper(string netID, string password)
        {
            this.session = NonceAuthentication.GetWsSession(netID, password, 5);
        }

        public WebResponse sendAuthenticatedGETRequest(string url)
        {
            return sendAuthenticatedGETRequest(url, null);
        }

        public WebResponse sendAuthenticatedGETRequest(string url, string acceptString)
        {
            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                if (!string.IsNullOrEmpty(acceptString))
                {
                    request.Accept = acceptString;
                }
                request.Headers.Add("Authorization", nonceHeader);
                var response = request.GetResponse();
                return response;
            }
            catch (WebException ex)
            {
                Stream errorStream = ex.Response.GetResponseStream();
                StreamReader streamReader = new StreamReader(errorStream);

                Console.Error.WriteLine(streamReader.ReadToEnd());
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
