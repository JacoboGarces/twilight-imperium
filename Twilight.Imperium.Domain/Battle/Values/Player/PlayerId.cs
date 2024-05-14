using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Values.Player
{
    public class PlayerId : Identity
    {
        public PlayerId() : base() { }

        private PlayerId( string value ) : base( value ) { }

        public static PlayerId Of( string value )
        {
            return new PlayerId( value );
        }
    }
}
