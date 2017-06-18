using System;
using System.Linq;
using NUnit.Framework;
using OrdersCollector.Core.Orders.Events;
using OrdersCollector.Core.Orders.Models;
using OrdersCollector.Core.Tests.Builders;

namespace OrdersCollector.Core.Tests.Orders.Models.OrderTests
{
    [TestFixture]
    public class AddOrderItemTestFixture
    {
        private Guid userId;

        [SetUp]
        public void SetUp()
        {
            userId = Guid.NewGuid();
        }

        [Test]
        public void AddOrderItem_WhenContentIsNull_MustThrowException()
        {
            // Arrange
            var subject = new OrderBuilder().Build();

            // Act
            TestDelegate action = () => subject.AddOrderItem(null, userId);

            // Assert
            Assert.That(action, Throws.ArgumentNullException
                .With.Property("ParamName").EqualTo("content"));
        }

        [Test]
        public void AddOrderItem_WhenInvoked_MustAddOrderItemWithContent()
        {
            // Arrange
            var content = "my order line";
            var subject = new OrderBuilder().Build();

            // Act
            subject.AddOrderItem(content, userId);

            // Assert
            var orderItem = subject.Items.Single();
            Assert.That(orderItem.Content, Is.EqualTo(content));
        }

        [Test]
        public void AddOrderItem_WhenInvoked_MustAddOrderItemWithUserId()
        {
            // Arrange
            var subject = new OrderBuilder().Build();

            // Act
            subject.AddOrderItem("x", userId);

            // Assert
            var orderItem = subject.Items.Single();
            Assert.That(orderItem.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void AddOrderItem_WhenInvoked_MustAddOrderItemAddedEventWithContent()
        {
            // Arrange
            var content = "my order line2";
            var subject = new OrderBuilder().Build();

            // Act
            subject.AddOrderItem(content, userId);

            // Assert
            var @event = subject.GetUncommitedChanges().Last() as OrderItemAdded;
            Assert.That(@event.Content, Is.EqualTo(content));
        }

        [Test]
        public void AddOrderItem_WhenInvoked_MustAddOrderItemAddedEventWithUserId()
        {
            // Arrange
            var subject = new OrderBuilder().Build();

            // Act
            subject.AddOrderItem("x", userId);

            // Assert
            var @event = subject.GetUncommitedChanges().Last() as OrderItemAdded;
            Assert.That(@event.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void AddOrderItem_WhenInvoked_MustAddOrderItemAddedEventWithOrderId()
        {
            // Arrange
            var subject = new OrderBuilder().Build();

            // Act
            subject.AddOrderItem("x", userId);

            // Assert
            var @event = subject.GetUncommitedChanges().Last() as OrderItemAdded;
            Assert.That(@event.OrderId, Is.EqualTo(subject.Id));
        }
    }
}