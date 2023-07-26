using AutoMapper;
using BLL.Abstractions;
using CryptoCurrency.Abstractions.Adapters;
using CryptoCurrency.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLLBriefCurrency = BLL.Models.BriefCurrency;
using BLLDetailedCurrency = BLL.Models.DetailedCurrency;

namespace CryptoCurrency.Adapters
{
    public class EntityReceiverAdapter : IEntityReceiverAdapter<BriefCurrency, DetailedCurrency>
    {
        private readonly IEntityReceiver<BLLBriefCurrency, BLLDetailedCurrency> _entityReceiver;
        private readonly IMapper _mapper;

        public EntityReceiverAdapter(IEntityReceiver<BLLBriefCurrency, BLLDetailedCurrency> entityReceiver, IMapper mapper)
        {
            _entityReceiver = entityReceiver;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BriefCurrency>> GetTopEntities(int skip, int count, string? search)
        {
            IEnumerable<BLLBriefCurrency>? pureTopAssets = await _entityReceiver.GetTopEntities(skip, count, search);

            if (pureTopAssets is null)
            {
                return new List<BriefCurrency>();
            }

            return _mapper.Map<IEnumerable<BLLBriefCurrency>, IEnumerable<BriefCurrency>>(pureTopAssets);
        }

        public async Task<DetailedCurrency?> GetDetailedEntity(string id)
        {
            BLLDetailedCurrency? pureDetailedCurrency = await _entityReceiver.GetDetailedEntity(id, 0, 10);

            if (pureDetailedCurrency is null)
            {
                return default;
            }

            return _mapper.Map<BLLDetailedCurrency, DetailedCurrency>(pureDetailedCurrency);
        }
    }
}
