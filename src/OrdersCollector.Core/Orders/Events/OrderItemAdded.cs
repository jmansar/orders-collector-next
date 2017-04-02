using System;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Orders.Events
{
    public class OrderItemAdded : Event
    {
        public OrderItemAdded(Guid orderId, string content, Guid userId)
        {
            OrderId = orderId;
            Content = content;
            UserId = userId;
        }

        public Guid OrderId { get; }

        public string Content { get; }

        public Guid UserId { get; }
    }
}