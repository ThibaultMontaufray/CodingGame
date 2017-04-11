using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bender_episode_1
{
    class Program
    {
        private enum Direction
        {
            SOUTH = 0,
            EAST = 1,
            NORTH = 2,
            WEST = 3,
            LOOP = -1
        }

        private static List<string> _tab = new List<string>();
        private static int _coordX, _coordY, _coordXNext, _coordYNext, _coordXPrev, _coordYPrev, _coordXTarget, _coordYTarget, _coordXTeleport1, _coordYTeleport1, _coordXTeleport2, _coordYTeleport2;
        private static Direction _currentDirection;
        private static bool _breakerMode;
        private static bool _invertMode;
        private static int _nbTeleportation;
        private static Queue<Direction> _orders = new Queue<Direction>();
        private static bool _loopDetected;

        static void Main(string[] args)
        {
            Init();
            Console.Error.WriteLine(string.Format(" -> Coord target [{0},{1}]", _coordXTarget, _coordYTarget));
            Stack();

            Process();
        }
        private static void Init()
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int L = int.Parse(inputs[0]);
            int C = int.Parse(inputs[1]);

            _coordXTeleport1 = -1;
            _coordYTeleport1 = -1;
            _coordXTeleport2 = -1;
            _coordYTeleport2 = -1;

            for (int i = 0; i < L; i++)
            {
                string row = Console.ReadLine();
                _tab.Add(row);
                if (row.Contains("@"))
                {
                    _coordY = _tab.Count - 1;
                    _coordX = row.IndexOf('@');
                }
                if (row.Contains("$"))
                {
                    _coordYTarget = _tab.Count - 1;
                    _coordXTarget = row.IndexOf('$');
                }
                if (row.Contains("T"))
                {
                    if (_coordYTeleport1 == -1)
                    { 
                        _coordYTeleport1 = _tab.Count - 1;
                        _coordXTeleport1 = row.IndexOf('T');
                    }
                    else
                    {
                        _coordYTeleport2 = _tab.Count - 1;
                        _coordXTeleport2 = row.IndexOf('T');
                    }
                }
                Console.Error.WriteLine(" -> " + row);
            }

            Console.Error.WriteLine(string.Format(" -> Teleporter 1 [{0},{1}]", _coordXTeleport1, _coordYTeleport1));
            Console.Error.WriteLine(string.Format(" -> Teleporter 2 [{0},{1}]", _coordXTeleport2, _coordYTeleport2));
            Console.Error.WriteLine(string.Format(" -> Target [{0},{1}]", _coordXTarget, _coordYTarget));
            _currentDirection = Direction.SOUTH;
            _breakerMode = false;
            _invertMode = false;
            _nbTeleportation = 0;
            _loopDetected = false;
        }
        private static void Process()
        {
            Console.Error.WriteLine("Orders : " + _orders.Count);
            if (!_loopDetected)
            { 
                foreach (var item in _orders)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("LOOP");
            }
        }
        private static void Stack()
        {
            while (_tab[_coordY][_coordX] != '$')
            {
                Console.Error.WriteLine(string.Format(" -> Position [{0},{1}] val : \"{2}\"", _coordX, _coordY, _tab[_coordY][_coordX]));
                ResolveAction();
                ApplyMovement();

                if (_orders.Count > 1000 || _nbTeleportation > 2 || _currentDirection == Direction.LOOP)
                {
                    _loopDetected = true;
                    break;
                }

                _coordXPrev = _coordX;
                _coordYPrev = _coordY;
                _coordX = _coordXNext;
                _coordY = _coordYNext;
            }
            Console.Error.WriteLine(" -> EOP");
            Console.Error.WriteLine(string.Format(" -> Position [{0},{1}] val : \"{2}\"", _coordX, _coordY, _tab[_coordY][_coordX]));
        }
        private static void ResolveAction()
        {
            //Console.Error.WriteLine(string.Format(" -> cell [{0},{1}] = \"{2}\"", x, y, _tab[y][x]));
            //if (DetectObstacle()) Console.Error.WriteLine(" -> obstacle");
            switch (_tab[_coordY][_coordX])
            {
                case 'T':
                    Teleportation();
                    if (DetectObstacle()) { ChangeDirection(); }
                    break;
                case 'B':
                    _breakerMode = !_breakerMode;
                    if (DetectObstacle()) { ChangeDirection(); }
                    break;
                case 'I':
                    _invertMode = !_invertMode;
                    if (DetectObstacle()) { ChangeDirection(); }
                    break;
                case 'N':
                    _currentDirection = Direction.NORTH;
                    if (DetectObstacle()) { ChangeDirection(); }
                    break;
                case 'S':
                    _currentDirection = Direction.SOUTH;
                    if (DetectObstacle()) { ChangeDirection(); }
                    break;
                case 'E':
                    _currentDirection = Direction.EAST;
                    if (DetectObstacle()) { ChangeDirection(); }
                    break;
                case 'W':
                    _currentDirection = Direction.WEST;
                    if (DetectObstacle()) { ChangeDirection(); }
                    break;
                case '@':
                case ' ':
                    if (DetectObstacle()) { ChangeDirection(); }
                    break;
                case 'X':
                    BreakTheWall();
                    if (DetectObstacle()) { ChangeDirection(); }
                    break;
                case '#':
                    Console.Error.WriteLine(" -> Error program");
                    _currentDirection = Direction.LOOP;
                    _loopDetected = true;
                    break;
                case '$':
                default:
                    Console.Error.WriteLine(" -> Destruction reached");
                    _currentDirection = Direction.LOOP;
                    _loopDetected = true;
                    break;
            }
        }
        private static void ApplyMovement()
        {
            switch (_currentDirection)
            {
                case Direction.SOUTH:
                    _coordXNext = _coordX;
                    _coordYNext = _coordY + 1;
                    break;
                case Direction.EAST:
                    _coordXNext = _coordX + 1;
                    _coordYNext = _coordY;
                    break;
                case Direction.NORTH:
                    _coordXNext = _coordX;
                    _coordYNext = _coordY - 1;
                    break;
                case Direction.WEST:
                    _coordXNext = _coordX - 1;
                    _coordYNext = _coordY;
                    break;
                case Direction.LOOP:
                default:
                    _loopDetected = true;
                    break;
            }
            Console.Error.WriteLine("# " + _currentDirection);
            _orders.Enqueue(_currentDirection);
        }
        private static bool DetectObstacle()
        {
            switch (_currentDirection)
            {
                case Direction.SOUTH:
                    return (_tab[_coordY + 1][_coordX] == '#' || (!_breakerMode && _tab[_coordY + 1][_coordX] == 'X'));
                case Direction.EAST:
                    return (_tab[_coordY][_coordX + 1] == '#' || (!_breakerMode && _tab[_coordY][_coordX + 1] == 'X'));
                case Direction.NORTH:
                    return (_tab[_coordY - 1][_coordX] == '#' || (!_breakerMode && _tab[_coordY - 1][_coordX] == 'X'));
                case Direction.WEST:
                    return (_tab[_coordY][_coordX - 1] == '#' || (!_breakerMode && _tab[_coordY][_coordX - 1] == 'X'));
                case Direction.LOOP:
                default:
                    _loopDetected = true;
                    return false;
            }
        }
        private static void ChangeDirection()
        {
            if (_invertMode) { ChangeDirectionInvert(); }
            else {  ChangeDirectionNormal(); }
        }
        private static void ChangeDirectionNormal()
        {
            if (_tab[_coordY + 1][_coordX] != '#' && _tab[_coordY + 1][_coordX] != 'X')
            {
                _currentDirection = Direction.SOUTH;
            }
            else if (_tab[_coordY][_coordX + 1] != '#' && _tab[_coordY][_coordX + 1] != 'X')
            {
                _currentDirection = Direction.EAST;
            }
            else if (_tab[_coordY - 1][_coordX] != '#' && _tab[_coordY - 1][_coordX] != 'X')
            {
                _currentDirection = Direction.NORTH;
            }
            else if (_tab[_coordY][_coordX - 1] != '#' && _tab[_coordY][_coordX - 1] != 'X')
            {
                _currentDirection = Direction.WEST;
            }
            else
            {
                Console.Error.WriteLine(" -> Only walls");
                _currentDirection = Direction.LOOP;
                _loopDetected = true;
            }
        }
        private static void ChangeDirectionInvert()
        {
            if (_tab[_coordY][_coordX - 1] != '#' && _tab[_coordY][_coordX - 1] != 'X')
            {
                _currentDirection = Direction.WEST;
            }
            else if (_tab[_coordY - 1][_coordX] != '#' && _tab[_coordY - 1][_coordX] != 'X')
            {
                _currentDirection = Direction.NORTH;
            }
            else if (_tab[_coordY][_coordX + 1] != '#' && _tab[_coordY][_coordX + 1] != 'X')
            {
                _currentDirection = Direction.EAST;
            }
            else if(_tab[_coordY + 1][_coordX] != '#' && _tab[_coordY + 1][_coordX] != 'X')
            {
                _currentDirection = Direction.SOUTH;
            }
            else
            {
                Console.Error.WriteLine("Only walls");
                _currentDirection = Direction.LOOP;
                _loopDetected = true;
            }
        }
        private static void Teleportation()
        {
            if (_coordX == _coordXTeleport1 && _coordY == _coordYTeleport1)
            {
                _coordX = _coordXTeleport2;
                _coordY = _coordYTeleport2;
            }
            else
            {
                _coordX = _coordXTeleport1;
                _coordY = _coordYTeleport1;
            }
            Console.Error.WriteLine(string.Format(" -> Teleportation to [{0},{1}]", _coordX, _coordY));
            _nbTeleportation++;
        }
        private static void BreakTheWall()
        {
            int counter;
            string row;
            string newRow = string.Empty;
            if (_breakerMode)
            {
                row = _tab[_coordY];
                counter = 0;
                foreach (char c in row.ToCharArray())
                {
                    if (counter != _coordX) { newRow += c; }
                    else { newRow += ' '; }
                    counter++;
                }
                _tab[_coordY] = newRow;
            }
        }
    }
}
