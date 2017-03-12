using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using OrdersCollector.Core.Orders.CommandHandlers;
using OrdersCollector.Core.Orders.Models;
using OrdersCollector.Core.Tests.Builders;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Tests.Orders.CommandHandlers
{
    [TestFixture]
    public class HandleTestFixture
    {
        private IAggregateRepository<Order> repository;

        [SetUp]
        public void SetUp()
        {
            repository = Substitute.For<IAggregateRepository<Order>>();
        }

        [Test]
        public void Handle_WhenMessageIsNull_MustThrowException()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            AsyncTestDelegate action = async () => await subject.Handle(null);

            // Assert
            Assert.That(action, Throws.ArgumentNullException
                .With.Property("ParamName").EqualTo("message"));
        }

        [Test]
        public async Task Handle_WhenInvoked_MustSaveNewOrder()
        {
            // Arrange
            var command = new CreateOrderCommandBuilder().Build();
            var subject = CreateSubject();

            // Act
            await subject.Handle(command);

            // Assert
            await repository.Received().Save(
                Arg.Is<Order>(
                    o => o.Id == command.OrderId &&
                         o.GroupId == command.GroupId &&
                         o.SupplierId == command.SupplierId));
        }

        private CreateOrderCommandHandler CreateSubject()
        {
            return new CreateOrderCommandHandler(repository);
        }
    }
}