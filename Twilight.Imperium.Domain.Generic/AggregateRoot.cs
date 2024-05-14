namespace Twilight.Imperium.Generic
{
    public abstract class AggregateRoot<I> : Entity<I> where I : Identity
    {
        private readonly ChangeEventSubscriber _changeEventSubscriber;

        public AggregateRoot( I id ) : base( id )
        {
            _changeEventSubscriber = new();
        }

        public List<DomainEvent> GetUncommittedChanges()
        {
            return _changeEventSubscriber.Events.ToList();
        }

        public void MarkAsCommitted()
        {
            _changeEventSubscriber.Events.Clear();
        }

        protected void Subscriber( EventChange eventChange )
        {
            _changeEventSubscriber.Subscribe( eventChange );
        }

        protected void Apply( DomainEvent domainEvent )
        {
            _changeEventSubscriber.Apply( domainEvent );
        }

        protected Action AppendEvent( DomainEvent domainEvent )
        {
            string aggregateName = this.GetType().Name.ToLower();
            domainEvent.AggregateName = aggregateName;
            domainEvent.AggregateId = Id.Value;
            return _changeEventSubscriber.Append( domainEvent );
        }
    }
}
