using System.Collections.Generic;
using System.Windows;
using Arge.Configuration;
using Arge.Themes;
using Arge.ViewModels;
using Caliburn.Micro;
using Microsoft.Practices.Unity;

namespace Arge.Bootstrapper
{
    public class ArgeBootstrapper : UnityBootstrapper
    {
        #region Methods

        protected override void RegisterTypes()
        {
            RegisterInterface<IWindowManager, ArgeWindowManager>();

            RegisterSingleton<ThemeManager>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            Config.Instance.Load();
            Container.Resolve<ThemeManager>().LoadTheme(Config.Instance[ConfigEntryType.Theme]);

            Dictionary<string, object> settings = new Dictionary<string, object>
                           {
                               { "Title", "Arge" },
                               { "Width"  , 1280 },
                               { "Height" , 720  }
                           };
            DisplayRootViewFor<ShellViewModel>(settings);
        }

        #endregion
    }
}
