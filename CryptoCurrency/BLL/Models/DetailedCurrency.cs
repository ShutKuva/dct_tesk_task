using Newtonsoft.Json;

namespace BLL.Models
{
    public class DetailedCurrency
    {
        public string Id { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal? PriceUsd { get; set; }
        public decimal? ChangePercent24Hr { get; set; }
        public decimal? VolumUsd24Hr { get; set; }

        [JsonIgnore]
        public IEnumerable<Market> Markets { get; set; } = default!;
    }
}
