namespace Heiskontroll
{
    public class Lift
    {
        private int _currentFloor;
        private Status _liftStatus;
        private Direction _currentDirection;
        private Door _doorStatus;
        public Lift(int currentFloor)
        {
            _currentFloor = currentFloor;
            _liftStatus = Status.Idle;
            _currentDirection = Direction.Up;
            _doorStatus = Door.Closed;
        }

        private enum Status
        {
            Moving,
            Stopped,
            Idle
        }

        private enum Direction
        {
            Up,
            Down
        }

        private enum Door
        {
            Open,
            Closed
        }

        private void GoToFloor(int nextFloor)
        {
            Console.WriteLine($"Lift requested at floor: {nextFloor}");
            Console.WriteLine($"Leaving floor: {_currentFloor}");

            int estimatedArrival = EstimateTimeToArrival(nextFloor);
            Console.WriteLine($"Lift arrives in: {estimatedArrival}s");

            _liftStatus = Status.Moving;
            Console.WriteLine($"Lift status: {_liftStatus}");

            Console.WriteLine($"Going: {_currentDirection}");
            Thread.Sleep(estimatedArrival * 1000);

            _currentFloor = nextFloor;
            Console.WriteLine($"Arrived at {_currentFloor}");
            _liftStatus = Status.Stopped;
            Console.WriteLine($"Lift status: {_liftStatus}");

            OpenDoors();
            Console.WriteLine($"Doors: {_doorStatus}");
            Thread.Sleep(2000);
            CloseDoors();
            Console.WriteLine($"Doors: {_doorStatus}");
            Thread.Sleep(2000);
        }

        public void ProcessQueue(List<int> requestedFloors)
        {
            if (requestedFloors[0] == _currentFloor)
            {
                return;
            }

            if (requestedFloors[0] > _currentFloor)
            {
                var upQueue = requestedFloors.Where(x => x >= requestedFloors[0]).OrderBy(x => x).Distinct();

                _currentDirection = Direction.Up;

                ProcessRequestsGoingUp(upQueue);

                var downQueue = requestedFloors.Where(x => x < requestedFloors[0]).OrderByDescending(x => x).Distinct();

                _currentDirection = Direction.Down;

                ProcessRequestsGoingDown(downQueue);

                _liftStatus = Status.Idle;
                Console.WriteLine($"No more requests. Lift status: {_liftStatus}");
            }

            else
            {
                var downQueue = requestedFloors.Where(x => x < requestedFloors[0]).OrderByDescending(x => x).Distinct();

                _currentDirection = Direction.Down;

                ProcessRequestsGoingDown(downQueue);

                var upQueue = requestedFloors.Where(x => x >= requestedFloors[0]).OrderBy(x => x).Distinct();

                _currentDirection = Direction.Up;

                ProcessRequestsGoingUp(upQueue);

                _liftStatus = Status.Idle;
                Console.WriteLine($"No more requests. Lift status: {_liftStatus}");
            }
        }

        private void ProcessRequestsGoingUp(IEnumerable<int> upQueue)
        {
            for (int i = _currentFloor; i <= upQueue.Max(); i++)
            {
                if (upQueue.Contains(i))
                {
                    GoToFloor(i);
                }
            }
        }

        private void ProcessRequestsGoingDown(IEnumerable<int> downQueue)
        {
            for (int i = _currentFloor; i >= downQueue.Min(); i--)
            {
                if (downQueue.Contains(i))
                {
                    GoToFloor(i);

                }
            }
        }

        private int EstimateTimeToArrival(int floor)
        {
            return Math.Abs(_currentFloor - floor);
        }

        public void Stop()
        {
            _liftStatus = Status.Stopped;
            Console.WriteLine($"Emergency stop triggered. Lift status: {_liftStatus}");
        }

        private void OpenDoors()
        {
            _doorStatus = Door.Open;
        }

        private void CloseDoors()
        {
            _doorStatus = Door.Closed;
        }
    }
}
