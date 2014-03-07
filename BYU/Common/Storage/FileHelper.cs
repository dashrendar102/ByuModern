﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography.DataProtection;

namespace Common.Storage
{
    public class FileHelper
    {
        private static int BUFFER_SIZE_BYTES = 1024;

        public static StorageFolder RoamingFolder
        {
            get
            {
                return ApplicationData.Current.RoamingFolder;
            }
        }

        public static StorageFolder LocalFolder
        {
            get
            {
                return ApplicationData.Current.LocalFolder;
            }
        }

        // returns a subfolder of the main storage folder, creating it if needed
        internal static async Task<StorageFolder> GetFolder(StorageFolder parent, string folderName)
        {
            return await parent.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
        }

        public static async Task<StorageFile> GetFile(StorageFolder folder, string filename)
        {
            try
            {
                var file = await folder.GetFileAsync(filename);
                return file;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<StorageFile> Save(StorageFolder folder, string filename, Stream dataStream, bool encrypt = true)
        {
            var file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            byte[] buffer = new byte[BUFFER_SIZE_BYTES];

            using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                if (encrypt)
                {
                    var encrypter = new DataProtectionProvider("LOCAL=user");
                    var istream = dataStream.AsInputStream();
                    await encrypter.ProtectStreamAsync(istream, fileStream);
                    await fileStream.FlushAsync();
                }
                else
                {
                    int bytesRead = await dataStream.ReadAsync(buffer, 0, 1024);
                    while (bytesRead > 0)
                    {
                        await fileStream.WriteAsync(buffer.AsBuffer(0, bytesRead));

                        bytesRead = await dataStream.ReadAsync(buffer, 0, BUFFER_SIZE_BYTES);
                    }
                }
            }

            return file;
        }

        public static async Task DeleteFile(StorageFolder folder, string filename)
        {
            try
            {
                var file = await GetFile(folder, filename);
                if (file != null)
                {
                    await file.DeleteAsync();
                }
            }
            catch (Exception)
            {

            }
        }

        public static async Task<Stream> OpenReadOnlyFileStream(StorageFolder folder, string filename, bool decrypt = true)
        {
            var file = await GetFile(folder, filename);
            return await OpenReadOnlyFileStream(file, decrypt);
        }

        public static async Task<Stream> OpenReadOnlyFileStream(StorageFile file, bool decrypt = true)
        {
            if (file == null)
            {
                return null;
            }
            var fileStream = await file.OpenAsync(FileAccessMode.Read);
            if (decrypt)
            {
                var decrypter = new DataProtectionProvider();

                // Create a random access stream to contain the decrypted data.
                InMemoryRandomAccessStream unprotectedDataStream = new InMemoryRandomAccessStream();
                IOutputStream dest = unprotectedDataStream.GetOutputStreamAt(0);
                await decrypter.UnprotectStreamAsync(fileStream, dest);
                await dest.FlushAsync();
                return unprotectedDataStream.GetInputStreamAt(0).AsStreamForRead();
            }
            else
            {
                return fileStream.AsStream();
            }
        }

        public static async Task<bool> FileExists(StorageFolder folder, string filename)
        {
            return (await GetFile(folder, filename)) != null;
        }
    }
}
