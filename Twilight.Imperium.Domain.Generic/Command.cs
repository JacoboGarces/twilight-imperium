namespace Twilight.Imperium.Generic
{
    public abstract class Command<T> where T : Identity
    {
        public T AggregateId { get; set; }

        protected Command( T aggregateId )
        {
            AggregateId = aggregateId;
        }
    }

    public abstract class InitialCommand
    {

    }
}