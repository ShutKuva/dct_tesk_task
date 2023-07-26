namespace CryptoCurrency.Abstractions.Adapters
{
    public interface IEntityReceiverAdapter<TBriefEntity, TDetailedEntity>
    {
        Task<IEnumerable<TBriefEntity>> GetTopEntities(int skip, int count, string? search);
        Task<TDetailedEntity?> GetDetailedEntity(string id);
    }
}
