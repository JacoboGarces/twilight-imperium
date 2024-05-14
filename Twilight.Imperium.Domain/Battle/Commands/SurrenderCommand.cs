using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Commands
{
    public class SurrenderCommand : Command<BattleId>
    {
        public SurrenderCommand( string aggregateId ) : base( BattleId.Of( aggregateId ) )
        {
        }
    }
}
