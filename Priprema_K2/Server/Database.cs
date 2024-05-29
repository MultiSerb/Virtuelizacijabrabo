using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Database
    {
        readonly static Dictionary<string, Book> collectionOfBooks;

        static Database()
        {
            collectionOfBooks = new Dictionary<string, Book>();
        }

        public static Dictionary<string, Book> CollectionOfBooks { get { return collectionOfBooks; } }
    }
}
