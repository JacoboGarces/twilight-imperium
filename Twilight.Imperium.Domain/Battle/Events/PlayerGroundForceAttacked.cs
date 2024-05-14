using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class PlayerGroundForceAttacked : DomainEvent
    {
        public PlayerGroundForceAttacked() : base( EnumEvents.PLAYER_GROUND_FORCE_ATTACKED.ToString() )
        {
        }
    }
}
