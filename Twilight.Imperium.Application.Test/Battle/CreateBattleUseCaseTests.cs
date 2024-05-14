using Moq;
using Twilight.Imperium.Application.Generic;
using Twilight.Imperium.Domain.Battle.Commands;
using Twilight.Imperium.Domain.Battle.Events;
using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Battle.Tests
{
    public class CreateBattleUseCaseTests
    {
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly CreateBattleUseCase _useCase;

        public CreateBattleUseCaseTests()
        {
            _mockRepository = new Mock<IEventRepository>();
            _useCase = new CreateBattleUseCase( _mockRepository.Object );
        }

        [Fact]
        public async void Execute_Success()
        {
            var domainEvent = new BattleCreated();
            var command = new CreateBattleCommand();

            List<DomainEvent> result = await _useCase.Execute( command );

            Assert.Equal( 1, result.Count );
            Assert.Equal( domainEvent.Type, result[0].Type );
            Assert.IsType<BattleCreated>( result[0] );
        }
    }
}
