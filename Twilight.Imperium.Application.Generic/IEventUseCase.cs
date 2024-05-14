using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Application.Generic
{
    public interface IEventUseCase<T> where T : DomainEvent
    {
        List<DomainEvent> Execute( T domainEvent );
    }
}
