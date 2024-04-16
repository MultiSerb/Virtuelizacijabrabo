using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ILibrary
    {
        [OperationContract]
        bool AddNewBook(Book book);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void DeleteBook(int id);

        [OperationContract]
        List<Book> GetAllBooks();

        [OperationContract]
        List<Book> GetAllBooksByAuthor(string firstName, string lastName);

        [OperationContract]
        List<Book> GetBooksByGener(Genre genre);

        [OperationContract]
        List<Book> GetBooksByYear(int year);
    }
}
