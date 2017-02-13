using System;

namespace OrdersCollector.EventSourcing
{
    public interface IEvent
    {
         DateTimeOffset Timestamp { get; }
    }
}