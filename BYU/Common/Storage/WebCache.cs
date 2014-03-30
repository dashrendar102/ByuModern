using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
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
                return null;
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

        internal async Task<Stream> GetCachedFileStream(string url, bool decrypt = true)
        {
            string fileName = TransformURLToFilename(url);
            return await FileHelper.OpenReadOnlyFileStream(await GetCacheFolder(), fileName, decrypt);
        }

        internal async Task<StorageFile> Cache(string url, Stream dataStream, bool encrypt = true)
        {
            string fileName = TransformURLToFilename(url);
            return await Download(fileName, dataStream, encrypt);
        }

        internal async Task<StorageFile> Download(string filename, Stream dataStream, bool encrypt = true)
        {
            return await FileHelper.Save(await GetCacheFolder(), filename, dataStream, encrypt);
        }

        public async Task<bool> IsCached(string url)
        {
            string filename = TransformURLToFilename(url);
            return await IsDownloaded(filename);
        }

        internal Task<bool> IsDownloaded(string filename)
        {
            return FileHelper.FileExists(cacheFolder, filename);
        }

        public async Task ClearCache()
        {
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
            await FileHelper.DeleteFile(cacheFolder, filename);
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
