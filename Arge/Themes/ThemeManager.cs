using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using Arge.Resources;
using RGB.NET.Core;

namespace Arge.Themes
{
    public class ThemeManager
    {
        #region Constants

        private const string THEME_FOLDER = "Themes";
        private const string THEME_FILE = "Theme.xaml";

        #endregion

        #region Properties & Fields

        private List<Theme> _availableThemes;
        public IEnumerable<Theme> AvailableThemes => _availableThemes;

        public Theme CurrentTheme { get; private set; }

        public string CurrentThemeFolder => CurrentTheme == null ? null : Path.Combine(PathHelper.GetAbsolutePath(THEME_FOLDER), CurrentTheme.Name);

        private ResourceDictionary _currentlyLoadedDictionary;

        #endregion

        #region Constructors

        public ThemeManager()
        {
            CheckAvailableThemes();
        }

        #endregion

        #region Methods

        public void LoadTheme(string name)
        {
            Theme theme = AvailableThemes.First(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
            LoadTheme(theme);
        }

        public void LoadTheme(Theme theme)
        {
            if (theme == null) return;

            ResourceDictionary themeDictionary;
            using (FileStream fs = new FileStream(theme.Path, FileMode.Open))
                themeDictionary = (ResourceDictionary)XamlReader.Load(fs);

            if (themeDictionary == null) return;

            if (_currentlyLoadedDictionary != null)
                Application.Current.Resources.MergedDictionaries.Remove(_currentlyLoadedDictionary);

            Application.Current.Resources.MergedDictionaries.Add(themeDictionary);
            _currentlyLoadedDictionary = themeDictionary;

            CurrentTheme = theme;

            ImageSources.Instance.Update(CurrentThemeFolder);
        }

        public void CheckAvailableThemes()
        {
            _availableThemes = Directory.GetDirectories(PathHelper.GetAbsolutePath(THEME_FOLDER))
                                        .Select(x => new Theme(Path.GetFileName(x), Path.Combine(x, THEME_FILE)))
                                        .ToList();
        }

        #endregion
    }
}
