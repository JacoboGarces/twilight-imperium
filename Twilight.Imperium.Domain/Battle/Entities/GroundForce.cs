using Twilight.Imperium.Domain.Battle.Shared;
using Twilight.Imperium.Domain.Battle.Shared.Interface;
using Twilight.Imperium.Domain.Battle.Values.GroundForce;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Entities
{
    public class GroundForce : Entity<GroundForceId>, IDamageTroops<GroundForce?>
    {
        public Life Life { get; private set; }
        public Damage Damage { get; private set; }

        public GroundForce() : base( new GroundForceId() )
        {
            Life = Life.Of( 1 );
            Damage = Damage.Of( 8 );
        }

        public Life ReceiveDamage()
        {
            int DAMAGE = 1;

            if (Life.Value != 0)
            {
                return Life = Life.Of( Life.Value - DAMAGE );
            }

            return Life;
        }

        public GroundForce? ThrowingDice()
        {
            var random = new Random();
            int Dice = random.Next( 1, 10 );

            if (Dice > Damage.Value)
            {
                return this;
            }

            return null;
        }

        public static GroundForce From()
        {
            return new GroundForce();
        }
    }
}
