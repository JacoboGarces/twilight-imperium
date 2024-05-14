using Twilight.Imperium.Domain.Battle.Shared;
using Twilight.Imperium.Domain.Battle.Shared.Interface;
using Twilight.Imperium.Domain.Battle.Values.Spaceship;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Entities
{
    public class Spaceship : Entity<SpaceshipId>, IDamageTroops<Spaceship?>
    {
        public Life Life { get; private set; }
        public Damage Damage { get; private set; }
        public Capacity Capacity { get; private set; }
        public SpecialCard? SpecialCard { get; private set; }
        public List<GroundForce> GroundForces { get; private set; }

        private Spaceship( Damage damage, Capacity capacity ) : base( new SpaceshipId() )
        {
            Life = Life.Of( 1 );
            Damage = damage;
            Capacity = capacity;
            GroundForces = new List<GroundForce>();
        }

        public void AddGroundForce()
        {
            GroundForces.Add( GroundForce.From() );
        }

        public void RemoveSpecialCard()
        {
            SpecialCard = null;
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

        public void AddCardEffect( SpecialCard specialCard )
        {
            if (SpecialCard is null)
            {
                SpecialCard = specialCard;
            }
        }

        public void ApplyCardEffect()
        {
            var optionActions = new Dictionary<EnumCard, Action>
              {
                {EnumCard.DEFENSE_PLUS, () => { IncreaseDefense(); }},
                {EnumCard.ATTACK_PLUS, () => { IncreaseDamageChance(); }},
            };

            if (optionActions.TryGetValue( SpecialCard.Value, out Action action ))
            {
                action();
            }
        }

        public Spaceship? ThrowingDice()
        {
            var random = new Random();
            int Dice = random.Next( 1, 10 );

            if (Dice > Damage.Value)
            {
                return this;
            }

            return null;
        }

        private void IncreaseDefense()
        {
            int DEFENSE = 1;
            Life = Life.Of( Life.Value + DEFENSE );
        }

        private void IncreaseDamageChance()
        {
            int DECREASE_PROBABILITY_DAMAGE = 2;
            Damage = Damage.Of( Damage.Value - DECREASE_PROBABILITY_DAMAGE );
        }

        public static Spaceship From( int damage, int capacity )
        {
            return new Spaceship( Damage.Of( damage ),
                                  Capacity.Of( capacity ) );
        }
    }
}
