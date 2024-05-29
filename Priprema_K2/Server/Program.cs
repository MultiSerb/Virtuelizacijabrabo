using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(LibraryService)))
            {
                host.Open();

                Console.WriteLine("Service is open, press any key to close it");
                Console.ReadKey();

                host.Close();
            }

            Console.WriteLine("Service is closed");
            Console.ReadKey();
        }
    }
}
