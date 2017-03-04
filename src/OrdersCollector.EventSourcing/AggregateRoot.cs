using System;
using System.Collections.Generic;

namespace OrdersCollector.EventSourcing
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private List<IEvent> uncommitedEvents;
        private Dictionary<Type, Action<IEvent>> eventsHandlers;

        public AggregateRoot()
        {
            uncommitedEvents = new List<IEvent>();
            eventsHandlers = new Dictionary<Type, Action<IEvent>>();
            Version = -1;
        }

        protected void RegisterEventHandler<TEvent>(Action<TEvent> eventHandler)
            where TEvent : IEvent
        {
            eventsHandlers.Add(typeof(TEvent), e => eventHandler((TEvent) e));
        }

        public Guid Id { get; protected set; }

        public int Version { get; private set; }

        public void LoadFromHistory(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyEvent(@event);
                Version++;
            }
        }

        public IEnumerable<IEvent> GetUncommitedChanges()
        {
            return uncommitedEvents.AsReadOnly();
        }

        public void MarkAllChangesAsCommited()
        {
            Version += uncommitedEvents.Count;
            uncommitedEvents.Clear();
        }

        protected void RaiseEvent(IEvent @event)
        {
            uncommitedEvents.Add(@event);
            ApplyEvent(@event);
        }

        private void ApplyEvent(IEvent @event)
        {
            if (!eventsHandlers.ContainsKey(@event.GetType()))
            {
                throw new UnsupportedEventException(@event);
            }

            var handler = eventsHandlers[@event.GetType()];
            handler.Invoke(@event);
        }
    }
}