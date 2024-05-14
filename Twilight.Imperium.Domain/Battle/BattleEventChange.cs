using Twilight.Imperium.Domain.Battle.Entities;
using Twilight.Imperium.Domain.Battle.Events;
using Twilight.Imperium.Domain.Battle.Values.GameTurn;
using Twilight.Imperium.Domain.Battle.Values.Spaceship;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle
{
    public class BattleEventChange : EventChange
    {
        public BattleEventChange( Battle battle )
        {
            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not BattleCreated) return;
                var domainEvent = (BattleCreated)@event;

                battle.Players = [];
            } );

            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not PlayerCreated) return;
                var domainEvent = (PlayerCreated)@event;

                if (battle.Players.Count >= 2)
                {
                    throw new Exception( "The battle cannot have more than two players" );
                }

                battle.Players.Add( Player.From( domainEvent.Name ) );
            } );

            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not SpaceshipCreated) return;
                var domainEvent = (SpaceshipCreated)@event;

                Player? player = battle.Players.First( player => player.Id.Value == domainEvent.PlayerId );

                if (player is null)
                {
                    throw new Exception( "Player with specified ID not found" );
                }

                battle.Players.Remove( player );
                int spaceships = player.AddSpaceship( domainEvent.DamageAndCapacity );
                battle.Players.Add( player );

                if (spaceships == 0)
                {
                    throw new Exception( "Could not add Spaceships in the player" );
                }
            } );

            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not GroundForceAdded) return;
                var domainEvent = (GroundForceAdded)@event;

                Player? player = battle.Players.First( player => player.Id.Value == domainEvent.PlayerId );

                if (player is null)
                {
                    throw new Exception( "Player with specified ID not found" );
                }

                if (player.Spaceships is null)
                {
                    throw new Exception( "The player has no spaceships in which to transport ground forces" );
                }

                battle.Players.Remove( player );
                player.AddGroundForces( domainEvent.GroundForces );
                battle.Players.Add( player );
            } );

            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not TurnGameStarted) return;
                var domainEvent = (TurnGameStarted)@event;

                Player? player = battle.Players.First( player => player.Id.Value == domainEvent.PlayerId );
                Player? enemy = battle.Players.First( player => player.Id.Value == domainEvent.EnemyId );

                if (player is null || enemy is null)
                {
                    throw new Exception( "Player or Enemy with specified ID not found" );
                }

                battle.gameTurn = GameTurn.From( player, enemy );
            } );

            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not PlayerSpaceshipAttacked) return;
                var domainEvent = (PlayerSpaceshipAttacked)@event;

                if (battle.gameTurn.Status.Value != EnumStatus.ACTIVE)
                {
                    throw new Exception( "The battle has already ended by surrender" );
                }

                battle.gameTurn.ThrowingDiceSpaceship();
            } );

            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not PlayerGroundForceAttacked) return;
                var domainEvent = (PlayerGroundForceAttacked)@event;

                if (battle.gameTurn.Status.Value != EnumStatus.ACTIVE)
                {
                    throw new Exception( "The battle has already ended by surrender" );
                }

                if (!battle.gameTurn.IsValidGroundAttack())
                {
                    throw new Exception( "You cannot attack on land, there are still spaceships" );
                }

                battle.gameTurn.ThrowingDiceGroundForce();
            } );

            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not SpecialCardAdded) return;
                var domainEvent = (SpecialCardAdded)@event;

                if (battle.gameTurn.Status.Value != EnumStatus.ACTIVE)
                {
                    throw new Exception( "The battle has already ended by surrender" );
                }

                if (battle.gameTurn.Player.Spaceships[0].SpecialCard != null)
                {
                    throw new Exception( "The player already has a special card" );
                }

                SpecialCard specialCard = battle.gameTurn.AssignSpecialCard( SpecialCard.Of( domainEvent.SpecialCard ) );

                if (specialCard.Value != domainEvent.SpecialCard)
                {
                    throw new Exception( "The special card could not be added" );
                }
            } );

            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not SpecialCardEffectApplied) return;
                var domainEvent = (SpecialCardEffectApplied)@event;

                if (battle.gameTurn.Status.Value != EnumStatus.ACTIVE)
                {
                    throw new Exception( "The battle has already ended by surrender" );
                }

                if (battle.gameTurn.Player.Spaceships[0].SpecialCard is null)
                {
                    throw new Exception( "The player does not have a special card" );
                }

                battle.gameTurn.ApplyCardEffectSpaceship();
            } );

            AddSub( ( DomainEvent @event ) =>
            {
                if (@event is not PlayerSurrendered) return;
                var domainEvent = (PlayerSurrendered)@event;

                if (battle.gameTurn.Status.Value != EnumStatus.ACTIVE)
                {
                    throw new Exception( "The battle has already ended by surrender" );
                }

                battle.gameTurn.Surrender();
            } );
        }
    }
}
