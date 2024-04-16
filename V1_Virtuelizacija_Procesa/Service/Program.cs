using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(LibraryService));
            host.Open();

            Console.WriteLine("Service is open, press any key to close it.");
            Console.ReadKey();

            host.Close();
            Console.WriteLine("Service is closed");
        }
    }
}
