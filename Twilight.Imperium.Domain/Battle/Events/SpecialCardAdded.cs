using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Domain.Battle.Values.Spaceship;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Events
{
    public class SpecialCardAdded : DomainEvent
    {
        public EnumCard SpecialCard { get; set; }

        public SpecialCardAdded( EnumCard specialCard ) : base( EnumEvents.PLAYER_SPECIAL_CARD_ADDED.ToString() )
        {
            SpecialCard = specialCard;
        }
    }
}
