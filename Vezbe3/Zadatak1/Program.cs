using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberMenu = 0;
            while (numberMenu != 2) 
            {
                numberMenu = printMainMenu();
                if(numberMenu == 1)
                {
                    Console.WriteLine("Name of file: ");
                    string path = Console.ReadLine();
                    int functionNumber = 0;
                    TextManipulation textManipulation = new TextManipulation(path);
                    while (functionNumber != 5)
                    {
                        functionNumber = printTextManipulatingMenu();
                        try
                        {
                            executeFunction(functionNumber, textManipulation);
                        }
                        catch (Exception e) {
                            Console.WriteLine("ERROR : " + e.Message);
                            break;
                        }
                    }
                    textManipulation.Dispose();
                }
            } 
        }

        static int printMainMenu()
        {
            Console.WriteLine("MENU");
            Console.WriteLine("1. Manipulate files");
            Console.WriteLine("2. End program");
            return int.Parse(Console.ReadLine());
        }

        static int printTextManipulatingMenu()
        {
            Console.WriteLine("1. Add text");
            Console.WriteLine("2. Read text");
            Console.WriteLine("3. Delete text");
            Console.WriteLine("4. Count words");
            Console.WriteLine("5. Exit");
            return int.Parse(Console.ReadLine());
        }

        static void executeFunction(int functionNumber, TextManipulation textManipulation)
        {
            switch (functionNumber)
            {
                case 1:
                    Console.WriteLine("Unesite tekst");
                    string text = Console.ReadLine();
                    textManipulation.AddTextToFile(text);
                    break;
                case 2:
                    Console.WriteLine("Tekst koji se nalazi u dokumentu");
                    Console.WriteLine(textManipulation.ReadAllText());
                    break;
                case 3:
                    textManipulation.DeleteAllText();
                    break;
                case 4:
                    Console.WriteLine("Unesite reći koje želite da izbrojite odvojene zarezom");
                    string[] words = Console.ReadLine().Replace(" ", "").Split(',');
                    List<WordInformation> list = textManipulation.CountWordsInText(words);
                    foreach (WordInformation word in list)
                    {
                        Console.WriteLine(word);
                    }
                    break;
            }
        }
    }
}
