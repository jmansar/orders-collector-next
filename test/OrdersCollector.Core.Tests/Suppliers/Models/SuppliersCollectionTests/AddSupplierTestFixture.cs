using System;
using NUnit.Framework;
using OrdersCollector.Core.Suppliers.Models;

namespace OrdersCollector.Core.Tests.Suppliers.Models.SuppliersCollectionTests
{
    public class AddSupplierTestFixture
    {

        [SetUp]
        public void SetUp()
        {
        }

        private SuppliersCollection CreateSubject()
        {
            return new SuppliersCollection(
                Guid.NewGuid(),
                Guid.NewGuid()
            );
        }

    }
}