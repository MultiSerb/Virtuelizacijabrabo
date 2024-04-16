using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Database
    {
        readonly static Dictionary<long, Book> collectionOfBooks;

        static Database()
        {
            collectionOfBooks = new Dictionary<long, Book>();
        }

        public static Dictionary<long, Book> CollectionOfBooks { get { return collectionOfBooks; } }
    
    }
}
