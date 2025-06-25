using S.Utility.U.EventBus;

namespace Tests
{
    public static class EventBusContainer
    {
        public static IEventBus Global = new EventBus();
    }
}
