﻿using Common.Storage;
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
        private const string userPhotoName = "userPhoto.jpg";

        public async static Task<Uri> getPhotoUri()
        {
            if (photoUri != null)
            {
                return photoUri;
            }
            StorageFile file = await WebCache.Instance.GetDownloadedFile(userPhotoName);
            if (file == null)
            {
                string photoRequestUrl = BYUWebServiceURLs.GET_USER_PHOTO_BY_PERSON_ID + (await WebServiceSession.GetSession()).personId;
                using (WebResponse response = await BYUWebServiceHelper.SendGETRequest(photoRequestUrl))
                {
                    Stream photoStream = response.GetResponseStream();
                    file = await WebCache.Instance.Download(userPhotoName, photoStream, encrypt: false);
                }
            }

            photoUri = new Uri(file.Path, UriKind.Absolute);
            return photoUri;
        }

        private static async Task<bool> photoFileExists()
        {
            if (photoUri == null)
            {
                return false;
            }
            return await WebCache.Instance.IsDownloaded(userPhotoName);
        }
    }
}
