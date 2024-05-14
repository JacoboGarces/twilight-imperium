using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class PlayerSurrendered : DomainEvent
    {
        public PlayerSurrendered() : base( EnumEvents.PLAYER_SURRENDERED.ToString() )
        {
        }
    }
}
