using System;
using System.Linq;
using NUnit.Framework;
using OrdersCollector.Core.Orders.Events;
using OrdersCollector.Core.Orders.Models;

namespace OrdersCollector.Core.Tests.Orders.Models.OrderAggregateTests
{
    public class ConstructorTestFixture
    {
        private Guid supplierId;
        private Guid groupId;

        [SetUp]
        public void SetUp()
        {
            supplierId = Guid.NewGuid();
            groupId = Guid.NewGuid();
        }

        [Test]
        public void Constructor_WhenInvoked_MustCreateOrderAggregateWithGeneratedId()
        {
            // Arrange, Act
            var subject = new OrderAggregate(supplierId, groupId);

            // Assert
            Assert.That(subject.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void Constructor_WhenInvoked_MustCreateOrderAggregateWithSupplierId()
        {
            // Arrange, Act
            var subject = new OrderAggregate(supplierId, groupId);

            // Assert
            Assert.That(subject.SupplierId, Is.EqualTo(supplierId));
        }

        [Test]
        public void Constructor_WhenInvoked_MustCreateOrderAggregateWithGroupId()
        {
            // Arrange, Act
            var subject = new OrderAggregate(supplierId, groupId);

            // Assert
            Assert.That(subject.GroupId, Is.EqualTo(groupId));
        }
 
        [Test]
        public void Constructor_WhenInvoked_MustCreateOrderAggregateWithStatusSetToDraft()
        {
            // Arrange, Act
            var subject = new OrderAggregate(supplierId, groupId);

            // Assert
            Assert.That(subject.Status, Is.EqualTo(OrderStatus.Draft));
        }
         
        [Test]
        public void Constructor_WhenInvoked_MustAddCreatedEventToUncommitedEvents()
        {
            // Arrange, Act
            var subject = new OrderAggregate(supplierId, groupId);

            // Assert
            var @event = subject.GetUncommitedChanges().Single();
            Assert.That(@event, Is.TypeOf<OrderCreated>());
        }
  
        [Test]
        public void Constructor_WhenInvoked_MustAddCreatedEventWithSupplierId()
        {
            // Arrange, Act
            var subject = new OrderAggregate(supplierId, groupId);

            // Assert
            var @event = subject.GetUncommitedChanges().Single() as OrderCreated;
            Assert.That(@event.SupplierId, Is.EqualTo(supplierId));
        }

        [Test]
        public void Constructor_WhenInvoked_MustAddCreatedEventWithGroupId()
        {
            // Arrange, Act
            var subject = new OrderAggregate(supplierId, groupId);

            // Assert
            var @event = subject.GetUncommitedChanges().Single() as OrderCreated;
            Assert.That(@event.GroupId, Is.EqualTo(groupId));
        }

        [Test]
        public void Constructor_WhenInvoked_MustAddCreatedEventWithOrderId()
        {
            // Arrange, Act
            var subject = new OrderAggregate(supplierId, groupId);

            // Assert
            var @event = subject.GetUncommitedChanges().Single() as OrderCreated;
            Assert.That(@event.OrderId, Is.EqualTo(subject.Id));
        }
    }
}