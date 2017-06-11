using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using Arge.Attributes;
using ReactiveUI;

namespace Arge.Resources
{
    public sealed class Fonts : ReactiveObject
    {
        #region Properties & Fields

        public static Fonts Instance { get; } = new Fonts();

        private FontFamily _default;
        [FileName("default")]
        public FontFamily Default
        {
            get => _default ?? new FontFamily("Segoe UI");
            set => _default = value;
        }

        private FontFamily _tooltip;
        [FileName("tooltip")]
        public FontFamily Tooltip
        {
            get => _tooltip ?? Default;
            set => _tooltip = value;
        }

        private FontFamily _navigation;
        [FileName("navigation")]
        public FontFamily Navigation
        {
            get => _navigation ?? Default;
            set => _navigation = value;
        }

        #endregion

        #region Constructors

        private Fonts()
        { }

        #endregion

        #region Methods

        public void Update(string baseDirectory)
        {
            string[] files = Directory.GetFiles(baseDirectory);

            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                string fileName = propertyInfo.GetCustomAttribute<FileNameAttribute>()?.FileName;
                if (string.IsNullOrWhiteSpace(fileName)) continue;

                propertyInfo.SetValue(this, ConvertToFontFamily(files.FirstOrDefault(x => string.Equals(Path.GetFileNameWithoutExtension(x), fileName, StringComparison.OrdinalIgnoreCase))));
                // ReSharper disable once ExplicitCallerInfoArgument - impossible!
                this.RaisePropertyChanged(propertyInfo.Name);
            }
        }

        private FontFamily ConvertToFontFamily(string path) => string.IsNullOrWhiteSpace(path) ? null : System.Windows.Media.Fonts.GetFontFamilies(path).FirstOrDefault();

        #endregion
    }
}
