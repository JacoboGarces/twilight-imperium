using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Commands
{
    public class ApplyEffectSpecialCardCommand : Command<BattleId>
    {
        public ApplyEffectSpecialCardCommand( string aggregateId ) : base( BattleId.Of( aggregateId ) )
        {
        }
    }
}
