using Arge.ViewModels;
using ReactiveUI;

namespace Arge.Views
{
    public class ShellViewBase : AbstractView<ShellViewModel> { }

    public partial class ShellView : ShellViewBase
    {
        #region Constructors

        public ShellView(ShellViewModel viewModel)
        {
            InitializeComponent();

            this.ViewModel = viewModel;

            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.NavigationManager.Navigation, v => v.TargetNavigation.Content));
            });
        }

        #endregion
    }
}
