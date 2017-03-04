using System;
using System.Threading.Tasks;
using MediatR;
using OrdersCollector.Core.Orders.Commands;
using OrdersCollector.Core.Orders.Models;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Orders.CommandHandlers
{
    public class CreateOrderCommandHandler : IAsyncRequestHandler<CreateOrderCommand>
    {
        private readonly IAggregateRepository<Order> repository;

        public CreateOrderCommandHandler(
            IAggregateRepository<Order> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(CreateOrderCommand message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            // TODO: validate that user has permission to create order
            // in the specified group
            var order = new Order(message.SupplierId, message.GroupId);
            await repository.Save(order);
        }
    }
}