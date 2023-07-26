using System.Collections.Generic;

namespace CryptoCurrency.Models
{
    public class DetailedCurrency
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal? Volume { get; set; }
        public decimal? Price { get; set; }
        public decimal? Changes { get; set; }
        public IEnumerable<Market> Markets { get; set; } = default!;
    }
}
