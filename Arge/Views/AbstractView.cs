using ReactiveUI;

namespace Arge.Views
{
    public class AbstractView<TViewModel> : ReactiveUserControl<TViewModel>
        where TViewModel : class
    {
        #region Constructors

        protected AbstractView()
        {
            this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext);
        }

        #endregion
    }
}
