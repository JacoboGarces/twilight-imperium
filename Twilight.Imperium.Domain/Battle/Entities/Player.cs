using Twilight.Imperium.Domain.Battle.Values.Player;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Entities
{
    public class Player : Entity<PlayerId>
    {
        public Name Name { get; private set; }
        public List<Spaceship> Spaceships { get; private set; }

        private Player( Name name ) : base( new PlayerId() )
        {
            Name = name;
            Spaceships = new List<Spaceship>();
        }

        public int AddSpaceship( List<(int, int)> damageAndCapacity )
        {
            foreach (var spaceship in damageAndCapacity)
            {
                Spaceships.Add( Spaceship.From( spaceship.Item1, spaceship.Item2 ) );
            }

            return Spaceships.Count;
        }

        public void AddGroundForces( int groundForces )
        {
            ValidateCapacity( groundForces );

            foreach (var spaceship in Spaceships)
            {
                if (spaceship.Capacity.Value > 0 && spaceship.GroundForces.Count == 0)
                {
                    int remainingCapacity = spaceship.Capacity.Value - spaceship.GroundForces.Count;
                    int forcesToAdd = Math.Min( groundForces, remainingCapacity );

                    for (int i = 0; i < forcesToAdd; i++)
                    {
                        spaceship.AddGroundForce();
                    }

                    groundForces -= forcesToAdd;

                    if (groundForces == 0) break;
                }
            }
        }

        private void ValidateCapacity( int groundForces )
        {
            int totalAvailableCapacity = Spaceships
                .Where( s => s.Capacity.Value > 0 && s.GroundForces.Count == 0 )
                .Sum( s => s.Capacity.Value - s.GroundForces.Count );

            if (totalAvailableCapacity < groundForces)
            {
                throw new InvalidOperationException( $"There is not enough spaceship capacity to add {groundForces} ground forces. Maximum available: {totalAvailableCapacity}" );
            }
        }

        public static Player From( string name )
        {
            return new Player( Name.Of( name ) );
        }
    }
}
