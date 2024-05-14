using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Commands
{
    public class AddGroundForcesCommand : Command<BattleId>
    {
        public string PlayerId { get; set; }
        public int GroundForces { get; set; }

        public AddGroundForcesCommand( string playerId, int groundForces, string aggregateId ) : base( BattleId.Of( aggregateId ) )
        {
            GroundForces = groundForces;
            PlayerId = playerId;
        }
    }
}
