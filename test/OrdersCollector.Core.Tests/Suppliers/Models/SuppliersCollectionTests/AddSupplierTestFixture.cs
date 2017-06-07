using System;
using System.Linq;
using NUnit.Framework;
using OrdersCollector.Core.Suppliers.Events;
using OrdersCollector.Core.Suppliers.Exceptions;
using OrdersCollector.Core.Suppliers.Models;

namespace OrdersCollector.Core.Tests.Suppliers.Models.SuppliersCollectionTests
{
    public class AddSupplierTestFixture
    {
        private Guid supplierInfoId;
        private string name;

        [SetUp]
        public void SetUp()
        {
            supplierInfoId = Guid.NewGuid();
            name = $"supplier {Guid.NewGuid()}";
        }

        [Test]
        public void AddSupplier_WhenNameIsNotNull_MustThrowException()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            TestDelegate action = () => subject.AddSupplier(supplierInfoId, null);

            // Assert
            Assert.That(action, Throws.ArgumentNullException
                .With.Property("ParamName").EqualTo("name"));
        }

        [Test]
        public void AddSupplier_WhenInvoked_MustAddSupplierWithSupplierInfoId()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            subject.AddSupplier(supplierInfoId, name);

            // Assert
            var supplier = subject.Suppliers.Single();
            Assert.That(supplier.SupplierInfoId, Is.EqualTo(supplierInfoId));
        }
         
        [Test]
        public void AddSupplier_WhenInvoked_MustAddSupplierWithSupplierName()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            subject.AddSupplier(supplierInfoId, name);

            // Assert
            var supplier = subject.Suppliers.Single();
            Assert.That(supplier.Name, Is.EqualTo(name));
        }
        
        [Test]
        public void AddSupplier_WhenInvoked_MustAddSupplierAddedEvent()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            subject.AddSupplier(supplierInfoId, name);

            // Assert
            var @event = subject.GetUncommitedChanges().Last();
            Assert.That(@event, Is.TypeOf<SupplierAdded>());
        }
      
        [Test]
        public void AddSupplier_WhenInvoked_MustAddSupplierAddedEventWithSupplierName()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            subject.AddSupplier(supplierInfoId, name);

            // Assert
            var @event = subject.GetUncommitedChanges().Last() as SupplierAdded;
            Assert.That(@event.Name, Is.EqualTo(name));
        }

        [Test]
        public void AddSupplier_WhenInvoked_MustAddSupplierAddedEventWithSupplierInfoId()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            subject.AddSupplier(supplierInfoId, name);

            // Assert
            var @event = subject.GetUncommitedChanges().Last() as SupplierAdded;
            Assert.That(@event.SupplierInfoId, Is.EqualTo(supplierInfoId));
        }

        [Test]
        public void AddSupplier_WhenSupplierWithSpecifiedNameAlreadyExists_MustThrowException()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            subject.AddSupplier(Guid.NewGuid(), name);
            TestDelegate action = () => subject.AddSupplier(supplierInfoId, name);

            // Assert
            Assert.That(action, Throws.TypeOf<SupplierWithSpecifiedNameOrAliasAlreadyExistsException>()
                .With.Property("SupplierName").EqualTo(name));
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