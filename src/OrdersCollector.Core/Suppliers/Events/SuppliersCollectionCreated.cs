using System;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Suppliers.Events
{
    public class SuppliersCollectionCreated : Event
    {
        public SuppliersCollectionCreated(
            Guid suppliersCollectionId,
            Guid groupId
        )
        {
            this.SuppliersCollectionId = suppliersCollectionId;
            this.GroupId = groupId;
        }

        public Guid SuppliersCollectionId { get; }

        public Guid GroupId { get; }
    }
}