using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class SpaceshipCreated : DomainEvent
    {
        public string PlayerId { get; set; }
        public List<(int, int)> DamageAndCapacity { get; set; }

        public SpaceshipCreated( string playerId, List<(int, int)> damageAndCapacity ) : base( EnumEvents.SPACESHIP_CREATED.ToString() )
        {
            PlayerId = playerId;
            DamageAndCapacity = damageAndCapacity;
        }
    }
}
