namespace Events
{
    public class EventService
    {
        private static EventService instance;
        public static EventService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventService();
                }
                return instance;
            }
        }

        public EventController OnPackagePickedUp { get; private set; }
        public EventController OnPackageDeliveredEvent { get; private set; }

        public EventService()
        {
            OnPackagePickedUp = new EventController();
            OnPackageDeliveredEvent = new EventController();
        }
    }
}