namespace BLL.Models
{
    public class BriefCurrency
    {
        public string Id { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal? PriceUsd { get; set; }
        public decimal? ChangePercent24Hr { get; set; }
    }
}
