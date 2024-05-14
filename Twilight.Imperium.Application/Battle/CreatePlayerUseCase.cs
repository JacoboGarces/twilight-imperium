using Twilight.Imperium.Application.Generic;
using Twilight.Imperium.Domain.Battle.Commands;
using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Battle
{
  public class CreatePlayerUseCase : ICommandUseCase<CreatePlayerCommand, BattleId>
  {
    private readonly IEventRepository _repository;

    public CreatePlayerUseCase( IEventRepository repository )
    {
      _repository = repository;
    }

    public async Task<List<DomainEvent>> Execute( CreatePlayerCommand command )
    {
      var events = await _repository.FindByAggregateId( command.AggregateId.Value );
      var battle = Domain.Battle.Battle.From( command.AggregateId.Value, events );
      battle.CreatePlayer( command.Name );

      var domainEvents = battle.GetUncommittedChanges().ToList();

      domainEvents.ForEach( ( DomainEvent domainEvent ) =>
      {
        _repository.Save( domainEvent );
      } );

      battle.MarkAsCommitted();
      return domainEvents;
    }
  }
}
