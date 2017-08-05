using System;

namespace OrdersCollector.Core.Groups.Models
{
    public class Participant
    {
        public Participant(string name, Guid? userId)
        {
            this.Name = name;
            this.UserId = userId;
        }
        
        public string Name { get; }

        public Guid? UserId { get; }
    }
}