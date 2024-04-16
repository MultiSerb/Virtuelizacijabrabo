using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaci123
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Z1
            List<int> listElements = new List<int>(154357);

            for (int i = 0; i < listElements.Capacity; i++)
            {
                if (i % 4 == 0)
                {
                    listElements.Add(i);
                }
                else
                {
                    listElements.Add(-i);
                }
            }

            int sum = 0;
            for (int i = 0; i < listElements.Count; i++)
            {
                if (listElements[i] % 17 == 0)
                {
                    sum += listElements[i];
                }
            }
            Console.WriteLine("Suma je " + sum);
#endregion

            #region Z2
            HashSet<int> hasSetElements = new HashSet<int>(listElements);

            #endregion

            #region Z3
            int count = 100000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                listElements.Contains(-100001);
            }
            stopwatch.Stop();
            Console.WriteLine("List " + stopwatch.Elapsed.TotalMilliseconds);
            stopwatch.Restart();
            for (int i = 0; i < count; i++)
            {
                hasSetElements.Contains(-100001);
            }
            stopwatch.Stop();
            Console.WriteLine("HashSet " + stopwatch.Elapsed.TotalMilliseconds);
            #endregion
        }
    }
}
