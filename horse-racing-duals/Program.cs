using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace horse_racing_duals
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> horses = new List<int>();
            int lastVal, currentVal;
            int minOffset = int.MaxValue;
            int N = int.Parse(Console.ReadLine());

            lastVal = int.MaxValue;
            for (int i = 0; i < N; i++)
            {
                horses.Add(int.Parse(Console.ReadLine()));
            }

            horses.Sort();
            foreach (int horse in horses)
            {
                currentVal = horse;
                if (Math.Abs(lastVal - currentVal) < minOffset)
                {
                    minOffset = Math.Abs(lastVal - currentVal);
                }
                lastVal = currentVal;
                Console.Error.WriteLine("Val " + currentVal + " offset : " + minOffset);
            }


            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine(minOffset);
        }
    }
}
