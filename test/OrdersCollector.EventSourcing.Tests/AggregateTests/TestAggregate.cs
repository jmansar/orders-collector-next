using System.Collections.Generic;

namespace OrdersCollector.EventSourcing.Tests.AggregateTests
{
    public class TestAggregate : Aggregate
    {
        private List<IEvent> appliedEvents = new List<IEvent>();
        
        public TestAggregate()
        {
            RegisterEventApplier<TestEvent1>(When);
            RegisterEventApplier<TestEvent2>(When);
        }
        
        private void When(TestEvent1 @event)
        {
            appliedEvents.Add(@event);
        }

        private void When(TestEvent2 @event)
        {
            appliedEvents.Add(@event);
        }

        public void InvokeRaiseEvent(IEvent @event)
        {
            RaiseEvent(@event);
        }

        public IEnumerable<IEvent> AppliedEvents => appliedEvents;
    }

    public class TestEvent1 : Event
    {
    }

    public class TestEvent2 : Event
    {
    }

    public class TestUnsupportedEvent : Event
    {
    }
}