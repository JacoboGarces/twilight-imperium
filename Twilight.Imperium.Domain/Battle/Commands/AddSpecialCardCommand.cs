using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Domain.Battle.Values.Spaceship;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Commands
{
    public class AddSpecialCardCommand : Command<BattleId>
    {
        public EnumCard SpecialCard { get; set; }

        public AddSpecialCardCommand( EnumCard specialCard, string aggregateId ) : base( BattleId.Of( aggregateId ) )
        {
            SpecialCard = specialCard;
        }
    }
}
