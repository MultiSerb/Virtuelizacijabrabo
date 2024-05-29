using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UploadClient
{
    public class BookRecommendation
    {
        private FileSystemWatcher fileWatcher;
        private readonly ILibrary proxy;
        
        public BookRecommendation(string path, ILibrary sender)
        {
            CreateFileSystemWatcher(path);
            this.proxy = sender;
        }

        public void CreateFile(string uploadPath)
        {
            Console.WriteLine("Enter book title as title.txt: ");
            string bookTitle = Console.ReadLine();
            try
            {
                using (var fs = File.Create(Path.Combine(uploadPath, bookTitle)))
                {
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR : {e}");
            }
        }

        private void CreateFileSystemWatcher(string path)
        {
            fileWatcher = new FileSystemWatcher()
            {
                Path = path,
                Filter = "*.*",
                NotifyFilter = NotifyFilters.LastWrite
            };

            fileWatcher.Changed += FileChanged;
            fileWatcher.EnableRaisingEvents = true;
        }

        private void FileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                SendFile(e.FullPath, e.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : {ex}");
            }
        }

        private void SendFile(string filePath, string fileName)
        {
            try
            {
                proxy.AddBookRecommendation(new FileManipulationOptions(GetMemoryStream(filePath), fileName));
                Console.WriteLine($"Recommondation for {filePath} modified at {DateTime.Now}");
            }
            catch (FaultException<CustomException> ex)
            {
                Console.WriteLine($"ERROR : {ex.Detail.Message}");
            }

        }

        public static MemoryStream GetMemoryStream(string filePath)
        {
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Cannot process the file because directory not exists.");
                return null;
            }

            MemoryStream memoryStream = new MemoryStream();
            FileStream fileStream = null;
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

