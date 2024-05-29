using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ILibrary> factory = new ChannelFactory<ILibrary>("BookReview");
            ILibrary proxy = factory.CreateChannel();

            proxy.Subscribe();
            PrintAllBooks(proxy);

            Console.WriteLine("Input book title as title.txt");
            string fileName = Console.ReadLine();
            try
            {
                proxy.ChangeScore(fileName, 5);
            }
            catch (FaultException<CustomException> ex)
            {
                Console.WriteLine($"ERROR : {ex.Detail.Message}");
            }

            PrintAllBooks(proxy);
            proxy.Unsubscribe();

            Console.ReadKey();
        }

        public static void PrintAllBooks(ILibrary proxy)
        {
            Console.WriteLine("<Press any key for book overview>");
            Console.ReadKey();

            Console.WriteLine("Book Overview:");
            foreach (Book book in proxy.GetAllBooks())
            {
                Console.WriteLine($"Title: {book.Title}  score: {book.Score}");
            }
            Console.WriteLine();
        }
    }
}
