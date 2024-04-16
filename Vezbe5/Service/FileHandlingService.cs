using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class FileHandlingService : IFileHandling
    {
        [OperationBehavior(AutoDisposeParameters = true)]
        public FileManipulationResults GetFiles(FileManipulationOptions options)
        {

            var results = new FileManipulationResults();
            try
            {
                var fileDirectoryPath = ConfigurationManager.AppSettings["path"];
                if (!Directory.Exists(fileDirectoryPath))
                {
                    results.ResultType = ResultTypes.Warning;
                    results.ResultMessage = "There are no directory for you on service";
                    return results; 
                }

                string[] files = Directory.GetFiles(fileDirectoryPath);
                foreach (string filePath in files)
                {
                    AddMemoryStream(filePath, options.KeyWord, results);
                }

            }
            catch (Exception e)
            {
                results.ResultType = ResultTypes.Failed;
                results.ResultMessage = e.Message;
            }

            return results;
        }

        [OperationBehavior(AutoDisposeParameters = true)]
        public FileManipulationResults SendFile(FileManipulationOptions options)
        {
            FileManipulationResults results = new FileManipulationResults();
            try
            {
                var fileDirectoryPath = ConfigurationManager.AppSettings["path"];
                if (!Directory.Exists(fileDirectoryPath))
                {
                    results.ResultType = ResultTypes.Warning;
                    results.ResultMessage = "There are no directory for you on service";
                    return results;
                }

                if (options.MemomoryStream == null || options.MemomoryStream.Length == 0)
                {
                    results.ResultType = ResultTypes.Warning;
                    results.ResultMessage = "Memory stream does not contain data!";
                    return results;
                }
                SaveFile(options.MemomoryStream, $"{fileDirectoryPath}/{options.KeyWord}");
            }
            catch (Exception ex)
            {
                results.ResultType = ResultTypes.Failed;
                results.ResultMessage = ex.Message;
            }
            return results;
        }

        /// <summary>
        /// If file name start with key words we need to add it to collection
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="results"></param>
        private void AddMemoryStream(string filePath, string keyWord, FileManipulationResults results)
        {
            string fileName = Path.GetFileName(filePath);
            if (fileName.StartsWith(keyWord, StringComparison.CurrentCultureIgnoreCase))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    MemoryStream ms = new MemoryStream();
                    fileStream.CopyTo(ms);
                    results.MemoryStremCollection.Add(fileName, ms);
                }
            }
        }

        private void SaveFile(MemoryStream memoryStream, string filePath)
        {
            using (FileStream fileStream = new FileStream($"{filePath}", FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fileStream);
                fileStream.Dispose();
                memoryStream.Dispose();
            }
        }

    }
}
