using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main()
        {
            ChannelFactory<IFileHandling> factory = new ChannelFactory<IFileHandling>("FileHandlingService");
            IFileHandling proxy = factory.CreateChannel();

            int selectedNumber;
            do
            {
                selectedNumber = PrintMenu();
                switch (selectedNumber)
                {
                    case 0:
                        Console.WriteLine("You need to select existing option");
                        break;
                    case 1:
                        SendFileOption(proxy);
                        break;
                    case 2:
                        ReceiveFilesOption(proxy);
                        break;
                }
            }
            while (selectedNumber != 3);
        }
        /// <summary>
        /// Print main menu and return selected option.
        /// </summary>
        /// <returns>
        /// Return selected option if option is not valid 0 is returned.
        /// </returns>
        static int PrintMenu()
        {
            Console.WriteLine("Select an option");
            Console.WriteLine("1. Send Files");
            Console.WriteLine("2. Receive files");
            Console.WriteLine("3. Exit program");
            if(Int32.TryParse(Console.ReadLine(), out int number))
            {
                if (number >= 1 && number <= 3)
                {
                    return number;
                }
            }
            return 0;
        }

        static void SendFileOption(IFileHandling proxy)
        {
            Console.WriteLine("Please input file that you want to sent");
            string fileName = Console.ReadLine();
            FileManipulationResults results =
                proxy.SendFile(new FileManipulationOptions(FileManipulation.GetMemoryStream(fileName), fileName));

            switch (results.ResultType)
            {
                case ResultTypes.Success:
                    Console.WriteLine("File is sucessfuly sent.");
                    break;
                case ResultTypes.Warning:
                    Console.WriteLine($"[WARNING] Send File return message:{results.ResultMessage}");
                    break;
                case ResultTypes.Failed:
                    Console.WriteLine($"[ERROR] Send File return message:{results.ResultMessage}");
                    break;
            }
        }

        static void ReceiveFilesOption(IFileHandling proxy)
        {
            Console.WriteLine("Please input key word for files");
            string keyWord = Console.ReadLine();
            FileManipulationResults results =
            proxy.GetFiles(new FileManipulationOptions(keyWord));

            switch (results.ResultType)
            {
                case ResultTypes.Success:
                    FileManipulation.DownloadFiles(results);
                    break;
                case ResultTypes.Warning:
                    Console.WriteLine($"[WARNING] Receive Files return message:{results.ResultMessage}");
                    break;
                case ResultTypes.Failed:
                    Console.WriteLine($"[ERROR] Receive Files return message:{results.ResultMessage}");
                    break;
            }
        }
    }
}
