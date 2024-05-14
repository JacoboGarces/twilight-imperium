using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class GroundForceAdded : DomainEvent
    {
        public string PlayerId { get; set; }
        public int GroundForces { get; set; }

        public GroundForceAdded( string playerId, int groundForces ) : base( EnumEvents.GROUND_FORCE_ADDED.ToString() )
        {
            GroundForces = groundForces;
            PlayerId = playerId;
        }
    }
}
