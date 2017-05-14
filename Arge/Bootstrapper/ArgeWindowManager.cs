using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using Arge.Controls;
using Arge.Misc;
using Caliburn.Micro;

namespace Arge.Bootstrapper
{
    public class ArgeWindowManager : WindowManager
    {
        #region Methods

        protected override Window EnsureWindow(object model, object view, bool isDialog)
        {
            Window window = view as Window;
            if (window == null)
            {
                window = new BlurredDecorationWindow()
                {
                    Content = view,
                    SizeToContent = SizeToContent.Manual,
                    Icon = new BitmapImage(new Uri("pack://application:,,,/Arge;component/Resources/ArgeBee.ico")),
                    IconCommand = new ActionCommand(() => Process.Start("http://arge.be"))
                };
                window.SetValue(View.IsGeneratedProperty, true);
                Window owner = InferOwnerOf(window);
                if (owner != null)
                {
                    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    window.Owner = owner;
                }
                else
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                Window owner = InferOwnerOf(window);
                if ((owner != null) && isDialog)
                    window.Owner = owner;
            }
            return window;
        }

        #endregion
    }
}
