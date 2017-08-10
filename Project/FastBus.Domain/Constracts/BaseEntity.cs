namespace FastBus.Domain.Constracts
{
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
