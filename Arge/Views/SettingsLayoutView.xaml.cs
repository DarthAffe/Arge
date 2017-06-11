using Arge.ViewModels;

namespace Arge.Views
{
    public class LightingLayoutViewBase : AbstractView<LightingLayoutViewModel> { }

    public partial class LightingLayoutView : LightingLayoutViewBase
    {
        public LightingLayoutView(LightingLayoutViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
        }
    }
}
