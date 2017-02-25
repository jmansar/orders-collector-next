using NUnit.Framework;

namespace OrdersCollector.EventSourcing.Tests.AggregateTests
{
    [TestFixture]
    public class RaiseEventTestFixture
    {
        [Test]
        public void RaiseEvent_WhenInvoked_MustRunEventHandler()
        {
            // Arrange
            var @event = new TestEvent1();
            var subject = new TestAggregate();

            // Act
            subject.InvokeRaiseEvent(@event);

            // Assert
            Assert.That(subject.AppliedEvents, Is.EqualTo(new []{ @event }));
        }

        [Test]
        public void RaiseEvent_WhenInvoked_MustAddEventToUncommitedChangesCollection()
        {
            // Arrange
            var @event = new TestEvent1();
            var subject = new TestAggregate();

            // Act
            subject.InvokeRaiseEvent(@event);

            // Assert
            Assert.That(subject.GetUncommitedChanges(), Is.EqualTo(new []{ @event }));
        }

        [Test]
        public void RaiseEvent_WhenInvoked_MustNotIncrementVersion()
        {
            // Arrange
            var @event = new TestEvent1();
            var subject = new TestAggregate();

            // Act
            subject.InvokeRaiseEvent(@event);

            // Assert
            Assert.That(subject.Version, Is.EqualTo(-1));
        }

        [Test]
        public void RaiseEvent_WhenEventNotSupportedByAggregatePassed_MustThrowException()
        {
            // Arrange
            var unsupportedEvent = new TestUnsupportedEvent();
            var subject = new TestAggregate();

            // Act
            TestDelegate action = () => subject.InvokeRaiseEvent(unsupportedEvent);

            // Assert
            Assert.That(action, Throws.Exception.TypeOf<UnsupportedEventException>());
        }
    }
}