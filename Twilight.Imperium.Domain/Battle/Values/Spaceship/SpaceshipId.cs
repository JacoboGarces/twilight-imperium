using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Values.Spaceship
{
    public class SpaceshipId : Identity
    {
        public SpaceshipId() : base() { }

        private SpaceshipId( string value ) : base( value ) { }

        public static SpaceshipId Of( string value )
        {
            return new SpaceshipId( value );
        }
    }
}
