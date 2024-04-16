using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    static class DataManipulation
    {
        static public void AddDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static public void RemoveDirectory(string path)
        {
            try
            {
                Directory.Delete(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static public string[] GetListOfAllFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        static public void CreateEmptyFiles(string directoryPath, params string[] fileNames) 
        {
            try
            {
               foreach(string fileName in fileNames)
               {
                    File.Create($"{directoryPath}/{fileName}").Dispose();
               }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static public void RemoveFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static public string ReadFile(string path)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.Peek()>=0)
                {
                    sb.AppendLine(reader.ReadLine());
                }
            }

            return sb.ToString();
        }
        static public void AddTextToFile(string path, string text)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
