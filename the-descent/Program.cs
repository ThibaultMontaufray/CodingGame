using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_descent
{
    class Program
    {
        static void Main(string[] args)
        {
            int highest = 0;
            int indexHighest = 0;
            // game loop
            while (true)
            {
                highest = 0;
                indexHighest = 0;
                for (int i = 0; i < 8; i++)
                {
                    int mountainH = int.Parse(Console.ReadLine()); // represents the height of one mountain, from 9 to 0.

                    if (mountainH > highest)
                    {
                        highest = mountainH;
                        indexHighest = i;
                    }

                    Console.Error.WriteLine("Input " + i + " : " + mountainH);
                }

                Console.WriteLine(indexHighest.ToString()); // The number of the mountain to fire on.
            }
        }
    }
}
