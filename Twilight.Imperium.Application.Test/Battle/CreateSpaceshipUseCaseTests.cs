using Moq;
using Twilight.Imperium.Application.Generic;
using Twilight.Imperium.Domain.Battle.Commands;
using Twilight.Imperium.Domain.Battle.Events;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Battle.Tests
{
    public class CreateSpaceshipUseCaseTests
    {
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly CreateSpaceshipUseCase _useCase;

        public CreateSpaceshipUseCaseTests()
        {
            _mockRepository = new Mock<IEventRepository>();
            _useCase = new CreateSpaceshipUseCase( _mockRepository.Object );
        }

        [Fact]
        public void Execute_Success()
        {
            List<(int, int)> spaceships = new List<(int, int)>();
            spaceships.Add( new( 8, 0 ) );
            spaceships.Add( new( 6, 4 ) );
            var command = new CreateSpaceshipCommand( "PlayerId", spaceships, "battle_id" );


            List<DomainEvent> domainEvents = new List<DomainEvent>();

            _mockRepository.Setup( repo => repo.FindByAggregateId( "battle_id" ) ).Returns( new List<DomainEvent>() );

            var result = _useCase.Execute( command );

            Assert.NotNull( result );
            Assert.NotEmpty( result );
            Assert.IsType<SpaceshipCreated>( result[0] );
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

            var command = new CreateSpaceshipCommand( "PlayerId", spaceships, "battle_id" );

            List<DomainEvent> domainEvents = new List<DomainEvent>();
            domainEvents.Add( cratedBattle );
            domainEvents.Add( createdPlayer );
            domainEvents.Add( createdSpaceship );

            _mockRepository.Setup( repo => repo.FindByAggregateId( "battle_id" ) )
                .Returns( domainEvents );

            _mockRepository.Setup( repo => repo.Save( It.IsAny<SpaceshipCreated>() ) )
                .Returns( ( SpaceshipCreated ev ) => ev );

            var result = _useCase.Execute( command );

            var spaceship = (SpaceshipCreated)result[0];
            Assert.Equal( 1, result.Count );
            Assert.Equal( spaceships.Count, spaceship.DamageAndCapacity.Count );
            Assert.Equal( createdSpaceship.Type, result[0].Type );
            Assert.IsType<SpaceshipCreated>( result[0] );
            _mockRepository.Verify( repo => repo.FindByAggregateId( "battle_id" ), Times.Once );
            _mockRepository.Verify( repo => repo.Save( It.IsAny<DomainEvent>() ), Times.AtLeastOnce );
        }
    }
}