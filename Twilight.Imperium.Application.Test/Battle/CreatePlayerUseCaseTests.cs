using Moq;
using Twilight.Imperium.Application.Generic;
using Twilight.Imperium.Domain.Battle.Commands;
using Twilight.Imperium.Domain.Battle.Events;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Battle.Tests
{
    public class CreatePlayerUseCaseTests
    {
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly CreatePlayerUseCase _useCase;

        public CreatePlayerUseCaseTests()
        {
            _mockRepository = new Mock<IEventRepository>();
            _useCase = new CreatePlayerUseCase( _mockRepository.Object );
        }

        [Fact]
        public void Execute_Success()
        {
            var command = new CreatePlayerCommand( "PlayerName", "battle_id" );

            _mockRepository.Setup( repo => repo.FindByAggregateId( "battle_id" ) ).Returns( new List<DomainEvent>() );

            var result = _useCase.Execute( command );

            Assert.NotNull( result );
            Assert.NotEmpty( result );
            Assert.IsType<PlayerCreated>( result[0] );
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

            var command = new CreatePlayerCommand( "Player777", "battle_id" );

            List<DomainEvent> domainEvents = new List<DomainEvent>();
            domainEvents.Add( cratedBattle );
            domainEvents.Add( createdPlayer );

            _mockRepository.Setup( repo => repo.FindByAggregateId( "battle_id" ) )
                .Returns( domainEvents );

            _mockRepository.Setup( repo => repo.Save( It.IsAny<PlayerCreated>() ) )
                .Returns( ( PlayerCreated ev ) => ev );

            var result = _useCase.Execute( command );

            var player = (PlayerCreated)result[0];
            Assert.Equal( 1, result.Count );
            Assert.Equal( "Player777", player.Name );
            Assert.Equal( createdPlayer.Type, result[0].Type );
            Assert.IsType<PlayerCreated>( result[0] );
            _mockRepository.Verify( repo => repo.FindByAggregateId( "battle_id" ), Times.Once );
            _mockRepository.Verify( repo => repo.Save( It.IsAny<DomainEvent>() ), Times.AtLeastOnce );
        }
    }
}