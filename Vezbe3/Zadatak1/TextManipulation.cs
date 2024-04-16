using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    struct WordInformation
    {
        public string word { get;  }
        public int count{ get;  }
        public WordInformation(string word,int count) 
        {
            this.word = word;
            this.count = count;
        }

        public override string ToString()
        {
            return "Word : " + word + " Count : " + count;
        }
    }
    class TextManipulation : IDisposable
    {
        private TextWriter textWriter;
        private TextReader textReader;
        private bool disposed = false;
        string path = "";
        public string Path { get => path; }

        public TextManipulation(string path)
        {
            this.path = path;
        }

        ~TextManipulation()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (textWriter != null)
                    {
                        textWriter.Dispose();
                    }
                    if (textReader != null)
                    {
                        textReader.Dispose();
                    }
                }
                disposed = true;
            }
        }

        public void AddTextToFile(string text)
        {
            textWriter = File.AppendText(path);
            textWriter.WriteLine(text);
            textWriter.Close();
        }

        public string ReadAllText()
        {
            textReader = File.OpenText(path);
            string text = textReader.ReadToEnd();
            textReader.Close();
            return text;
        }

        public List<WordInformation> CountWordsInText(params string[] args)
        {
            string[] allWords = ReadAllText().Replace("\r\n", " ").Replace("\r", " ").Split(' ');
            List<WordInformation> listWords = new List<WordInformation>();

            foreach (string word in args)
            {
                string[] allSameWords = allWords.Where(w => string.Compare(w,word, true) == 0).ToArray();
                int number = allSameWords.Length;
                listWords.Add(new WordInformation(word, number));
            }
            return listWords;
        }       

        public void DeleteAllText()
        {
            File.WriteAllText(path, string.Empty);            
        }
    }
}
