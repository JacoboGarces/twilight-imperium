using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Values.GameTurn
{
    public class Status : IValueObject<EnumStatus>
    {
        public EnumStatus Value { get; private set; }

        public Status( EnumStatus value )
        {
            ValidateType( value );

            Value = value;
        }

        public static Status Of( EnumStatus value )
        {
            return new Status( value );
        }

        private void ValidateType( EnumStatus value )
        {
            if (!Enum.IsDefined( typeof( EnumStatus ), value ))
            {
                throw new ArgumentException( "Invalid type" );
            }
        }
    }
}
