using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Common.Storage
{
    internal class WebCache
    {
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

        internal async Task<Stream> GetCachedFileStream(string url)
        {
            string fileName = TransformURLToFilename(url);
            return await FileHelper.OpenReadOnlyFileStream(fileName);
        }

        internal async Task<StorageFile> Cache(string url, Stream dataStream)
        {
            string fileName = TransformURLToFilename(url);
            return await Download(fileName, dataStream);
        }

        internal async Task<StorageFile> Download(string filename, Stream dataStream)
        {
            return await FileHelper.Save(filename, dataStream);
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
