using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Values.GroundForce
{
    public class GroundForceId : Identity
    {
        public GroundForceId() : base() { }

        private GroundForceId( string value ) : base( value ) { }

        public static GroundForceId Of( string value )
        {
            return new GroundForceId( value );
        }
    }
}
