using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class FileManipulationOptions : IDisposable
    {
        public FileManipulationOptions(MemoryStream memomoryStream, string fileName)
        {
            this.MemomoryStream = memomoryStream;
            this.FileName = fileName;
        }

        [DataMember]
        public MemoryStream MemomoryStream { get; set; }

        [DataMember]
        public string FileName { get; set; }

        public void Dispose()
        {
            if (MemomoryStream == null)
                return;
            try
            {
                MemomoryStream.Dispose();
                MemomoryStream.Close();
                MemomoryStream = null;
            }
            catch (Exception)
            {
                Console.WriteLine("Unsuccesful disposing!");
            }
        }
    }
}
