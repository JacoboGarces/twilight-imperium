namespace Twilight.Imperium.Generic
{
    public abstract class EventChange
    {
        public HashSet<Action<DomainEvent>> Subscriptors { get; private set; } = [];

        protected void AddSub<T>( Action<T> sub ) where T : DomainEvent
        {
            Subscriptors.Add( (Action<DomainEvent>)sub );
        }
    }
}
