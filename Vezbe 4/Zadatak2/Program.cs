using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak2
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Unesite putanju fajla");
            string path = Console.ReadLine();

            Console.WriteLine("Unesite tekst, svaki novi red odvojite enterom, ukoliko želite da prekinete unost unestite reč END");
            //Create the file.
            using (FileStream fs = File.Create(path))
            {
                string line = "";
                byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
               
                do
                {
                    line = Console.ReadLine();
                    if (line.Equals("END"))
                    {
                        break;
                    }

                    byte[] lineInBytes = new UTF8Encoding(true).GetBytes(line);
                    fs.Write(lineInBytes, 0, lineInBytes.Length);
                    fs.Write(newline, 0, newline.Length);

                } while (true);         
            }

            //Open the stream and read it back.
            Console.WriteLine($"------------ {path} ----------------- ");
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                int readLen;
                while ((readLen = fs.Read(b, 0, b.Length)) > 0)
                {
                    Console.WriteLine(temp.GetString(b, 0, readLen));
                }
            }

            Console.ReadKey();
        }
    }
}
