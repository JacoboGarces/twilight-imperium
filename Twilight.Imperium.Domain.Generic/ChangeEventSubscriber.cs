namespace Twilight.Imperium.Generic
{
    public class ChangeEventSubscriber
    {
        public List<DomainEvent> Events { get; set; } = [];
        private readonly HashSet<Action<DomainEvent>> _subscriptors = [];
        private readonly Dictionary<string, int> _versions = new Dictionary<string, int>();

        public void Subscribe( EventChange eventChange )
        {
            _subscriptors.UnionWith( eventChange.Subscriptors );
        }

        public Action Append( DomainEvent domainEvent )
        {
            Events.Add( domainEvent );
            return () => Apply( domainEvent );
        }

        public void Apply( DomainEvent domainEvent )
        {
            foreach (var sub in _subscriptors)
            {
                sub.Invoke( domainEvent );
                int newVersion = FindNextVersion( domainEvent );
                domainEvent.Version = newVersion;
            }
        }

        private int FindNextVersion( DomainEvent domainEvent )
        {
            if (_versions.ContainsKey( domainEvent.Type ))
            {
                _versions.TryGetValue( domainEvent.Type, out int version );
                return _versions[domainEvent.Type] = version + 1;
            }

            _versions.Add( domainEvent.Type, domainEvent.Version );
            return _versions[domainEvent.Type];
        }
    }
}
