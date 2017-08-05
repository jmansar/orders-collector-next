using System;

namespace OrdersCollector.Core.Groups.Models
{
    public class Group
    {
        public Group(
            Guid id,
            string name,
            Guid suppliersCollectionId,
            Guid participantsCollectionId
        )
        {
            Id = id;
            Name = name;
            SuppliersCollectionId = suppliersCollectionId;
            ParticipantsCollectionId = participantsCollectionId;
        }

        public Guid Id { get; }
        
        public string Name { get; }

        public Guid SuppliersCollectionId { get; }

        public Guid ParticipantsCollectionId { get; }
    }
}