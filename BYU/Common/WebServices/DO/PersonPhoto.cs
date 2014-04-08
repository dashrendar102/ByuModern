using Common.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Common.WebServices.DO
{
    public class PersonPhoto
    {
        private static Uri photoUri = null;
        //private const string userPhotoName = "userPhoto.jpg";
        private static string userPhotoName = null;

        public async static Task<Uri> getPhotoUri()
        {
            if (photoUri != null)
            {
                return photoUri;
            }

            StorageFile file = null;

            if (userPhotoName != null)
            {
                file = await WebCache.Instance.GetDownloadedFile(userPhotoName);
            }

            if (file == null)
            {
                string personId = (await WebServiceSession.GetSession()).personId;
                string photoRequestUrl = BYUWebServiceURLs.GET_USER_PHOTO_BY_PERSON_ID + personId;
                userPhotoName = personId + ".jpg";
                using (WebResponse response = await BYUWebServiceHelper.SendGetRequest(photoRequestUrl))
                {
                    Stream photoStream = response.GetResponseStream();
                    file = await WebCache.Instance.Download(userPhotoName, photoStream, encrypt: false);
                }
            }

            photoUri = new Uri(file.Path, UriKind.Absolute);
            return photoUri;
        }

        public async static Task DeletePhoto()
        {
            photoUri = null;

            if (userPhotoName != null)
            {
                await WebCache.Instance.DeleteDownloadedItem(userPhotoName);
                userPhotoName = null;
            }
        }

        private static async Task<bool> photoFileExists()
        {
            if (photoUri == null || userPhotoName == null)
            {
                return false;
            }

            return await WebCache.Instance.IsDownloaded(userPhotoName);
        }
    }
}
