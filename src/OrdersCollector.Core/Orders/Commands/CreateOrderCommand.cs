using System;
using MediatR;

namespace OrdersCollector.Core.Orders.Commands
{
    public class CreateOrderCommand : IRequest
    {

        public CreateOrderCommand(Guid orderId, Guid groupId, Guid supplierId, Guid userId)
        {
            this.OrderId = orderId;
            this.GroupId = groupId;
            this.SupplierId = supplierId;
            this.UserId = userId;
        }

        public Guid OrderId { get; }

        public Guid GroupId { get; }

        public Guid SupplierId { get; }

        public Guid UserId { get; }
    }
}