using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Commands
{
    public class AttackPlayerGroundForceCommand : Command<BattleId>
    {
        public AttackPlayerGroundForceCommand( string aggregateId ) : base( BattleId.Of( aggregateId ) )
        {
        }
    }
}
