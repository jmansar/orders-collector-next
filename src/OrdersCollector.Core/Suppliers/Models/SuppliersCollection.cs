using System;
using System.Collections.Generic;
using OrdersCollector.Core.Suppliers.Events;
using OrdersCollector.Core.Suppliers.Exceptions;
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
            RegisterEventHandler<SupplierAdded>(When);
        }

        public Guid GroupId { get; private set; }

        public IReadOnlyCollection<Supplier> Suppliers => suppliers.AsReadOnly();

        public void AddSupplier(Guid supplierInfoId, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (suppliersDict.ContainsKey(name))
            {
                throw new SupplierWithSpecifiedNameOrAliasAlreadyExistsException(
                    name
                );
            }

            RaiseEvent(new SupplierAdded(
                supplierInfoId,
                name
            ));
        }

        private void When(SuppliersCollectionCreated @event)
        {
            Id = @event.SuppliersCollectionId;
            GroupId = @event.GroupId;
        }

        private void When(SupplierAdded @event)
        {
            var supplier = new Supplier(
                @event.SupplierInfoId,
                @event.Name
            );

            suppliers.Add(supplier);
            suppliersDict[supplier.Name] = supplier;
        }
    }
}