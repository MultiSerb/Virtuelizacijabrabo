using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    static class FileManipulation
    {
        public static void DownloadFiles(FileManipulationResults results)
        {
            var downloadPath = ConfigurationManager.AppSettings["downloadPath"];
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            foreach (KeyValuePair<string, MemoryStream> stream in results.MemoryStremCollection)
            {
                string fileName = stream.Key;
                using (FileStream fileStream = new FileStream($"{downloadPath}\\{fileName}", FileMode.Create, FileAccess.Write))
                {
                    stream.Value.WriteTo(fileStream);
                    fileStream.Dispose();
                    stream.Value.Dispose();
                    Console.WriteLine($"Downloaded file {stream.Key}");
                }
            }

        }

        /// <summary>
        /// This method return Memory stream with serialize data.
        /// </summary>
        /// <param name="filePath">Path of file for serialization</param>
        /// <returns>Serialize file in Memory Stream </returns>
        public static MemoryStream GetMemoryStream(string fileName)
        {
            var uploadPath = ConfigurationManager.AppSettings["uploadPath"];
            if (!Directory.Exists(uploadPath))
            {
                Console.WriteLine($"Cannot process the file because directory not exists.");
                return null;
            }

            MemoryStream memoryStream = new MemoryStream();
            FileStream fileStream = null;
            string filePath = $"{uploadPath}/{fileName}";
            try
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                fileStream.CopyTo(memoryStream);
                fileStream.Close();
                
            } 
            catch (IOException e)
            {
                Console.WriteLine($"Cannot process the file {filePath}. Message: {e.Message}");  
            } 
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {filePath}.  Message: {e.Message}");                
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }
            return memoryStream;
        }

    }
 }
