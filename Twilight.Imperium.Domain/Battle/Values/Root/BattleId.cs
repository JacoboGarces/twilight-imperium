using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Values.Root
{
    public class BattleId : Identity
    {
        public BattleId() : base() { }

        private BattleId( string value ) : base( value ) { }

        public static BattleId Of( string value )
        {
            return new BattleId( value );
        }
    }
}
