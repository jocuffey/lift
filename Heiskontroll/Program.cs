namespace Heiskontroll
{
    public class Program
    {
        public static void Main()
        {
            Lift lift = new Lift(0); 
            LiftController controller = new LiftController(lift);

            controller.ButtonPressInternal(10);
            controller.ButtonPressInternal(16);
            controller.ButtonPressExternal(0,3);
            controller.ButtonPressInternal(0); 
            controller.ButtonPressInternal(7);
            controller.ButtonPressExternal(12, 1);
            controller.ButtonPressInternal(5);

            controller.ProcessRequestsInQueue();

            controller.EmergencyStop();

        }
    }
}
