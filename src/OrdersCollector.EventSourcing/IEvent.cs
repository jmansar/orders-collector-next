using System;

namespace OrdersCollector.EventSourcing
{
    public interface IEvent
    {
        Guid EventId { get; }

        string EventType { get; }
        
        DateTimeOffset Timestamp { get; }
    }
}