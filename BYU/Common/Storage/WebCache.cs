using Newtonsoft.Json;
using System;
using System.IO;
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
        private async Task<StorageFolder> GetCacheFolder()
        {
            if (cacheFolder == null)
            {
                cacheFolder = await FileHelper.LocalFolder.CreateFolderAsync(cacheFolderName, CreationCollisionOption.OpenIfExists);
                return null;
            }
            return cacheFolder;
        }
        private const string cacheFolderName = "webcache";

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

        public async Task CacheObject<T>(string identifier, T objectToSerialize, bool encrypt = true)
        {
            try
            {
                string fileName = TransformURLToFilename(identifier);
                Tuple<object> foo = new Tuple<object>(objectToSerialize);
                //string json = JsonConvert.SerializeObject(objectToSerialize);
                //string json2 = JsonConvert.SerializeObject(foo);


                XmlSerializer serializer = new XmlSerializer(typeof(T));

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new System.Text.UnicodeEncoding(); // no BOM in a .NET string
                settings.Indent = false;
                settings.OmitXmlDeclaration = false;

                using (StringWriter textWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                    {
                        serializer.Serialize(xmlWriter, objectToSerialize);
                    }
                    var str = textWriter.ToString();
                    str.ToString();
                }

                //await FileHelper.SaveStringToFile(cacheFolder, fileName, json2, encrypt);
            }
            catch(Exception)
            {
                return;
            }
        }


        public async Task<T> RetrieveObjectFromCache<T>(string identifier, bool decrypt = true)
        {
            try
            {
                string json = await FileHelper.ReadStringFromFile(cacheFolder, TransformURLToFilename(identifier), decrypt);
                var bob = JsonConvert.DeserializeObject<Tuple<T>>(json);
                //DataContractJsonSerializer foo = new DataContractJsonSerializer(typeof(Tuple<T>));
                //var bob2 = JsonConvert.DeserializeObject<Tuple<T>>(json, new JsonSerializerSettings().);
                //bob
                //var klj = 
                return default(T);

            }
            catch (Exception)
            {
                return default(T);
            }
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
