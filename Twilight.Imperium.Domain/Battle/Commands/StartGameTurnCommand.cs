using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Commands
{
    public class StartGameTurnCommand : Command<BattleId>
    {
        public string PlayerId { get; set; }
        public string EnemyId { get; set; }

        public StartGameTurnCommand( string playerId, string enemyId, string aggregateId ) : base( BattleId.Of( aggregateId ) )
        {
            PlayerId = playerId;
            EnemyId = enemyId;
        }
    }
}
