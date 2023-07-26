using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using CryptoCurrency.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoCurrency.Services
{
    public class ThemeService : ObservableObject, IThemeService<string, string>
    {
        private readonly Themes _themes;

        private ICommand _changeThemeCommand = default!;
        private ICollection<string> _themeKeys = default!;
        private string _currentKey = string.Empty;

        public ThemeService(Themes themes)
        {
            _changeThemeCommand = new RelayCommand<string>(ChangeTheme);

            _themes = themes;
        }

        public ICollection<string> Keys => _themeKeys;
        public string CurrentKey => _currentKey;

        public ICommand ChangeThemeCommand => _changeThemeCommand;

        public Task StartAsync(string parameter)
        {
            _themeKeys = _themes.ThemesWithPaths.Keys;
            ChangeTheme(_themes.DefaultTheme);
            OnPropertyChanged(nameof(Keys));
            return Task.CompletedTask;
        }

        private void ChangeTheme(string? themeKey)
        {
            if (themeKey is null)
            {
                return;
            }

            ResourceDictionary oldTheme = (ResourceDictionary)Application.Current.Resources.FindName("Theme");
            Application.Current.Resources.MergedDictionaries.Remove(oldTheme);

            var newTheme = new ResourceDictionary()
            {
                Source = new Uri(_themes.ThemesWithPaths[themeKey], UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(newTheme);

            _currentKey = themeKey;

            OnPropertyChanged(nameof(CurrentKey));
        }
    }
}
