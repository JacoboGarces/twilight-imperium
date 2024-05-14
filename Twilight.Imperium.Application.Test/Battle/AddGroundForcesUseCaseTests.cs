using Moq;
using Twilight.Imperium.Application.Generic;
using Twilight.Imperium.Domain.Battle.Commands;
using Twilight.Imperium.Domain.Battle.Events;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Battle.Tests
{
    public class AddGroundForcesUseCaseTests
    {
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly AddGroundForcesUseCase _useCase;

        public AddGroundForcesUseCaseTests()
        {
            _mockRepository = new Mock<IEventRepository>();
            _useCase = new AddGroundForcesUseCase( _mockRepository.Object );
        }

        [Fact]
        public void Execute_Success()
        {
            var command = new AddGroundForcesCommand( "PlayerId", 2, "battle_id" );

            _mockRepository.Setup( repo => repo.FindByAggregateId( "battle_id" ) ).Returns( new List<DomainEvent>() );

            var result = _useCase.Execute( command );

            Assert.NotNull( result );
            Assert.NotEmpty( result );
            Assert.IsType<GroundForceAdded>( result[0] );
            _mockRepository.Verify( repo => repo.FindByAggregateId( "battle_id" ), Times.Once );
            _mockRepository.Verify( repo => repo.Save( It.IsAny<DomainEvent>() ), Times.AtLeastOnce );
        }

        [Fact]
        public void Execute_Success_Event()
        {
            var cratedBattle = new BattleCreated();
            cratedBattle.AggregateId = "battle_id";

            var createdPlayer = new PlayerCreated( "Player777" );
            createdPlayer.AggregateId = "battle_id";

            List<(int, int)> spaceships = new List<(int, int)>();
            spaceships.Add( new( 8, 0 ) );
            spaceships.Add( new( 6, 4 ) );
            var createdSpaceship = new SpaceshipCreated( "PlayerId", spaceships );
            createdSpaceship.AggregateId = "battle_id";

            var addGroundForce = new GroundForceAdded( "PlayerId", 1 );
            addGroundForce.AggregateId = "battle_id";

            var command = new AddGroundForcesCommand( "PlayerId", 1, "battle_id" );


            List<DomainEvent> domainEvents = new List<DomainEvent>();
            domainEvents.Add( cratedBattle );
            domainEvents.Add( createdPlayer );
            domainEvents.Add( createdSpaceship );
            domainEvents.Add( addGroundForce );

            _mockRepository.Setup( repo => repo.FindByAggregateId( "battle_id" ) )
                .Returns( domainEvents );

            _mockRepository.Setup( repo => repo.Save( It.IsAny<GroundForceAdded>() ) )
                .Returns( ( GroundForceAdded ev ) => ev );

            var result = _useCase.Execute( command );

            var groundForce = (GroundForceAdded)result[0];
            Assert.Equal( 1, result.Count );
            Assert.Equal( 1, groundForce.GroundForces );
            Assert.Equal( addGroundForce.Type, result[0].Type );
            Assert.IsType<GroundForceAdded>( result[0] );
            _mockRepository.Verify( repo => repo.FindByAggregateId( "battle_id" ), Times.Once );
            _mockRepository.Verify( repo => repo.Save( It.IsAny<DomainEvent>() ), Times.AtLeastOnce );
        }
    }
}