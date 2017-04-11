using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace don_t_panic_episode_1
{
    public struct Floor
    {
        public int Level;
        public List<int> Elevator;
        public int PositionExit;
    }
    class Program
    {
        public enum Order
        {
            WAIT,
            LEFT,
            RIGHT,
            BLOCK
        }

        public static Floor[] _floors;
        public static int _cloneFloor;
        public static int _clonePos;
        public static string _cloneDirection;
        public static Order _nextOrder;
        public static int _startPostion;
        public static int _indexElevator;

        static void Main(string[] args)
        {
            Init();
            Process();
        }
        public static void Init()
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int nbFloors = int.Parse(inputs[0]); // number of floors
            int width = int.Parse(inputs[1]); // width of the area
            int nbRounds = int.Parse(inputs[2]); // maximum number of rounds
            int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
            int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
            int nbTotalClones = int.Parse(inputs[5]); // number of generated clones
            int nbAdditionalElevators = int.Parse(inputs[6]); // ignore (always zero)
            int nbElevators = int.Parse(inputs[7]); // number of elevators

            _floors = new Floor[nbFloors];
            _floors[exitFloor].PositionExit = exitPos;
            Console.Error.WriteLine(string.Format("FLOOR count {0}", _floors.Length));

            for (int i = 0; i < nbElevators; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
                int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
                Console.Error.WriteLine(string.Format("ELEVATOR Floor {0} position {1}", elevatorFloor, elevatorPos));

                if (_floors[elevatorFloor].Elevator == null) { _floors[elevatorFloor].Elevator = new List<int>(); }
                _floors[elevatorFloor].Elevator.Add(elevatorPos);
            }
            _nextOrder = Order.RIGHT;
            _startPostion = -1;
            Console.Error.WriteLine(string.Format("EXIT Floor {0} position {1}", exitFloor, exitPos));
        }
        public static void Process()
        {
            string[] inputs;

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                _cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
                _clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
                _cloneDirection = inputs[2]; // direction of the leading clone: LEFT or RIGHT
                if (_startPostion == -1) { _startPostion = _clonePos; Console.Error.WriteLine(string.Format("Start position : {0}", _startPostion)); }

                Console.Error.WriteLine(string.Format("LEADER : Floor {0} Position {1} direction {2};{3}", _cloneFloor, _clonePos, _cloneDirection[0], _cloneDirection[1]));
                
                DetermineElevator();
                DetermineDirection();
                Console.WriteLine(_nextOrder.ToString()); // action: WAIT or BLOCK
            }
        }
        public static void DetermineDirection()
        {
            //Console.Error.WriteLine(string.Format("Elevator {0} clonePosition {1}", _floors[_cloneFloor].Elevator[_indexElevator], _clonePos));
            if (_cloneFloor == -1)
            {
                _nextOrder = Order.WAIT;
                Console.Error.WriteLine("Stack stack stack");
            }
            else if (_clonePos == _startPostion && _cloneFloor == 0)
            {
                _nextOrder = Order.WAIT;

            }
            else if (_floors[_cloneFloor].Elevator != null && _floors[_cloneFloor].Elevator.Count > 0)
            {
                if (_floors[_cloneFloor].Elevator[_indexElevator] == _clonePos)
                {
                    _nextOrder = Order.WAIT;
                }
                else if (_floors[_cloneFloor].Elevator[_indexElevator] - _clonePos > 0)
                {
                    if (_cloneDirection.Equals("RIGHT")) { _nextOrder = Order.WAIT; }
                    else { _nextOrder = Order.BLOCK; }
                }
                else
                {
                    if (_cloneDirection.Equals("LEFT")) { _nextOrder = Order.WAIT; }
                    else { _nextOrder = Order.BLOCK; }
                }
            }
            else if (_floors[_cloneFloor].PositionExit != -1)
            {
                Console.Error.WriteLine(string.Format("Position exit : {0} position clone : {1} direction : {2}", _floors[_cloneFloor].PositionExit, _clonePos, _cloneDirection));
                if (_floors[_cloneFloor].PositionExit == _clonePos)
                {
                    _nextOrder = Order.WAIT;
                }
                else if (_floors[_cloneFloor].PositionExit - _clonePos > 0)
                {
                    if (_cloneDirection.Equals("RIGHT")) { _nextOrder = Order.WAIT; }
                    else { _nextOrder = Order.BLOCK; }
                }
                else
                {
                    if (_cloneDirection.Equals("LEFT")) { _nextOrder = Order.WAIT; }
                    else { _nextOrder = Order.BLOCK; }
                }
            }
            else
            {
                _nextOrder = Order.WAIT;
                Console.Error.WriteLine("Heu roger, we got a problem !");
            }
        }
        public static void DetermineElevator()
        {
            _indexElevator = 0;
            try
            {
                if (_cloneFloor != -1 && _floors[_cloneFloor].Elevator != null)
                {
                    foreach (var item in _floors[_cloneFloor].Elevator)
                    {
                        Console.Error.WriteLine(" --> Elevator pos : " + item);
                    }
                    if (_floors[_cloneFloor].Elevator.Count > 0)
                    {
                        if (_floors[_cloneFloor].Elevator[_indexElevator] == _startPostion) { _indexElevator++; }
                    }
                    Console.Error.WriteLine("Current elevator position : " + _floors[_cloneFloor].Elevator[_indexElevator]);
                }
            }
            catch (Exception exp)
            {
                Console.Error.WriteLine("Exception : " + exp.Message);
                _indexElevator = 0;
            }
        }
    }
}
