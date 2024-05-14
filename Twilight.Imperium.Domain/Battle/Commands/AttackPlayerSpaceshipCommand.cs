using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Commands
{
    public class AttackPlayerSpaceshipCommand : Command<BattleId>
    {
        public AttackPlayerSpaceshipCommand( string aggregateId ) : base( BattleId.Of( aggregateId ) )
        {
        }
    }
}
