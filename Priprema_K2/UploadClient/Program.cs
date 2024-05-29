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
    public class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ILibrary> factory = new ChannelFactory<ILibrary>("Library");
            ILibrary proxy = factory.CreateChannel();

            var uploadPath = ConfigurationManager.AppSettings["uploadPath"];

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            BookRecommendation bookRecommendation = new BookRecommendation(uploadPath, proxy);

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
                        bookRecommendation.CreateFile(uploadPath);
                        break;
                }
            }
            while (selectedNumber != 2);
            Console.ReadKey();
        }

        static int PrintMenu()
        {
            Console.WriteLine("Select an option");
            Console.WriteLine("1. Add book recommendation");
            Console.WriteLine("2. Exit and provide content for recommendations");
            if (Int32.TryParse(Console.ReadLine(), out int number))
            {
                if (number >= 1 && number <= 2)
                {
                    return number;
                }
            }
            return 0;
        }
    }
}
