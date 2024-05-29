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
        [FaultContract(typeof(CustomException))]
        void AddBookRecommendation(FileManipulationOptions options);

        [OperationContract]
        List<Book> GetAllBooks();

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void ChangeScore(string title, int score);

        [OperationContract]
        void Subscribe();

        [OperationContract]
        void Unsubscribe();
    }
}
