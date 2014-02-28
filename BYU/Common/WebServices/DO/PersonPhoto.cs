using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                using (Stream photoStream = await BYUWebServiceHelper.sendAuthenticatedGETRequest(BYUWebServiceURLs.GET_USER_PHOTO_BY_PERSON_ID + (await WebServiceSession.GetSession()).personId))
                {

                    IAsyncOperation<StorageFile> getPhotoFileAsync = ApplicationData.Current.LocalFolder.CreateFileAsync(userPhotoName, CreationCollisionOption.ReplaceExisting);
                    Task<StorageFile> getPhotoFileTask = getPhotoFileAsync.AsTask<StorageFile>();
                    getPhotoFileTask.Wait();
                    StorageFile resultFile = getPhotoFileTask.Result;

                    IAsyncOperation<IRandomAccessStream> fileStreamAsync = resultFile.OpenAsync(FileAccessMode.ReadWrite);
                    Task<IRandomAccessStream> fileStreamTask = fileStreamAsync.AsTask<IRandomAccessStream>();
                    fileStreamTask.Wait();
                    IRandomAccessStream fileStream = fileStreamTask.Result;

                    using (Stream writeStream = fileStream.GetOutputStreamAt(0).AsStreamForWrite())
                    {

                        byte[] buffer = new byte[1025];
                        Task<int> bytesReadTask = photoStream.ReadAsync(buffer, 0, 1024);
                        bytesReadTask.Wait();
                        int bytesRead = bytesReadTask.Result;

                        while (bytesRead > 0)
                        {
                            Task writeTask = writeStream.WriteAsync(buffer, 0, bytesRead);
                            writeTask.Wait();

                            bytesReadTask = photoStream.ReadAsync(buffer, 0, 1024);
                            bytesReadTask.Wait();
                            bytesRead = bytesReadTask.Result;
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
