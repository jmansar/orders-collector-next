using System;
using System.Threading.Tasks;

namespace OrdersCollector.EventSourcing
{
    public interface IAggregateRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
         Task<TAggregateRoot> Get(Guid aggregateRootId);

         Task Save(TAggregateRoot aggregateRoot);
    }
}