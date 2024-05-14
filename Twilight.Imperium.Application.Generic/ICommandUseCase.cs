using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Generic
{
    public interface ICommandUseCase<T, I> where T : Command<I> where I : Identity
    {
        Task<List<DomainEvent>> Execute( T command );
    }
}
