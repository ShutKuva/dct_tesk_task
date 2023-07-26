namespace BLL.Abstractions
{
    public interface IEntityReceiver<TBriefEntity, TDetailedEntity>
    {
        Task<IEnumerable<TBriefEntity>?> GetTopEntities(int skip, int count, string? serach);
        Task<TDetailedEntity?> GetDetailedEntity(string id, int skipMarkets, int countMarkets);
    }
}
