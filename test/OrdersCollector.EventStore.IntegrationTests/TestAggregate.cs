using System;
using System.Collections.Generic;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.EventStore.IntegrationTests
{
    public class TestAggregate : AggregateRoot
    {
        private List<IEvent> appliedEvents = new List<IEvent>();
        
        public TestAggregate()
        {
            Id = Guid.NewGuid();
            RegisterEventHandler<TestEvent>(When);
        }
        
        private void When(TestEvent @event)
        {
            appliedEvents.Add(@event);
        }

        public void InvokeRaiseEvent(IEvent @event)
        {
            RaiseEvent(@event);
        }

        public IEnumerable<IEvent> AppliedEvents => appliedEvents;
    }

    public class TestEvent : Event
    {
        public Guid Property { get; set; }
    }
}