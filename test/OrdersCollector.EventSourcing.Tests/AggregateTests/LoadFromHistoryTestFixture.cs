using System.Linq;
using NUnit.Framework;

namespace OrdersCollector.EventSourcing.Tests.AggregateTests
{
    [TestFixture]
    public class LoadFromHistoryTestFixture
    {
        [Test]
        public void LoadFromHistory_WhenInvoked_MustApplyEvents()
        {
            // Arrange
            var testEvent1 = new TestEvent1();
            var testEvent2 = new TestEvent2();
            var expected = new IEvent[]{ testEvent1, testEvent2 };
            var subject = new TestAggregate();

            // Act
            subject.LoadFromHistory(expected);

            // Assert
            Assert.That(subject.AppliedEvents, Is.EqualTo(expected));
        }

        [Test]
        public void LoadFromHistory_WhenInvoked_MustNotAddEventsToUnpublishedEventsCollection()
        {
            // Arrange
            var testEvent1 = new TestEvent1();
            var testEvent2 = new TestEvent2();
            var events = new IEvent[]{ testEvent1, testEvent2 };
            var subject = new TestAggregate();

            // Act
            subject.LoadFromHistory(events);

            // Assert
            Assert.That(subject.GetUncommitedChanges(), Is.Empty);
        }

        [TestCase(3)]
        [TestCase(4)]
        public void LoadFromHistory_WhenInvoked_MustSetVersionToIndexOfLastAppliedEvent(int numberOfEvents)
        {
            // Arrange
            var events = Enumerable.Range(0, numberOfEvents)
                .Select(x => new TestEvent1())
                .ToArray();
            var subject = new TestAggregate();

            // Act
            subject.LoadFromHistory(events);

            // Assert
            Assert.That(subject.Version, Is.EqualTo(numberOfEvents - 1));
        }

        [Test]
        public void LoadFromHistory_WhenEventNotSupportedByAggregatePassed_MustThrowException()
        {
            // Arrange
            var unsupportedEvent = new TestUnsupportedEvent();
            var subject = new TestAggregate();

            // Act
            TestDelegate action = () => subject.LoadFromHistory(new IEvent[] { unsupportedEvent });

            // Assert
            Assert.That(action, Throws.Exception.TypeOf<UnsupportedEventException>());
        }
    }
}