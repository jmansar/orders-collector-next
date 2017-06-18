using System;
using System.Linq;
using NUnit.Framework;
using OrdersCollector.Core.Suppliers.Events;
using OrdersCollector.Core.Suppliers.Models;

namespace OrdersCollector.Core.Tests.Suppliers.Models.SuppliersCollectionTests
{
    public class ConstructorTestFixture
    {
        private Guid suppliersCollectionId;
        private Guid groupId;

        [SetUp]
        public void SetUp()
        {
            suppliersCollectionId = Guid.NewGuid();
            groupId = Guid.NewGuid();
        }

        [Test]
        public void Constructor_WhenInvoked_MustCreateSuppliersCollectionWithId()
        {
            // Arrange, Act
            var subject = new SuppliersCollection(suppliersCollectionId, groupId);

            // Assert
            Assert.That(subject.Id, Is.EqualTo(suppliersCollectionId));
        }

        [Test]
        public void Constructor_WhenInvoked_MustCreateSuppliersCollectionWithGroupId()
        {
            // Arrange, Act
            var subject = new SuppliersCollection(suppliersCollectionId, groupId);

            // Assert
            Assert.That(subject.GroupId, Is.EqualTo(groupId));
        }

        [Test]
        public void Constructor_WhenInvoked_MustAddSuppliersCollectionCreatedEvent()
        {
            // Arrange, Act
            var subject = new SuppliersCollection(suppliersCollectionId, groupId);

            // Assert
            var @event = subject.GetUncommitedChanges().Single();
            Assert.That(@event, Is.TypeOf<SuppliersCollectionCreated>());
        }

        [Test]
        public void Constructor_WhenInvoked_MustAddSuppliersCollectionCreatedWithGroupId()
        {
            // Arrange, Act
            var subject = new SuppliersCollection(suppliersCollectionId, groupId);

            // Assert
            var @event = subject.GetUncommitedChanges().Single() as SuppliersCollectionCreated;
            Assert.That(@event.GroupId, Is.EqualTo(groupId));
        }

        [Test]
        public void Constructor_WhenInvoked_MustAddSuppliersCollectionId()
        {
            // Arrange, Act
            var subject = new SuppliersCollection(suppliersCollectionId, groupId);

            // Assert
            var @event = subject.GetUncommitedChanges().Single() as SuppliersCollectionCreated;
            Assert.That(@event.SuppliersCollectionId, Is.EqualTo(suppliersCollectionId));
        }
    }
}