using System;

namespace OrdersCollector.EventSourcing
{
    public class Event : IEvent 
    {
        public Event()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset Timestamp { get; }
    }
}