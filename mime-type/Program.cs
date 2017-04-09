using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mime_type
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo fi;
            Dictionary<string, string> mime = new Dictionary<string, string>();

            int N = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
            int Q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.
            for (int i = 0; i < N; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                string EXT = inputs[0]; // file extension
                string MT = inputs[1]; // MIME type.
                mime["." + EXT.ToLower()] = MT;
            }
            for (int i = 0; i < Q; i++)
            {
                string FNAME = Console.ReadLine(); // One file name per line.

                fi = new FileInfo(FNAME);
                if (mime.ContainsKey(fi.Extension.ToLower()))
                {
                    Console.WriteLine(mime[fi.Extension.ToLower()]);
                }
                else
                {
                    Console.WriteLine("UNKNOWN");
                }
            }
        }
    }
}
