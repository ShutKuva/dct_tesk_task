namespace BLL.Abstractions
{
    public interface IHistoryReceiver<THistoryObject>
    {
        Task<IEnumerable<THistoryObject>> GetHistory(string id, string interval, long? start, long? end);
    }
}
