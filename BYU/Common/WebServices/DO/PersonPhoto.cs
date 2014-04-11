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
        private static Uri photoUri;

        public async static Task<Uri> GetPhotoUriAsync()
        {
            if (photoUri == null)
            {
                string personId = WebServiceSession.GetSession().Result.personId;
                var photoRequestUrl = BYUWebServiceURLs.GET_USER_PHOTO_BY_PERSON_ID + personId;

                var file = await WebCache.Instance.RetrieveFile(photoRequestUrl, useEncryption: false);

                photoUri = new Uri(file.Path, UriKind.Absolute);
            }
            return photoUri;
        }
    }
}
