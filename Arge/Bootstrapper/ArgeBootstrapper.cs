using System.Collections.Generic;
using System.Windows;
using Arge.ViewModels;

namespace Arge.Bootstrapper
{
    public class ArgeBootstrapper : UnityBootstrapper
    {
        #region Methods

        protected override void RegisterTypes()
        {
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            Dictionary<string, object> settings = new Dictionary<string, object>
                           {
                               { "Title", "Arge" },
                               { "SizeToContent", SizeToContent.Manual },
                               { "Width"  , 1280 },
                               { "Height" , 720  }
                           };
            DisplayRootViewFor<ShellViewModel>(settings);
        }

        #endregion
    }
}
