using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Values.GameTurn
{
    public class GameTurnId : Identity
    {
        public GameTurnId() : base() { }

        private GameTurnId( string value ) : base( value ) { }

        public static GameTurnId Of( string value )
        {
            return new GameTurnId( value );
        }
    }
}
