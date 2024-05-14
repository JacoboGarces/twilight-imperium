using Twilight.Imperium.Domain.Battle.Shared;
using Twilight.Imperium.Domain.Battle.Values.GameTurn;
using Twilight.Imperium.Domain.Battle.Values.Spaceship;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle.Entities
{
    public class GameTurn : Entity<GameTurnId>
    {
        public Status Status { get; private set; }
        public Player Player { get; private set; }
        public Player Enemy { get; private set; }
        public List<Spaceship> DestroyedEnemySpaceships { get; private set; }
        public List<GroundForce> DestroyedEnemyGroundForces { get; private set; }

        public GameTurn( Player player, Player enemy ) : base( new GameTurnId() )
        {
            Status = Status.Of( EnumStatus.ACTIVE );
            Player = player;
            Enemy = enemy;
            DestroyedEnemySpaceships = new List<Spaceship>();
            DestroyedEnemyGroundForces = new List<GroundForce>();
        }

        public void ThrowingDiceSpaceship()
        {
            foreach (var spaceship in Player.Spaceships)
            {
                if (spaceship.Life.Value > 0)
                {
                    Spaceship? result = spaceship.ThrowingDice();

                    if (result != null)
                    {
                        AssignDamageSpaceship();
                    }
                }
            }

            ChangeTurn();
        }

        public void ThrowingDiceGroundForce()
        {
            foreach (var spaceship in Player.Spaceships)
            {
                ThrowingDiceForGroundForces( spaceship );
            }

            ChangeTurn();
        }

        private void ThrowingDiceForGroundForces( Spaceship enemySpaceship )
        {
            foreach (var groundForce in enemySpaceship.GroundForces)
            {
                if (groundForce.Life.Value > 0)
                {
                    GroundForce? result = groundForce.ThrowingDice();

                    if (result != null)
                    {
                        AssignDamageGroundForce();
                    }
                }
            }
        }

        public Status Surrender()
        {
            return Status = Status.Of( EnumStatus.SURRENDERED );
        }

        public SpecialCard AssignSpecialCard( SpecialCard specialCard )
        {
            foreach (var spaceship in Player.Spaceships)
            {
                spaceship.AddCardEffect( specialCard );
            }

            return Player.Spaceships[0].SpecialCard;
        }

        public SpecialCard ApplyCardEffectSpaceship()
        {
            foreach (var spaceship in Player.Spaceships)
            {
                spaceship.ApplyCardEffect();
                spaceship.RemoveSpecialCard();
            }

            return Player.Spaceships[0].SpecialCard;
        }

        public bool IsValidGroundAttack()
        {
            foreach (var spaceship in Enemy.Spaceships)
            {
                if (spaceship.Life.Value > 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void AssignDamageSpaceship()
        {
            foreach (var enemySpaceship in Enemy.Spaceships)
            {
                if (enemySpaceship.Life.Value > 0)
                {
                    Life remainingLife = enemySpaceship.ReceiveDamage();
                    if (remainingLife.Value == 0)
                    {
                        DestroyedEnemySpaceships.Add( enemySpaceship );
                    }
                }
            }
        }

        private void AssignDamageGroundForce()
        {
            foreach (var enemySpaceship in Enemy.Spaceships)
            {
                DamageGroundForce( enemySpaceship );
            }
        }

        private void DamageGroundForce( Spaceship enemySpaceship )
        {
            foreach (var enemyGroundForce in enemySpaceship.GroundForces)
            {
                if (enemyGroundForce.Life.Value > 0)
                {
                    Life remainingLife = enemyGroundForce.ReceiveDamage();
                    if (remainingLife.Value == 0)
                    {
                        DestroyedEnemyGroundForces.Add( enemyGroundForce );
                    }
                }
            }
        }

        private void ChangeTurn()
        {
            Player playerAux = Player;
            Player = Enemy;
            Enemy = playerAux;
        }

        public static GameTurn From( Player player, Player enemy )
        {
            return new GameTurn( player, enemy );
        }
    }
}
