using System;
using System.Collections.Generic;
using OrdersCollector.EventSourcing;

namespace OrdersCollector.Core.Groups.Models
{
    public class ParticipantsCollection : AggregateRoot
    {
        private List<Participant> participants = new List<Participant>();

        public ParticipantsCollection(
            Guid participantsCollectionId,
            Guid groupId
        ) : this()
        {

        }

        private ParticipantsCollection()
        {

        }

        public Guid GroupId { get; private set; }

        public IReadOnlyCollection<Participant> Participants => participants.AsReadOnly());

        public void AddParticipant(string name, Guid? userId)
        {
            
        }
    }
}