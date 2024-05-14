using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Values.Spaceship
{
    public class Capacity : IValueObject<int>
    {
        public int Value { get; private set; }

        public Capacity( int value )
        {
            ValidateValue( value );

            Value = value;
        }

        public static Capacity Of( int value )
        {
            return new Capacity( value );
        }

        private void ValidateValue( int value )
        {
            if (value < 0)
            {
                throw new ArgumentException( "Capacity cannot be negative" );
            }
        }
    }
}
