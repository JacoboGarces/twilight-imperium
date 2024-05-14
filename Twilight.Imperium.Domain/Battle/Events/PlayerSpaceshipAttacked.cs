using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class PlayerSpaceshipAttacked : DomainEvent
    {
        public PlayerSpaceshipAttacked() : base( EnumEvents.PLAYER_SPACESHIP_ATTACKED.ToString() )
        {
        }
    }
}
