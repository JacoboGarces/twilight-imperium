using Twilight.Imperium.Application.Generic;
using Twilight.Imperium.Domain.Battle.Commands;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Battle
{
    public class CreateBattleUseCase : IInitialCommandUseCase<CreateBattleCommand>
    {
        private readonly IEventRepository _repository;

        public CreateBattleUseCase( IEventRepository repository )
        {
            _repository = repository;
        }

        public async Task<List<DomainEvent>> Execute( CreateBattleCommand command )
        {
            var battle = new Domain.Battle.Battle();

            var domainEvents = battle.GetUncommittedChanges().ToList();

            domainEvents.ForEach( async ( DomainEvent domainEvent ) =>
            {
                await _repository.Save( domainEvent );
            } );

            battle.MarkAsCommitted();
            return domainEvents;
        }
    }
}
