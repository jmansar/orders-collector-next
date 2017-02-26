using System;
using System.Collections.Generic;
using OrdersCollector.Core.Orders.Events;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Orders.Models
{
    public class OrderAggregate : Aggregate
    {
        public OrderAggregate(
            Guid supplierId,
            Guid groupId
        ) : this()
        {
            RaiseEvent(new OrderCreated(
                Guid.NewGuid(),
                supplierId,
                groupId
            ));
        }

        private OrderAggregate()
        {
            RegisterEventHandler<OrderCreated>(When);
        }

        public Guid Id { get; private set; }

        public DateTimeOffset? ExpiryDate { get; private set; }

        public OrderStatus Status { get; private set; }

        public Guid GroupId { get; private set; }

        public Guid SupplierId { get; private set; }

        public IEnumerable<OrderItem> Items { get; }

        private void When(OrderCreated @event)
        {
            Id = @event.OrderId;
            SupplierId = @event.SupplierId;
            GroupId = @event.GroupId;
        }
    }
}
