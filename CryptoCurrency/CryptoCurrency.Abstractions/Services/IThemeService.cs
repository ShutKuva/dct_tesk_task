using System.Windows.Input;

namespace CryptoCurrency.Abstractions.Services
{
    public interface IThemeService<TKey, TParameter> : IStartableService<TKey>
    {
        ICollection<TKey> Keys { get; }

        TKey CurrentKey { get; }

        ICommand ChangeThemeCommand { get; }
    }
}
