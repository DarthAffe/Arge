using Arge.ViewModels;
using ReactiveUI;

namespace Arge.Views
{
    public class NavigationViewBase : ReactiveUserControl<NavigationViewModel>
    { }

    public partial class NavigationView : NavigationViewBase
    {
        public NavigationView(NavigationViewModel viewModel)
        {
            InitializeComponent();

            this.ViewModel = viewModel;
        }
    }
}
