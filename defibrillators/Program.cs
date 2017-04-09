using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace defibrillators
{
    class Program
    {
        struct Defibrilator
        {
            public int Id;
            public string Name;
            public string Address;
            public string Phone;
            public double Long;
            public double Lat;
        }
        static void Main(string[] args)
        {
            string[] dump;
            string line;
            double X, Y, D;
            double minDist = double.MaxValue;
            double LON = double.Parse(Console.ReadLine().Replace(",", "."));
            double LAT = double.Parse(Console.ReadLine().Replace(",", "."));
            Defibrilator tmpDefib, closestDefib = new Defibrilator();
            List<Defibrilator> lstDefib = new List<Defibrilator>();

            //Console.Error.WriteLine(string.Format("\r\n[Position : Lat : {0}, Lon : {1}]", LAT, LON));
            //Console.Error.WriteLine(string.Format("\r\n"));

            int N = int.Parse(Console.ReadLine());
            for (int i = 0; i < N; i++)
            {
                line = Console.ReadLine();
                //Console.Error.WriteLine("DUMP " + line);
                dump = line.Split(';');
                if (dump.Length >5)
                {
                    tmpDefib = new Defibrilator()
                    {
                        Id = int.Parse(dump[0]),
                        Name = dump[1],
                        Address = dump[2],
                        Phone = dump[3],
                        Long = double.Parse(dump[4].Replace(",", ".")),
                        Lat = double.Parse(dump[5].Replace(",", "."))
                    };
                    lstDefib.Add(tmpDefib);
                    //Console.Error.WriteLine(string.Format("[Defibrilator : {0}, Lat : {1}, Lon : {2}]" + tmpDefib.Name, tmpDefib.Lat, tmpDefib.Long));
                }
            }

            foreach (Defibrilator defib in lstDefib)
            {
                X = (defib.Long - LON) * Math.Cos((LAT + defib.Lat) / 2);
                Y = defib.Lat - LAT;
                D = Math.Sqrt((X * X) + (Y * Y)) * 6371;
                Console.Error.WriteLine("Current distance : " + D);
                Console.Error.WriteLine(string.Format("[Defibrilator : {0}, address : {1}, Lat : {2}, Lon : {3}]", defib.Name, defib.Address, defib.Lat, defib.Long));
                if (D < minDist)
                {
                    minDist = D;
                    closestDefib = defib;
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            if (closestDefib.Name != null) { Console.WriteLine(closestDefib.Name); }
            else { Console.WriteLine(string.Empty); }
        }
    }
}
