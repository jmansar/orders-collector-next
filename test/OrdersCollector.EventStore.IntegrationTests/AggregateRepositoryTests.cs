using System;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using NUnit.Framework;
using OrdersCollector.Utils.Serialization;

namespace OrdersCollector.EventStore.IntegrationTests
{
    [TestFixture]
    public class AggregateRepositoryTests
    {
        private IEventStoreConnection connection;
        private IJsonSerializer serializer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // TODO: get connectionString from config
            var connectionString = "ConnectTo = tcp://admin:changeit@localhost:1113";
            connection = EventStoreConnection.Create(connectionString);
            connection.ConnectAsync().Wait();

            serializer = new TestJsonSerializer();
        }

        [Test]
        public async Task CanStoreAndRetrieveAggregate()
        {
            // Arrange
            var aggregate = new TestAggregate();
            var @event = CreateEvent();
            aggregate.InvokeRaiseEvent(@event);
            var subject = CreateSubject();

            // Act
            await subject.Save(aggregate);
            var result = await subject.Get(aggregate.Id);

            // Assert
            var restoredEvent = result.AppliedEvents.Single() as TestEvent;
            Assert.That(restoredEvent.Property, Is.EqualTo(@event.Property));
        }

        [Test]
        public async Task CanStoreAndRetrieveAggregateWithNumberOfEventsGreaterThanSliceSize()
        {
            // Arrange
            var aggregate = new TestAggregate();
            var eventsCount = Math.Ceiling(AggregateRepository<TestAggregate>.EventsSliceSize*1.5);
            for (var i = 0; i < eventsCount; i++)
            {
                aggregate.InvokeRaiseEvent(CreateEvent());
            }

            var subject = CreateSubject();

            // Act
            await subject.Save(aggregate);
            var result = await subject.Get(aggregate.Id);

            // Assert
            Assert.That(aggregate.AppliedEvents.Count(), Is.EqualTo(eventsCount));
        }

        private TestEvent CreateEvent()
        {
            return new TestEvent()
            {
                Property = Guid.NewGuid()
            };
        }

        private AggregateRepository<TestAggregate> CreateSubject()
        {
            return new AggregateRepository<TestAggregate>(
                connection,
                serializer
            );
        }
    }
}