using System;
using System.Collections.Generic;

namespace OrdersCollector.Core.Suppliers.Models
{
    public class Supplier
    {
        public Supplier(
            Guid supplierInfoId, 
            string name, 
            params string[] aliases)
        {
            SupplierInfoId = supplierInfoId;
            Name = name;
            Aliases = aliases;
        }

        public Guid SupplierInfoId { get; }

        public string Name { get; }

        public IReadOnlyCollection<string> Aliases { get; }
    }
}