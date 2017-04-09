using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace power_of_thor_episode_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            double lightX = int.Parse(inputs[0]); // the X position of the light of power
            double lightY = int.Parse(inputs[1]); // the Y position of the light of power
            double posTX = int.Parse(inputs[2]); // Thor's starting X position
            double posTY = int.Parse(inputs[3]); // Thor's starting Y position

            double difX, difY;
            bool n, s, e, w = false;
            string answer;

            // game loop
            while (true)
            {
                int remainingTurns = int.Parse(Console.ReadLine()); // The remaining amount of turns Thor can move. Do not remove this line.
                
                n = s = e = w = false;
                difX = lightX - posTX;
                difY = lightY - posTY;

                if (difX > 0) { e = true; }
                else if (difX < 0) { w = true; }
                if (difY > 0) { s =  true; }
                else if (difY < 0) { n = true; }

                if (n & e) { answer = "NE"; posTX++; posTY--; }
                else if (n & w) { answer = "NW"; posTX--; posTY--; }
                else if (s & e) { answer = "SE"; posTX++; posTY++; }
                else if (s & w) { answer = "SW"; posTX--; posTY++; }
                else if (n) { answer = "N"; posTY--; }
                else if (s) { answer = "S"; posTY++; }
                else if (e) { answer = "E"; posTX++; }
                else if (w) { answer = "W"; posTX--; }
                else { answer = string.Empty; }

                Console.Error.WriteLine(string.Format("Thor position X:{0}, Y:{1}", posTX, posTY));
                Console.WriteLine(answer);
            }
        }
    }
}
