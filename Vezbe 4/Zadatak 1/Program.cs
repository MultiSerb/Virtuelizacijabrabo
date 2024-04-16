using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    class Program
    {
        static void Main()
        {
            DataManipulation.AddDirectory("Direktorijum/Poddirektorijum1");
            DataManipulation.AddDirectory("Direktorijum/Poddirektorijum2");
            DataManipulation.AddDirectory("Direktorijum/Poddirektorijum3");
            DataManipulation.AddDirectory("Direktorijum/Poddirektorijum4");
            DataManipulation.AddDirectory("Direktorijum/Poddirektorijum5");


            DataManipulation.RemoveDirectory("Direktorijum/Poddirektorijum2");

            DataManipulation.CreateEmptyFiles("Direktorijum/Poddirektorijum1", "fajl1.txt", "fajl2.txt", "fajl3.txt");

            printCollection(DataManipulation.GetListOfAllFiles("Direktorijum/Poddirektorijum1"));

            DataManipulation.AddTextToFile("Direktorijum/Poddirektorijum1/fajl1.txt",
                "Novi tekst koji treba da se smesti u fajl1. Kako bismo testirali funkciju ");
            DataManipulation.AddTextToFile("Direktorijum/Poddirektorijum1/fajl1.txt",
                "Sledeci paragraf koji se nalazi u fajlu");
            DataManipulation.AddTextToFile("Direktorijum/Poddirektorijum1/fajl1.txt",
                "Treci paragraf fajla za testiranje");

            DataManipulation.RemoveFile("Direktorijum/Poddirektorijum1/fajl2.txt");

            printCollection(DataManipulation.GetListOfAllFiles("Direktorijum/Poddirektorijum1"));

            Console.WriteLine(DataManipulation.ReadFile("Direktorijum/Poddirektorijum1/fajl1.txt"));

            Console.ReadKey();
        }

        static void printCollection(object[] collection)
        {
            foreach(object obj in collection)
            {
                Console.WriteLine(obj);
            }

        }
    }
}
