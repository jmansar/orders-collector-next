using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using OrdersCollector.EventSourcing;
using OrdersCollector.Utils.Serialization;

namespace OrdersCollector.EventStore
{
    public class AggregateRepository<TAggregateRoot> : IAggregateRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        public const int EventsSliceSize = 10;

        private readonly IEventStoreConnection connection;
        private readonly IJsonSerializer serializer;

        public AggregateRepository(IEventStoreConnection connection, IJsonSerializer serializer)
        {
            this.connection = connection;
            this.serializer = serializer;
        }

        public async Task<TAggregateRoot> Get(Guid aggregateRootId)
        {
            var aggregateRoot = (TAggregateRoot)Activator.CreateInstance(typeof(TAggregateRoot));

            long nextSliceStart = StreamPosition.Start;
            StreamEventsSlice currentSlice;

            do
            {
                currentSlice = await connection.ReadStreamEventsForwardAsync(
                    aggregateRootId.ToString(),
                    nextSliceStart,
                    EventsSliceSize,
                    false);

                var @events = currentSlice.Events
                    .Select(e => CreateDomainEvent(e.Event))
                    .ToList();

                aggregateRoot.LoadFromHistory(@events);

                nextSliceStart = currentSlice.NextEventNumber;
            }
            while (!currentSlice.IsEndOfStream);

            return aggregateRoot;
        }

        public async Task Save(TAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
            {
                throw new ArgumentNullException(nameof(aggregateRoot));
            }

            var uncommitedChanges = aggregateRoot.GetUncommitedChanges();
            if (!uncommitedChanges.Any())
            {
                return;
            }

            var eventsData = aggregateRoot.GetUncommitedChanges().Select(CreateEventData);

            var result = await connection.AppendToStreamAsync(
                aggregateRoot.Id.ToString(),
                aggregateRoot.Version,
                eventsData);
            
            // TODO: Catch WrongExpectedVersionException
        }

        private EventData CreateEventData(IEvent @event)
        {
            var json = serializer.Serialize(@event);
            return new EventData(
                @event.EventId,
                @event.EventType,
                true,
                Encoding.UTF8.GetBytes(json),
                null);
        }

        private IEvent CreateDomainEvent(RecordedEvent recordedEvent)
        {
            var json = Encoding.UTF8.GetString(recordedEvent.Data);
            return serializer.Deserialize<IEvent>(json);
        }
    }
}
