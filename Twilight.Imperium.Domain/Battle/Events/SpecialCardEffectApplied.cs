using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class SpecialCardEffectApplied : DomainEvent
    {
        public SpecialCardEffectApplied() : base( EnumEvents.PLAYER_SPECIAL_CARD_EFFECT_APPLIED.ToString() )
        {
        }
    }
}
