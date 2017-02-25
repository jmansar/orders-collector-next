using System;

namespace OrdersCollector.EventSourcing
{
    public class UnsupportedEventException : Exception
    {
        public UnsupportedEventException(IEvent @event) 
            : base($"Event {@event.GetType()} is not supported. Event handler is not registered.")
         {
            Event = @event;
         }
        
        public IEvent Event { get; }
    }
}