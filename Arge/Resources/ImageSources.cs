using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using Arge.Attributes;
using ReactiveUI;

namespace Arge.Resources
{
    public class ImageSources : ReactiveObject
    {
        #region Properties & Fields

        private static ImageSources _instace;
        public static ImageSources Instance => _instace ?? (_instace = new ImageSources());

        private readonly ImageSourceConverter _imageSourceConverter = new ImageSourceConverter();

        [FileName("background")]
        public ImageSource WindowBackground { get; private set; }

        [FileName("close")]
        public ImageSource WindowClose { get; private set; }

        [FileName("minimize")]
        public ImageSource WindowMinimize { get; private set; }

        #endregion

        #region Constructors

        private ImageSources()
        { }

        #endregion

        #region Methods

        public void Update(string baseDirectory)
        {
            string[] files = Directory.GetFiles(baseDirectory);

            foreach (PropertyInfo propertyInfo in typeof(ImageSources).GetProperties())
            {
                string fileName = propertyInfo.GetCustomAttribute<FileNameAttribute>()?.FileName;
                if (string.IsNullOrWhiteSpace(fileName)) continue;

                propertyInfo.SetValue(this, ConvertToImageSource(files.FirstOrDefault(x => string.Equals(Path.GetFileNameWithoutExtension(x), fileName, StringComparison.OrdinalIgnoreCase))));
                // ReSharper disable once ExplicitCallerInfoArgument - impossible!
                this.RaisePropertyChanged(propertyInfo.Name);
            }
        }

        private ImageSource ConvertToImageSource(string path)
        {
            return string.IsNullOrWhiteSpace(path) ? null : _imageSourceConverter.ConvertFromString(path) as ImageSource;
        }

        #endregion
    }
}
