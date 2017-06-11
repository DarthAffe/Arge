using ReactiveUI;

namespace Arge.ViewModels
{
    public abstract class AbstractViewModel : ReactiveObject, ISupportsActivation, IRoutableViewModel
    {
        #region Properties & Fields

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        #endregion

        #region Constructors

        protected AbstractViewModel(string urlPathSegment, IScreen screen)
        {
            this.UrlPathSegment = urlPathSegment;
            this.HostScreen = screen;
        }

        #endregion
    }
}
