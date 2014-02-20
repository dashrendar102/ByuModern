using Common.WebServices.DOs;
using Common.Extensions;
using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.WebServices.DOs.UserInformation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using Windows.Storage.Streams;
using Windows.Storage;
using Common.Storage;

namespace Common.WebServices
{
    public class AuthenticatedWebServiceFacade
    {
        private BYUWebServiceHelper wsHelper;

        public AuthenticatedWebServiceFacade(string username, string password)
        {
            try
            {
                this.wsHelper = new BYUWebServiceHelper(username, password);
            }
            catch (Exception)
            {

            }
        }

        public Boolean AuthenticationIsValid
        {
            get
            {
                return wsHelper != null;
            }
        }

        public T GetAndDeserializeJson<T>(string url)
        {
            try
            {
                var response = wsHelper.sendAuthenticatedGETRequest(url, "application/json");
                var respStr = response.GetContentAsString();
                return JsonConvert.DeserializeObject<T>(respStr);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public UserInformation LoadUserInformation()
        {
            string url = BYUWebServiceURLs.GetFullURL(BYUWebServiceURLs.GET_PERSONAL_INFO, wsHelper.NetID);
            var root = GetAndDeserializeJson<Common.WebServices.DOs.UserInformation.RootObject>(url);
            return root.PersonSummaryService.response;
        }

        public async Task<Uri> GetPhoto(string fileName)
        {
            string url = BYUWebServiceURLs.GetFullURL(BYUWebServiceURLs.GET_USER_PHOTO_BY_NET_ID, wsHelper.NetID);
            var response = wsHelper.sendAuthenticatedGETRequest(url, "application/xml");

            return await FileHelper.DownloadFile(response, fileName);
        }
    }
}
