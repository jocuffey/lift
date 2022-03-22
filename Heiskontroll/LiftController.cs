namespace Heiskontroll
{
    public class LiftController
    {
        private readonly List<int> _requestQueue;
        private readonly Lift _lift;

        public LiftController(Lift lift)
        {
            _lift = lift;
            _requestQueue = new List<int>();
        }

        public void EmergencyStop()
        {
            _lift.Stop();
        }

        public void ButtonPressInternal(int requestedFloor)
        {
            _requestQueue.Add(requestedFloor);
        }

        public void ButtonPressExternal(int originFloor, int requestedFloor)
        {
            _requestQueue.Add(originFloor);
            _requestQueue.Add(requestedFloor);
        }

        public void ProcessRequestsInQueue()
        {
            _lift.ProcessQueue(_requestQueue);
        }
    }
}
