using Twilight.Imperium.Domain.Battle.Entities;
using Twilight.Imperium.Domain.Battle.Events;
using Twilight.Imperium.Domain.Battle.Values.Root;
using Twilight.Imperium.Domain.Battle.Values.Spaceship;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Domain.Battle
{
    public class Battle : AggregateRoot<BattleId>
    {
        public List<Player> Players { get; set; }
        public GameTurn gameTurn { get; set; }

        private Battle( BattleId id ) : base( id )
        {
        }

        public Battle() : base( new BattleId() )
        {
            Subscriber( new BattleEventChange( this ) );
            AppendEvent( new BattleCreated() ).Invoke();
        }

        public void CreatePlayer( string name )
        {
            AppendEvent( new PlayerCreated( name ) ).Invoke();
        }

        public void CreateSpaceship( string PlayerId, List<(int, int)> damageAndCapacity )
        {
            AppendEvent( new SpaceshipCreated( PlayerId, damageAndCapacity ) ).Invoke();
        }

        public void StartGameTurn( string playerId, string enemyId )
        {
            AppendEvent( new TurnGameStarted( playerId, enemyId ) ).Invoke();
        }

        public void AttackPlayerSpaceship()
        {
            AppendEvent( new PlayerSpaceshipAttacked() ).Invoke();
        }

        public void AttackPlayerGroundForce()
        {
            AppendEvent( new PlayerGroundForceAttacked() ).Invoke();
        }

        public void AddSpecialCard( EnumCard specialCard )
        {
            AppendEvent( new SpecialCardAdded( specialCard ) ).Invoke();
        }

        public void ApplyEffectSpecialCard()
        {
            AppendEvent( new SpecialCardEffectApplied() ).Invoke();
        }

        public void Surrender()
        {
            AppendEvent( new PlayerSurrendered() ).Invoke();
        }

        public void AddGroundForces( string playerId, int groundForces )
        {
            AppendEvent( new GroundForceAdded( playerId, groundForces ) ).Invoke();
        }

        public static Battle From( string identity, List<DomainEvent> events )
        {
            var battle = new Battle( BattleId.Of( identity ) );
            events.ForEach( battle.Apply );
            return battle;
        }
    }
}
