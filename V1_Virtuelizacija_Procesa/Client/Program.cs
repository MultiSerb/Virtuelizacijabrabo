using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ILibrary> factory = new ChannelFactory<ILibrary>("LibraryService");

            ILibrary proxy = factory.CreateChannel();

            Book book1 = new Book("Scott", "Fitzgerald", "The Great Gatsby", new DateTime(1925,10,1), Genre.Tragedy);
            Book book2 = new Book("Vladimir", "Nabokov", "Lolita", new DateTime(1955,2,5), Genre.Unknown);
            Book book3 = new Book("Hermann", "Hesse", "Siddhartha", new DateTime(1925,5,6), Genre.Fiction);
            Book book4 = new Book("Paulo", " Coelho", "The Alchemist", new DateTime(1988, 8, 10), Genre.Fantasy);
            Book book5 = new Book("Joanne", "Rowling", "Harry Potter : Philosopher's Stone", new DateTime(1997,7,26), Genre.Fantasy);
            Book book6 = new Book("Joanne", "Rowling", "Harry Potter : Chamber of Secrets", new DateTime(1998, 7, 26), Genre.Fantasy);
            Book book7 = new Book("Joanne", "Rowling", "Harry Potter : Prisoner of Azkaban", new DateTime(1999, 7, 26), Genre.Fantasy);
            Book book8 = new Book("Joanne", "Rowling", "Harry Potter : Goblet of Fire", new DateTime(2000, 7, 26), Genre.Fantasy);
            Book book9 = new Book("Gabriel", "García Márquez", "One Hundred Years of Solitude", new DateTime(1967,12,1), Genre.Fiction);
            Book book10 = new Book("Gabriel", "García Márquez", "The Year of the Plague", new DateTime(1979,10,20), Genre.Fiction);

            proxy.AddNewBook(book1);
            proxy.AddNewBook(book1);
            proxy.AddNewBook(book2);
            proxy.AddNewBook(book3);
            proxy.AddNewBook(book4);
            proxy.AddNewBook(book5);
            proxy.AddNewBook(book6);
            proxy.AddNewBook(book7);
            proxy.AddNewBook(book8);
            proxy.AddNewBook(book9);
            proxy.AddNewBook(book10);

            try
            {
                proxy.DeleteBook(4);

            }catch (FaultException<CustomException> e)
            {
                Console.WriteLine($"ERROR : {e.Detail.Message}");
            }

            try
            {
                proxy.DeleteBook(25);

            }
            catch (FaultException<CustomException> e)
            {
                Console.WriteLine($"ERROR : {e.Detail.Message}");
            }

            Console.WriteLine("ALL BOOKS");
            PrintAllBooks(proxy.GetAllBooks());
            Console.WriteLine("GENRE FICTION");
            PrintAllBooks(proxy.GetBooksByGener(Genre.Fiction));
            Console.WriteLine("YEAR 1925");
            PrintAllBooks(proxy.GetBooksByYear(1925));
            Console.WriteLine("BOOKS BY AUTHOR JOANNE ROWLING");
            PrintAllBooks(proxy.GetAllBooksByAuthor("Joanne", "Rowling"));
            Console.Read();
        }

        static void PrintAllBooks(List<Book> books)
        {
            Console.WriteLine("--------------------LIST OF BOOKS-------------------------");
            foreach (Book book in books)
            {
                Console.WriteLine(book.ToString());
            }
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
