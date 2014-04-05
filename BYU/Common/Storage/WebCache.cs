using Common.WebServices;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Common.Storage
{
    public class WebCache
    {
        private StorageFolder cacheFolder;
        private const string cacheFolderName = "webcache";
        private async Task<StorageFolder> GetCacheFolder()
        {
            if (cacheFolder == null)
            {
                cacheFolder = await FileHelper.LocalFolder.CreateFolderAsync(cacheFolderName, CreationCollisionOption.OpenIfExists);
            }
            return cacheFolder;
        }

        //this prevents illegal filename characters like '/' at the expense of filename intelligibility.
        private string TransformURLToFilename(string url)
        {
            return Hash(url);
        }

        private string Hash(string text)
        {
            IBuffer input = CryptographicBuffer.ConvertStringToBinary(text, BinaryStringEncoding.Utf8);
            var hasher = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);

            IBuffer hashed = hasher.HashData(input);

            // format it...
            return CryptographicBuffer.EncodeToHexString(hashed);
        }

        //if the file is not already cached, cache it
        internal async Task<Stream> GetCachedFileStream(string url, bool useEncryption = true, bool authenticate = true, TimeSpan timeout = default(TimeSpan))
        {
            string fileName = TransformURLToFilename(url);
            StorageFile file = await FileHelper.GetFile(await GetCacheFolder(), fileName);
            if (file != null)
            {
                TimeSpan age = DateTimeOffset.Now - file.DateCreated;
                if (timeout == default(TimeSpan) || age < timeout)
                {
                    return await FileHelper.OpenReadOnlyFileStream(file, useEncryption);
                }
                //is this desirable? Should we delete or just ignore it?
                await file.DeleteAsync();
            }
            file = await DownloadToCache(url, fileName, useEncryption, authenticate);
            return await FileHelper.OpenReadOnlyFileStream(file, useEncryption);
        }

        private async Task<StorageFile> DownloadToCache(string url, string fileName, bool useEncryption, bool authenticate)
        {
            //use the application/json accept header because we only support JSON parsing in this method
            using (var response = await BYUWebServiceHelper.SendGetRequest(url, authenticate, "application/json"))
            {
                var file = await Download(url, response.GetResponseStream(), useEncryption);
                return file;
            }
        }

        internal async Task<StorageFile> GetDownloadedFile(string filename)
        {
            var folder = await GetCacheFolder();
            return await FileHelper.GetFile(folder, filename);
        }

        internal async Task<StorageFile> Download(string filename, Stream dataStream, bool encrypt = true)
        {
            return await FileHelper.Save(await GetCacheFolder(), filename, dataStream, encrypt);
        }

        internal async Task<bool> IsDownloaded(string filename)
        {
            return await FileHelper.FileExists(await GetCacheFolder(), filename);
        }

        public async Task ClearCache()
        {
            await GetCacheFolder();
            if (cacheFolder != null)
            {
                await cacheFolder.DeleteAsync();
            }
        }

        internal async Task DeleteCachedItem(string url)
        {
            await DeleteDownloadedItem(TransformURLToFilename(url));
        }

        internal async Task DeleteDownloadedItem(string filename)
        {
            await FileHelper.DeleteFile(await GetCacheFolder(), filename);
        }

        private static WebCache instance;
        public static WebCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WebCache();
                }
                return instance;
            }
        }
        private WebCache()
        {
        }
    }
}
