using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Generic
{
  public interface IEventRepository
  {
    Task<List<DomainEvent>> Save( DomainEvent domainEvent );
    Task<List<DomainEvent>> FindByAggregateId( string aggregateId );
  }
}
