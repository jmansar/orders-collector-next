using System;
using OrdersCollector.Utils.Time;

namespace OrdersCollector.EventSourcing
{
    public abstract class Event : IEvent 
    {
        public static string GetEventType(Type type)
        {
            return type.Name;
        }

        public Event()
        {
            Timestamp = Clock.UtcNow;
            EventId = Guid.NewGuid();
        }

        public string EventType => Event.GetEventType(this.GetType());

        public DateTimeOffset Timestamp { get; }

        public Guid EventId { get; }
    }
}