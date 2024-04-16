using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public enum Genre 
    { 
        [EnumMember]Fiction, 
        [EnumMember]Fantasy, 
        [EnumMember]Tragedy, 
        [EnumMember]Unknown
    }

    [DataContract]
    public class Book
    {
        static int count = 1;
        int id;
        string firstName;
        string lastName;
        string title;
        Genre genreOfBook;
        DateTime dateOfPublishing;

        public Book():this("", "", "", new DateTime(),Genre.Unknown) { }
        public Book(string firstName, string lastName, string title, DateTime dateOfPublishing, Genre genreOfBook)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.title = title;
            this.dateOfPublishing = dateOfPublishing;
            this.genreOfBook = genreOfBook;
            id = count++;
        }

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string FirstName { get => firstName; set => firstName = value; }
        [DataMember]
        public string LastName { get => lastName; set => lastName = value; }
        [DataMember]
        public string Title { get => title; set => title = value; }
        [DataMember]
        public DateTime DateOfPublishing { get => dateOfPublishing; set => dateOfPublishing = value; }
        [DataMember]
        public Genre GenreOfBook { get => genreOfBook; set => genreOfBook = value; }

        public bool WrittenByAuthor(string firstName, string lastName)
        {
            return FirstName == firstName && LastName == lastName;
        }

        public bool IsBookPublishOnYear(int year)
        {
            return DateOfPublishing.Year == year;
        }

        public override string ToString()
        {
            return $"ID : {Id} Title : {Title} Author : {FirstName} {LastName} " +
                $"Genre : { GenreOfBook} Date : {DateOfPublishing}";
        }
    }
}
