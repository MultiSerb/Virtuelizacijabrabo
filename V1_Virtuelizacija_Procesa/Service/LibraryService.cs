using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class LibraryService : ILibrary
    {
        public bool AddNewBook(Book book)
        {
            bool canBeAdded = !Database.CollectionOfBooks.ContainsKey(book.Id);
            if (canBeAdded)
            {
                Database.CollectionOfBooks.Add(book.Id, book);
            }
            return canBeAdded;
        }

        public void DeleteBook(int id)
        {
            if(!Database.CollectionOfBooks.Remove(id))
            {
                throw new FaultException<CustomException>( new CustomException ("Book doesn't exists"));
            }
        }

        public List<Book> GetAllBooksByAuthor(string firstName, string lastName)
        {
            List<Book> resultBooks = new List<Book>();
            foreach(Book book in Database.CollectionOfBooks.Values)
            {
                if(book.WrittenByAuthor(firstName, lastName))
                {
                    resultBooks.Add(book);
                }
            }
            return resultBooks;
        }

        public List<Book> GetAllBooks()
        {
            return new List<Book>(Database.CollectionOfBooks.Values);
        }

        public List<Book> GetBooksByGener(Genre genre)
        {
            List<Book> resultBooks = new List<Book>();
            foreach (Book book in Database.CollectionOfBooks.Values)
            {
                if (book.GenreOfBook.Equals(genre))
                {
                    resultBooks.Add(book);
                }                
            }
            return resultBooks;
        }

        public List<Book> GetBooksByYear(int year)
        {
            List<Book> resultBooks = new List<Book>();
            foreach (Book book in Database.CollectionOfBooks.Values)
            {
                if (book.IsBookPublishOnYear(year))
                {
                    resultBooks.Add(book);
                }
            }
            return resultBooks;
        }
    }
}
