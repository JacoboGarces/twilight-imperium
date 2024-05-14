using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Commands
{
    public class CreateSpaceshipCommand : Command<BattleId>
    {
        public string PlayerId { get; set; }
        public List<(int, int)> DamageAndCapacity { get; set; }

        public CreateSpaceshipCommand( string playerId, List<(int, int)> damageAndCapacity, string aggregateId ) : base( BattleId.Of( aggregateId ) )
        {
            PlayerId = playerId;
            DamageAndCapacity = damageAndCapacity;
        }
    }
}
