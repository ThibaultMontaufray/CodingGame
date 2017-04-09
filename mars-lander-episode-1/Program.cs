using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars_lander_episode_1
{
    class Program
    {
        private static int _x;
        private static int _y;
        private static int _hSpeed; // the horizontal speed (in m/s), can be negative.
        private static int _vSpeed; // the vertical speed (in m/s), can be negative.
        private static int _fuel; // the quantity of remaining fuel in liters.
        private static int _rotate; // the rotation angle in degrees (-90 to 90).
        private static int _power; // the thrust power (0 to 4).

        static void Main(string[] args)
        {
            string[] inputs;
            int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
            for (int i = 0; i < surfaceN; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
                int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            }

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                _x = int.Parse(inputs[0]);
                _y = int.Parse(inputs[1]);
                _hSpeed = int.Parse(inputs[2]);
                _vSpeed = int.Parse(inputs[3]);
                _fuel = int.Parse(inputs[4]);
                _rotate = int.Parse(inputs[5]);
                _power = int.Parse(inputs[6]);

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                SetPower();
                SetRotation();

                // 2 integers: rotate power. rotate is the desired rotation angle (should be 0 for level 1), power is the desired thrust power (0 to 4).
                Console.WriteLine(string.Format("{0} {1}", _rotate, _power));
            }
        }
        private static void SetPower()
        {
            if (_vSpeed / 2 > -(_y / 100))
            {
                _power--;
            }
            else
            {
                _power++;
            }
            if (_power < 0) { _power = 0; }
            if (_power > 4) { _power = 4; }
        }
        private static void SetRotation()
        {
            _rotate = 0;
        }
    }
}
