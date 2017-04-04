using System;
using OrdersCollector.Core.Orders.Models;

namespace OrdersCollector.Core.Tests.Builders
{
    public class OrderBuilder
    {
        private Guid orderId;
        private Guid supplierId;
        private Guid groupId;
        
        public OrderBuilder()
        {
            orderId = Guid.NewGuid();
            supplierId = Guid.NewGuid();
            groupId = Guid.NewGuid();
        }

        public Order Build()
        {
            var order = new Order(
                orderId,
                supplierId,
                groupId
            );

            return order;
        }
    }
}