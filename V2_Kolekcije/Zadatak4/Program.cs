using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak4
{
    class Program
    {
        static void Main(string[] args)
        {
            int brojElemenata = 100000;
            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            for (int i = 0; i < brojElemenata; i++)
            {
                if (i % 3 == 0)
                {
                    dictionary.Add(i, i * 4);
                }
                else
                    dictionary.Add(i, i * i);
            }

            Stopwatch stopwatch = new Stopwatch();

            int brojPuta = 1000000, key = 0, value = 0;

            stopwatch.Start();
            for (int i = 0; i < brojPuta; i++)
            {
                if (dictionary.ContainsKey(key))
                    value = dictionary[key];  
            }
            stopwatch.Stop();
            Console.WriteLine("ContainsKey " + stopwatch.Elapsed.TotalMilliseconds);
            stopwatch.Restart();
            for (int i = 0; i < brojPuta; i++)
            {
                dictionary.TryGetValue(key, out value);
            }
            stopwatch.Stop();
            Console.WriteLine("TryGetValue " + stopwatch.Elapsed.TotalMilliseconds);

        }
    }
}
