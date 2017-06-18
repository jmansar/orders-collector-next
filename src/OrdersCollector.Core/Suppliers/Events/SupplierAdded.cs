using System;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Suppliers.Events
{
    public class SupplierAdded : Event
    {
        public SupplierAdded(
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