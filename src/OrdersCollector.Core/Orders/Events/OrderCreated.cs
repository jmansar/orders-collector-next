using System;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Orders.Events
{
    public class OrderCreated : Event
    {
        public OrderCreated(Guid orderId, Guid supplierId, Guid groupId)
        {
            this.OrderId = orderId;
            this.SupplierId = supplierId;
            this.GroupId = groupId;
        }

        public Guid OrderId { get; }

        public Guid SupplierId { get; }

        public Guid GroupId { get; }
    }
}