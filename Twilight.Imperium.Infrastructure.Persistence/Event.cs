using Twilight.Imperium.Generic;

namespace Twilight.Imperium.Infrastructure.Persistence
{
  public class Event : DomainEvent
  {
    public Event( string type ) : base( type )
    {
    }
  }
}
