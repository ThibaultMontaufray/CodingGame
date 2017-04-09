using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temperatures
{
    class Program
    {
        static void Main(string[] args)
        {
            double theTempPos = Double.MaxValue;
            double theTempNeg = Double.MinValue;
            double theTemp, tmpTemp;

            int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
            string temps = Console.ReadLine(); // the n temperatures expressed as integers ranging from -273 to 5526

            if (n != 0)
            {
                foreach (string t in temps.Split(' '))
                {
                    tmpTemp = double.Parse(t);
                    if (tmpTemp > 0 && theTempPos > tmpTemp) { theTempPos = tmpTemp; }
                    else if (tmpTemp < 0 && theTempNeg < tmpTemp) { theTempNeg = tmpTemp; }
                }
                if (Math.Abs(theTempNeg) < theTempPos) { theTemp = theTempNeg; }
                else { theTemp = theTempPos; }
            }
            else { theTemp = 0; }
            Console.WriteLine(theTemp);
        }
    }
}
