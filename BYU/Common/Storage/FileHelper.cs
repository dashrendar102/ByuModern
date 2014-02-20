using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Common.Storage
{
    public class FileHelper
    {
        public static StorageFolder StorageFolder
        {
            get
            {
                return ApplicationData.Current.LocalFolder;
            }
        }

        public static Uri StorageFolderPath
        {
            get
            {
                return new Uri(StorageFolder.Path);
            }
        }

        //based on http://stackoverflow.com/questions/10836367/download-an-image-to-local-storage-in-metro-style-apps
        public static async Task<Uri> DownloadFile(HttpResponseMessage webResponse, string fileName)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            var fs = await file.OpenAsync(FileAccessMode.ReadWrite);
            DataWriter writer = new DataWriter(fs.GetOutputStreamAt(0));
            writer.WriteBytes(await webResponse.Content.ReadAsByteArrayAsync());
            await writer.StoreAsync();
            writer.DetachStream();
            await fs.FlushAsync();
            return new Uri(file.Path, UriKind.Absolute);
        }

        public static async void DeleteFile(string fileName)
        {
            try
            {
                var file = await StorageFolder.GetFileAsync(fileName);
                if (file != null)
                {
                    await file.DeleteAsync();
                }
            }
            catch (Exception)
            {

            }
        }

        //public static async Task<IRandomAccessStream> OpenReadOnlyFileStream(string fileName)
        //{
        //    return await OpenReadOnlyFileStream(GetFileUri(fileName));
        //}

        public static async Task<IRandomAccessStream> OpenReadOnlyFileStream(Uri uri)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            return await file.OpenAsync(FileAccessMode.Read);
        }
    }
}
