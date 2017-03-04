using System;
using OrdersCollector.Core.Orders.Commands;

namespace OrdersCollector.Core.Tests.Builders
{
    public class CreateOrderCommandBuilder
    {
        private Guid groupId;
        private Guid supplierId;
        private Guid userId;

        public CreateOrderCommandBuilder()
        {
            groupId = Guid.NewGuid();
            supplierId = Guid.NewGuid();
            userId = Guid.NewGuid();
        }

        public CreateOrderCommand Build()
        {
            return new CreateOrderCommand(
                groupId,
                supplierId,
                userId);
        }
    }
}