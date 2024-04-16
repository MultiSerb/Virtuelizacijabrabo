using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FileManipulationResults : IDisposable
    {
        public FileManipulationResults()
        {
            ResultType = ResultTypes.Success;
            MemoryStremCollection = new Dictionary<string, MemoryStream>();
        }

        [DataMember]
        public Dictionary<string, MemoryStream> MemoryStremCollection { get; set; }

        [DataMember]
        public string ResultMessage { get; set; }

        [DataMember]
        public ResultTypes ResultType { get; set; }

        public void Dispose()
        {
            try
            {
                if (MemoryStremCollection == null)
                    return;
                foreach (KeyValuePair<string, MemoryStream> kvp in MemoryStremCollection)
                {
                    if (kvp.Value != null)
                    {
                        kvp.Value.Dispose();
                        kvp.Value.Close();
                    }
                }
                MemoryStremCollection.Clear();
            }
            catch (Exception)
            {
                Console.WriteLine("Unsuccesful disposing!");
            }
        }
    }
}
