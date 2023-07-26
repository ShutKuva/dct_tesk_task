using BLL.Abstractions;
using BLL.Models;
using System.Globalization;

namespace BLL
{
    public class HistoryReceiver : IHistoryReceiver<HistoryNode>
    {
        private readonly IHttpService _httpService;

        public HistoryReceiver(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<HistoryNode>> GetHistory(string id, string interval, long? start, long? end)
        {
            MultipleData<HistoryNode>? rawHistoryNodes = await _httpService.GetObject<MultipleData<HistoryNode>>($"assets/{id}/history", GetQueries(interval, start, end));

            return rawHistoryNodes?.Data ?? new List<HistoryNode>();
        }

        private Dictionary<string, string> GetQueries(string interval, long? start, long? end)
        {
            var result = new Dictionary<string, string>()
            {
                ["interval"] = interval,
            };

            if (start is not null)
            {
                result.Add("start", start.Value.ToString());
            }

            if (end is not null)
            {
                result.Add("end", end.Value.ToString());
            }

            return result;
        }
    }
}
