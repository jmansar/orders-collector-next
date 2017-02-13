using NUnit.Framework;

namespace OrdersCollector.EventSourcing.Tests.AggregateTests
{
    [TestFixture]
    public class MarkAllChangesAsCommitedTestFixture
    {
        [Test]
        public void MarkAllChangesAsCommited_WhenInvoked_MustClearUncommitedCollection()
        {
            // Arrange
            var subject = new TestAggregate();
            subject.InvokeRaiseEvent(new TestEvent1());

            // Act
            subject.MarkAllChangesAsCommited();

            // Assert
            Assert.That(subject.GetUncommitedChanges(), Is.Empty);
        }

        [Test]
        public void MarkAllChangesAsCommited_WhenInvoked_MustIncrementVersionForEveryCommitedEvent()
        {
            // Arrange
            var subject = new TestAggregate();
            subject.LoadFromHistory(new IEvent[]{ new TestEvent1(), new TestEvent2() });
            subject.InvokeRaiseEvent(new TestEvent1());
            subject.InvokeRaiseEvent(new TestEvent1());

            // Act
            subject.MarkAllChangesAsCommited();

            // Assert
            Assert.That(subject.Version, Is.EqualTo(3));
        }
    }
}