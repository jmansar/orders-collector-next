using System;
using System.Collections.Generic;
using OrdersCollector.Core.Suppliers.Events;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Suppliers.Models
{
    public class SuppliersCollection : AggregateRoot 
    {
        private List<Supplier> suppliers = new List<Supplier>();
        private Dictionary<string, Supplier> suppliersDict = new Dictionary<string, Supplier>();

        public SuppliersCollection(
            Guid suppliersCollectionId,
            Guid groupId
        ) : this()
        {
            RaiseEvent(new SuppliersCollectionCreated(
                suppliersCollectionId,
                groupId
            ));
        }

        private SuppliersCollection()
        {
            RegisterEventHandler<SuppliersCollectionCreated>(When);
        }

        public Guid GroupId { get; private set; }

        private void When(SuppliersCollectionCreated @event)
        {
            Id = @event.SuppliersCollectionId;
            GroupId = @event.GroupId;
        }
    }
}