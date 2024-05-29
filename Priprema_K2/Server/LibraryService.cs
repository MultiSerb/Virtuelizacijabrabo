using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class LibraryService : ILibrary
    {
        private readonly LibrarySubscriber subscriber = new LibrarySubscriber();
        public event BookScoreHandler BookScoreChanged;

        private readonly string fileDirectoryPath = ConfigurationManager.AppSettings["bookPath"];

        #region AddBookRecommendation
        [OperationBehavior(AutoDisposeParameters = true)]
        public void AddBookRecommendation(FileManipulationOptions options)
        {
            try
            {
                if (!Directory.Exists(fileDirectoryPath))
                {
                    Directory.CreateDirectory(fileDirectoryPath);
                }

                if (options.MemomoryStream == null || options.MemomoryStream.Length == 0)
                {
                    throw new FaultException<CustomException>(new CustomException($"No content provided for book {options.FileName}"));
                }

                SaveFile(options.MemomoryStream, $"{fileDirectoryPath}/{options.FileName}");
            }
            catch (Exception ex)
            {
                throw new FaultException<CustomException>(new CustomException(ex.Message));
            }
        }

        private void SaveFile(MemoryStream memoryStream, string filePath)
        {
            using (FileStream fileStream = new FileStream($"{filePath}", FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fileStream);
                fileStream.Dispose();
                memoryStream.Dispose();
            }

        }
        #endregion method

        #region GetAllBooks
        private void DatabaseSetUp()
        {
                string[] files = Directory.GetFiles(fileDirectoryPath);
                foreach (string filePath in files)
                {
                    Database.CollectionOfBooks.Add(Path.GetFileName(filePath), new Book(Path.GetFileName(filePath).Split('.')[0]));
                }
        }

        public List<Book> GetAllBooks()
        {
            if(Database.CollectionOfBooks.Count == 0)
            {
                DatabaseSetUp();
            }
           
            return new List<Book>(Database.CollectionOfBooks.Values);
        }
        #endregion


        public void ChangeScore(string title, int score)
        {
            if(Database.CollectionOfBooks.TryGetValue(title, out Book book))
            {
                if(BookScoreChanged != null)
                {
                    BookScoreChanged(this, new BookEventArgs(book.Title, book.Score, score));
                    book.Score = score;
                }
                return;
            }
            throw new FaultException<CustomException>(new CustomException($"Book {title} not found"));

        }

        public void Subscribe()
        {
            BookScoreChanged += subscriber.OnRaitingChanged;
        }

        public void Unsubscribe()
        {
            BookScoreChanged -= subscriber.OnRaitingChanged;
        }
    }
}

