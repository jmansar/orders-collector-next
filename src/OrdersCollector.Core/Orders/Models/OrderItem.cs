using System;

namespace OrdersCollector.Core.Orders.Models
{
    public class OrderItem
    {
        public OrderItem(string content, Guid userId)
        {
            Content = content;
            UserId = userId;
        }
        public string Content { get; }

        public Guid UserId { get; }
    }
}