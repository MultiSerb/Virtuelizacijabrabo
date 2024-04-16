using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak3
{
    class Program
    {
        static void Main(string[] args)
        {
            int count;
            byte[] byteArray;
            char[] charArray;
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            Console.WriteLine("Uneste tekst koji želite da pretovorite u niz bajtova");
            string text = Console.ReadLine();
            // Create the data to write to the stream.
            byte[] textInBytes = uniEncoding.GetBytes(text);
            using (MemoryStream memStream = new MemoryStream())
            {
                // Write string to the stream byte by byte.
                count = 0;
                while (count < textInBytes.Length)
                {
                    memStream.WriteByte(textInBytes[count++]);
                }

                // Set the position to the beginning of the stream.
               

                byteArray = new byte[memStream.Length];
                count = memStream.Read(byteArray, 0, byteArray.Length);                

                // Decode the byte array into a char array
                // and write it to the console.
                charArray = new char[uniEncoding.GetCharCount(byteArray, 0, count)];
                uniEncoding.GetDecoder().GetChars(byteArray, 0, count, charArray, 0);
                Console.WriteLine(charArray);
                Console.ReadKey();
            }
        }
    }
}
