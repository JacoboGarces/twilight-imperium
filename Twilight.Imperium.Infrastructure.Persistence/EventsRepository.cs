using MongoDB.Driver;
using Twilight.Imperium.Application.Generic;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Infrastructure.Persistence
{
  public class EventsRepository : IEventRepository
  {
    private readonly IMongoCollection<Event> _eventRepository;

    public EventsRepository( IMongoCollection<Event> repository )
    {
      _eventRepository = repository;
    }

    public async Task<List<DomainEvent>> FindByAggregateId( string aggregateId )
    {
      var cursor = _eventRepository.FindAsync( ( eve ) => eve.AggregateId.Equals( aggregateId ) ).Result;
      var events = await cursor.ToListAsync();
      return events.Select(e => e as DomainEvent).ToList();
    }

    public async Task<List<DomainEvent>> Save( DomainEvent domainEvent )
    {
      var @event = new Event( domainEvent.Type )
      {
        AggregateId = domainEvent.AggregateId,
        AggregateName = domainEvent.AggregateName,
        Moment = domainEvent.Moment,
        Type = domainEvent.Type,
        UUID = domainEvent.UUID,
        Version = domainEvent.Version
      };
      _eventRepository.InsertOne( @event );

      return await FindByAggregateId( domainEvent.AggregateId );
    }
  }
}
