using BLL.Abstractions;
using BLL.Models;
using CryptoCurrency.Abstractions.Adapters;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrency.Adapters
{
    class HistoryReceiverAdapter : IHistoryReceiverAdapter<ObservablePoint>
    {
        private readonly IHistoryReceiver<HistoryNode> _historyReceiver;

        public HistoryReceiverAdapter(IHistoryReceiver<HistoryNode> historyReceiver)
        {
            _historyReceiver = historyReceiver;
        }

        public async Task<IEnumerable<ObservablePoint>> GetSeries(string id, string interval, long? start, long? end)
        {
            IEnumerable<HistoryNode> nodes = await _historyReceiver.GetHistory(id, interval, start, end);

            return nodes.Select(node => new ObservablePoint(node.Time, (double)node.PriceUsd));
        }
    }
}
