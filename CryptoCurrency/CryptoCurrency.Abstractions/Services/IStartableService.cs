namespace CryptoCurrency.Abstractions.Services
{
    public interface IStartableService<TParameter>
    {
        public Task StartAsync(TParameter parameter);
    }
}
