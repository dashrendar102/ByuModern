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
            if (!photoFileExists())
            {
                string photoRequestUrl = BYUWebServiceURLs.GET_USER_PHOTO_BY_PERSON_ID + (await WebServiceSession.GetSession()).personId;
                using (WebResponse response = await BYUWebServiceHelper.sendAuthenticatedGETRequest(photoRequestUrl))
                {
                    Stream photoStream = response.GetResponseStream();

                    IAsyncOperation<StorageFile> getPhotoFileAsync = ApplicationData.Current.LocalFolder.CreateFileAsync(userPhotoName, CreationCollisionOption.ReplaceExisting);
                    StorageFile resultFile = await getPhotoFileAsync.AsTask<StorageFile>();

                    IAsyncOperation<IRandomAccessStream> fileStreamAsync = resultFile.OpenAsync(FileAccessMode.ReadWrite);
                    IRandomAccessStream fileStream = await fileStreamAsync.AsTask<IRandomAccessStream>();

                    using (Stream writeStream = fileStream.GetOutputStreamAt(0).AsStreamForWrite())
                    {

                        byte[] buffer = new byte[1025];
                        int bytesRead = await photoStream.ReadAsync(buffer, 0, 1024);

                        while (bytesRead > 0)
                        {
                            await writeStream.WriteAsync(buffer, 0, bytesRead);

                            bytesRead = await photoStream.ReadAsync(buffer, 0, 1024);
                        }
                    }

                    photoUri = new Uri(resultFile.Path, UriKind.Absolute);
                    return photoUri;
                }
            }
            else
            {
                return photoUri;
            }
        }

        private static bool photoFileExists()
        {
            if (photoUri == null)
            {
                return false;
            }

            try
            {
                IAsyncOperation<StorageFile> getPhotoFileAsync = ApplicationData.Current.LocalFolder.GetFileAsync(userPhotoName);
                Task<StorageFile> getPhotoFileTask = getPhotoFileAsync.AsTask<StorageFile>();
                getPhotoFileTask.Wait();
                StorageFile resultFile = getPhotoFileTask.Result;

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
