namespace Twilight.Imperium.Generic
{
    public abstract class Entity<I> where I : Identity
    {
        public I Id { get; private set; }

        public Entity( I identity )
        {
            if (identity == null)
            {
                throw new ArgumentNullException( nameof( identity ), "The identity cannot be null" );
            }

            Id = identity;
        }
    }
}