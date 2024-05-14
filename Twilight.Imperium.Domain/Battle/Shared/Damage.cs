using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Shared
{
    public class Damage : IValueObject<int>
    {
        public int Value { get; private set; }

        public Damage(int value)
        {
            ValidateValue(value);

            Value = value;
        }

        public static Damage Of(int value)
        {
            return new Damage(value);
        }

        private void ValidateValue(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Damage value must be greater than zero");
            }
        }
    }
}
