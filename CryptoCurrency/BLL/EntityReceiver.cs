using BLL.Abstractions;
using BLL.Models;

namespace BLL
{
    public class EntityReceiver : IEntityReceiver<BriefCurrency, DetailedCurrency>
    {
        private readonly IHttpService _httpService;

        public EntityReceiver(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<BriefCurrency>?> GetTopEntities(int skip, int count, string? search)
        {
            MultipleData<BriefCurrency>? assets = await _httpService
                .GetObject<MultipleData<BriefCurrency>>("assets", GenerateQueryDictionary(skip, count, search));

            if (assets is null)
            {
                return default;
            }

            return assets.Data;
        }

        public async Task<DetailedCurrency?> GetDetailedEntity(string id, int skipMarkets, int countMarkets)
        {
            SingleData<DetailedCurrency>? asset = await _httpService.GetObject<SingleData<DetailedCurrency>>($"assets/{id}");

            if (asset is null)
            {
                return null;
            }

            MultipleData<Market>? markets = await _httpService.GetObject<MultipleData<Market>>($"assets/{id}/markets", GenerateQueryDictionary(skipMarkets, countMarkets, null));

            DetailedCurrency result = asset.Data;

            result.Markets = markets?.Data ?? new List<Market>();

            return result;
        }

        private Dictionary<string, string> GenerateQueryDictionary(int skip, int count, string? search)
        {
            var queries = new Dictionary<string, string>();

            if (skip >= 0)
            {
                queries.Add("offset", skip.ToString());
            }

            if (count > 0)
            {
                queries.Add("limit", count.ToString());
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                queries.Add("search", search);
            }

            return queries;
        }
    }
}
