using System;

namespace S.Utility.U.EventBus
{
    public interface IEventBus
    {
        void Subscribe<T>(Action<T> callback);
        void Unsubscribe<T>(Action<T> callback);
        void Publish<T>(T eventData);
    }
}
