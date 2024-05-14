using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Values.Spaceship
{
    public class SpecialCard : IValueObject<EnumCard>
    {
        public EnumCard Value { get; private set; }

        public SpecialCard( EnumCard value )
        {
            ValidateType( value );

            Value = value;
        }

        public static SpecialCard Of( EnumCard value )
        {
            return new SpecialCard( value );
        }

        private void ValidateType( EnumCard value )
        {
            if (!Enum.IsDefined( typeof( EnumCard ), value ))
            {
                throw new ArgumentException( "Invalid type" );
            }
        }
    }
}
