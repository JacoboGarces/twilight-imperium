using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Values.Player
{
    public class Name : IValueObject<string>
    {
        public string Value { get; private set; }

        public Name( string value )
        {
            ValidateValue( value );

            Value = value.Trim();
        }

        public static Name Of( string value )
        {
            return new Name( value );
        }

        private void ValidateValue( string value )
        {
            if (string.IsNullOrWhiteSpace( value ))
            {
                throw new ArgumentException( "Name cannot be null or empty" );
            }
        }
    }
}
