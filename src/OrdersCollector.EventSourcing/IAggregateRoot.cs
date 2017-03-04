using System;
using System.Collections.Generic;

namespace OrdersCollector.EventSourcing
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

        void LoadFromHistory(IEnumerable<IEvent> events);
        
        IEnumerable<IEvent> GetUncommitedChanges();
        
        int Version { get; }
    }
}