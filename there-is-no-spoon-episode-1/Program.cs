using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace there_is_no_spoon_episode_1
{
    class Program
    {
        private static List<string> _tab = new List<string>();
        private static char currentCell, rightCell, bottomCell;
        private static int _leftX, _leftY, _bottomX, _bottomY;
        private static int _width;
        private static int _height;

        static void Main(string[] args)
        {
            _width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
            _height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
            for (int i = 0; i < _height; i++)
            {
                string line = Console.ReadLine(); // width characters, each either 0 or .
                _tab.Add(line);
                Console.Error.WriteLine(line);
            }

            for (int rowIndex = 0; rowIndex < _height; rowIndex++)
            {
                for (int cell = 0; cell < _width; cell++)
                {
                    if (_tab[rowIndex][cell] != '.')
                    { 
                        GetRightNextNode(cell, rowIndex);
                        GetBottomNextNode(cell, rowIndex);
                        Console.WriteLine(string.Format("{0} {1} {2} {3} {4} {5}", cell, rowIndex, _leftX, _leftY, _bottomX, _bottomY));
                    }
                }
            }
        }
        private static void GetRightNextNode(int x, int y)
        {
            _leftX = _leftY = -1;
            for (int i = x+1; i < _width; i++)
            {
                if (_tab[y][i] != '.')
                {
                    _leftX = i;
                    _leftY = y;
                    break;
                }
            }
        }
        private static void GetBottomNextNode(int x, int y)
        {
            _bottomX = _bottomY = -1;
            int[] retCoord = new int[2] { -1, -1 };
            for (int i = y+1; i < _height; i++)
            {
                if (_tab[i][x] != '.')
                {
                    _bottomX = x;
                    _bottomY = i;
                    break;
                }
            }
        }
    }
}
