using System;
using OrdersCollector.Utils.Time;

namespace OrdersCollector.EventSourcing
{
    public abstract class Event : IEvent 
    {
        public Event()
        {
            Timestamp = Clock.UtcNow;
        }

        public DateTimeOffset Timestamp { get; }
    }
}