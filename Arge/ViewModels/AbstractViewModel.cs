using ReactiveUI;

namespace Arge.ViewModels
{
    public abstract class AbstractViewModel : ReactiveObject, ISupportsActivation
    {
        #region Properties & Fields

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        #endregion
    }
}
