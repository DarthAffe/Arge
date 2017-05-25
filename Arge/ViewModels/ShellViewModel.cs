using System;
using Arge.Bootstrapper;
using Arge.Enums;
using ReactiveUI;

namespace Arge.ViewModels
{
    public class ShellViewModel : AbstractViewModel
    {
        #region Properties & Fields

        public NavigationManager NavigationManager => NavigationManager.Instance;

        #endregion

        #region Constructors

        public ShellViewModel()
        {
            this.WhenActivated(delegate (Action<IDisposable> action)
                               {
                                   NavigationManager.Navigate<NavigationViewModel>(NavigationTargets.Navigation);
                               });
        }

        #endregion

        #region Methods

        #endregion
    }
}
