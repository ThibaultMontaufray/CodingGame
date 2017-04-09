using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ascii_art
{
    class Program
    {
        //private char[] _alpha = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
        private static string _alpha = "abcdefghijklmnopqrstuvwxyz";
        static void Main(string[] args)
        {
            int L = int.Parse(Console.ReadLine());
            int H = int.Parse(Console.ReadLine());
            string T = Console.ReadLine();
            List<string> ascii = new List<string>();
            string outputRow;

            Console.Error.WriteLine(string.Format("[{0}]", T));

            int charPos;

            for (int i = 0; i < H; i++)
            {
                ascii.Add(Console.ReadLine());
                Console.Error.WriteLine(string.Format("[{0}]", ascii[ascii.Count - 1]));
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            for (int j = 0; j < H; j++) // row
            {
                outputRow = string.Empty;
                for (int i = 0; i < T.Length; i++)
                {
                    if (_alpha.IndexOf(T.ToLower()[i]) != -1)
                    {
                        charPos = _alpha.IndexOf(T.ToLower()[i]);
                    }
                    else
                    {
                        charPos = _alpha.Length;
                    }
                    for (int k = 0; k < L; k++) // columns
                    {
                        outputRow += ascii[j][k + (L * charPos)];
                    }
                }
                if (!string.IsNullOrEmpty(outputRow)) { Console.WriteLine(outputRow); }
            }
        }
    }
}
