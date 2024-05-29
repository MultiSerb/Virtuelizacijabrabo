using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public delegate void BookScoreHandler(object sender, BookEventArgs e);

    [DataContract]
    public class Book
    { 
        private string title;
        private int score;

        public Book(string title)
        {
            this.title = title;
            this.score = 0;
        }

        public Book() { }

        [DataMember]
        public string Title { get { return title; } set { title = value; } }

        [DataMember]
        public int Score { get { return score; } set { score = value; } }
    }
}
