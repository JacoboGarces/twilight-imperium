using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Shared
{
    public class Life : IValueObject<int>
    {
        public int Value { get; private set; }

        public Life(int value)
        {
            ValidateValue(value);

            Value = value;
        }

        public static Life Of(int value)
        {
            return new Life(value);
        }

        private void ValidateValue(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Life value must be greater than zero");
            }
        }
    }
}
