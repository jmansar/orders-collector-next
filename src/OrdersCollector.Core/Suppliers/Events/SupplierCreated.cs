using System;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Suppliers.Events
{
    public class SupplierCreated : Event
    {
        public SupplierCreated(
            Guid supplierInfoId,
            string name
        )
        {
            SupplierInfoId = supplierInfoId;
            Name = name;
        }

        public Guid SupplierInfoId { get; }

        public string Name { get; }
    }
}