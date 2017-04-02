using System;
using System.Collections.Generic;
using OrdersCollector.Core.Orders.Events;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Orders.Models
{
    public class Order : AggregateRoot
    {
        private List<OrderItem> items = new List<OrderItem>();

        public Order(
            Guid orderId,
            Guid supplierId,
            Guid groupId
        ) : this()
        {
            RaiseEvent(new OrderCreated(
                orderId,
                supplierId,
                groupId
            ));
        }

        private Order()
        {
            RegisterEventHandler<OrderCreated>(When);
            RegisterEventHandler<OrderItemAdded>(When);
        }

        public DateTimeOffset? ExpiryDate { get; private set; }

        public OrderStatus Status { get; private set; }

        public Guid GroupId { get; private set; }

        public Guid SupplierId { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => items; 

        public void AddOrderItem(string content, Guid userId)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            RaiseEvent(new OrderItemAdded(
                Id,
                content,
                userId
            ));
        }

        private void When(OrderItemAdded @event)
        {
            items.Add(new OrderItem(
                @event.Content,
                @event.UserId
            ));
        }

        private void When(OrderCreated @event)
        {
            Id = @event.OrderId;
            SupplierId = @event.SupplierId;
            GroupId = @event.GroupId;
        }
    }
}
