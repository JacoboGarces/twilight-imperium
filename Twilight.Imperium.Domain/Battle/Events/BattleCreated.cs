using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class BattleCreated : DomainEvent
    {
        public BattleCreated() : base( EnumEvents.BATTLE_CREATED.ToString() )
        {
        }
    }
}
