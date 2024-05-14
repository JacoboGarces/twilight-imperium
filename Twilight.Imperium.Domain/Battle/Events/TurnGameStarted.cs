using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class TurnGameStarted : DomainEvent
    {
        public string PlayerId { get; set; }
        public string EnemyId { get; set; }

        public TurnGameStarted( string playerId, string enemyId ) : base( EnumEvents.TURN_GAME_STARTED.ToString() )
        {
            PlayerId = playerId;
            EnemyId = enemyId;
        }
    }
}
