using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Common.Storage
{
    public class FileHelper
    {
        private static int BUFFER_SIZE_BYTES = 1024;

        public static StorageFolder StorageFolder
        {
            get
            {
                return ApplicationData.Current.RoamingFolder;
            }
        }

        public static Uri StorageFolderPath
        {
            get
            {
                return new Uri(StorageFolder.Path);
            }
        }

        public static Uri GetFileUri(string filename)
        {
            return new Uri(StorageFolderPath, filename);
        }

        public static async Task<StorageFile> GetFile(string filename)
        {
            try
            {
                var file = await StorageFolder.GetFileAsync(filename);
                return file;
            } catch (Exception)
            {
                 return null;
            }
        }

        public static async Task<StorageFile> Save(string filename, Stream dataStream)
        {
            var file = await StorageFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            
            byte[] buffer = new byte[BUFFER_SIZE_BYTES];

            using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                int bytesRead = await dataStream.ReadAsync(buffer, 0, 1024);
                while (bytesRead > 0)
                {
                    await fileStream.WriteAsync(buffer.AsBuffer(0, bytesRead));
                    //await fileStream.WriteAsync(buffer, 0, bytesRead);

                    bytesRead = await dataStream.ReadAsync(buffer, 0, BUFFER_SIZE_BYTES);
                }
            }

            return file;
        }

        public static async void DeleteFile(string filename)
        {
            try
            {
                var file = await GetFile(filename);
                if (file != null)
                {
                    await file.DeleteAsync();
                }
            }
            catch (Exception)
            {

            }
        }

        public static async Task<Stream> OpenReadOnlyFileStream(string filename)
        {
            return await OpenFileStream(filename, FileAccessMode.Read);
        }

        public static async Task<Stream> OpenReadOnlyFileStream(StorageFile file)
        {
            return await OpenFileStream(file, FileAccessMode.Read);
        }

        public static async Task<Stream> OpenWritableFileStream(StorageFile file)
        {
            return await OpenFileStream(file, FileAccessMode.ReadWrite);
        }

        public static async Task<Stream> OpenWritableFileStream(string filename)
        {
            return await OpenFileStream(filename, FileAccessMode.ReadWrite);
        }

        public static async Task<Stream> OpenFileStream(string filename, FileAccessMode accessMode)
        {
            var file = await GetFile(filename);
            return await OpenFileStream(file, accessMode);
        }

        public static async Task<Stream> OpenFileStream(StorageFile file, FileAccessMode accessMode)
        {
            if (file == null)
            {
                return null;
            }

            var stream = await file.OpenAsync(accessMode);

            return stream.AsStream();
        }

        public static async Task<bool> FileExists(string filename)
        {
            return (await GetFile(filename)) != null;
        }
    }
}
