using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Generic
{
    public interface IInitialCommandUseCase<T> where T : InitialCommand
    {
        Task<List<DomainEvent>> Execute( T command );
    }
}
