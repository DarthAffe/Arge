using Arge.Controls.Window;
using Arge.ViewModels;
using ReactiveUI;

namespace Arge.Views
{
    public class ShellViewBase : BlurredDecorationWindow, IViewFor<ShellViewModel>
    {
        #region Properties & Fields

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as ShellViewModel;
        }
        public ShellViewModel ViewModel { get; set; }

        #endregion

        #region Constructors

        protected ShellViewBase()
        {
            this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext);
        }

        #endregion
    }

    public partial class ShellView : ShellViewBase
    {
        #region Constructors

        public ShellView(ShellViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;

            this.WhenActivated(d =>
                               {
                                   d(this.OneWayBind(ViewModel, vm => vm.OpenHomepage, view => view.IconCommand));
                                   d(this.OneWayBind(ViewModel, vm => vm.NavigationTargets, view => view.ListBoxNavigation.ItemsSource));
                                   d(this.Bind(ViewModel, vm => vm.SelectedNavigationTarget, view => view.ListBoxNavigation.SelectedItem));
                               });
        }

        #endregion
    }
}
