using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class PlayerCreated : DomainEvent
    {
        public string Name { get; set; }

        public PlayerCreated( string name ) : base( EnumEvents.PLAYER_CREATED.ToString() )
        {
            Name = name;
        }
    }
}
