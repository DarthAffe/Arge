using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using Arge.Configuration;
using Arge.Controls.Window;
using Arge.Themes;
using Arge.ViewModels;
using Arge.Views;
using Microsoft.Practices.Unity;
using ReactiveUI;

namespace Arge.Bootstrapper
{
    public class ArgeBootstrapper : UnityBootstrapper
    {
        #region Methods

        protected override void RegisterTypes()
        {
            RegisterServices();
            RegisterViews();
        }

        private void RegisterServices()
        {
            RegisterSingleton<ThemeManager>();
        }

        private void RegisterViews()
        {
            RegisterView<ShellView, ShellViewModel>();
            RegisterView<NavigationView, NavigationViewModel>();
        }

        protected override void InitializeHandlers()
        {
        }

        protected override void Start()
        {
            Config.Instance.Load();
            Container.Resolve<ThemeManager>().LoadTheme(Config.Instance[ConfigEntryType.Theme]);

            BlurredDecorationWindow window = new BlurredDecorationWindow
            {
                Width = 1280,
                Height = 720,
                Icon = new BitmapImage(new Uri("pack://application:,,,/Arge;component/Resources/ArgeBee.ico")),
                IconCommand = ReactiveCommand.Create(() => Process.Start("http://arge.be")),
                Content = Container.Resolve<ShellView>()
            };
            Application.Current.MainWindow = window;
            window.Show();
        }

        #endregion
    }
}
