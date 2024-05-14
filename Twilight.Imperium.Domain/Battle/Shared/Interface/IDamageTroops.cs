namespace Twilight.Imperium.Domain.Battle.Shared.Interface
{
    public interface IDamageTroops<T>
    {
        Life ReceiveDamage();
        T ThrowingDice();
    }
}
