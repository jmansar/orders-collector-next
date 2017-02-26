using System;
using NUnit.Framework;
using OrdersCollector.Utils.Time;

namespace OrdersCollector.EventSourcing.Tests.EventTests
{
    public class ConstructorTestFixture
    {
        private DateTimeOffset utcNow;

        [SetUp]
        public void SetUp()
        {
            utcNow = DateTimeOffset.UtcNow;
            Clock.UtcDateTimeProvider = () => utcNow;
        }

        [TearDown]
        public void TearDown()
        {
            Clock.ResetToDefault();
        }

        [Test]
        public void Constructor_WhenInvoked_MustSetTimestampToUtcNot()
        {
            // Arrange, Act
            var subject = new TestEvent();

            // Assert
            Assert.That(subject.Timestamp, Is.EqualTo(utcNow));
        }

        private class TestEvent : Event
        {
        }
    }
}