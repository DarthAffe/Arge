using System.Windows;
using Arge.Bootstrapper;

namespace Arge
{
    public partial class App : Application
    {
        #region Properties & Fields

        private readonly ArgeBootstrapper _bootstrapper;

        #endregion

        #region Constructors

        public App()
        {
            _bootstrapper = new ArgeBootstrapper();
        }

        #endregion

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _bootstrapper.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            _bootstrapper.OnExit(e);
        }

        #endregion
    }
}
