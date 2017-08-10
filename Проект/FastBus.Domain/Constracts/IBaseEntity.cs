namespace FastBus.Domain.Constracts
{
    public interface IBaseEntity<TKey> where TKey : struct
    {
        TKey Id { get; set; }
    }
}
