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
        public FileManipulationOptions(MemoryStream memomoryStream, string keyWord)
        {
            this.MemomoryStream = memomoryStream;
            this.KeyWord = keyWord;
        }

        public FileManipulationOptions(string keyWord)
        {
            this.MemomoryStream = null;
            this.KeyWord = keyWord;
        }

        [DataMember]
        public MemoryStream MemomoryStream { get; set; }

        [DataMember]
        public string KeyWord { get; set; }

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
