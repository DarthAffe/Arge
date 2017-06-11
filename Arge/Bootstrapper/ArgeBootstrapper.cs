using System.Windows;
using Arge.Configuration;
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
            RegisterView<LightingLayoutView, LightingLayoutViewModel>();
            RegisterView<SettingsLayoutView, SettingsLayoutViewModel>();
        }

        protected override void InitializeHandlers()
        {
        }

        protected override void Start()
        {
            Config.Instance.Load();
            Container.Resolve<ThemeManager>().LoadTheme(Config.Instance[ConfigEntryType.Theme]);

            ShellView window = Container.Resolve<ShellView>();
            RegisterSingleton(typeof(IScreen), window.ViewModel);
            Application.Current.MainWindow = window;
            window.Show();
        }

        #endregion
    }
}
