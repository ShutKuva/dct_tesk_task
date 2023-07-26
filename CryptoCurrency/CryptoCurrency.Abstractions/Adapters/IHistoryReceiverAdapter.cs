namespace CryptoCurrency.Abstractions.Adapters
{
    public interface IHistoryReceiverAdapter<TConvertedObject>
    {
        Task<IEnumerable<TConvertedObject>> GetSeries(string id, string interval, long? start, long? end);
    }
}
