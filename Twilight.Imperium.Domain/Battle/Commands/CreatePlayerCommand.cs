using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Commands
{
    public class CreatePlayerCommand : Command<BattleId>
    {
        public string Name { get; set; }

        public CreatePlayerCommand( string name, string aggregateId ) : base( BattleId.Of( aggregateId ) )
        {
            Name = name;
        }
    }
}
