using Arge.ViewModels;

namespace Arge.Views
{
    public class SettingsLayoutViewBase : AbstractView<SettingsLayoutViewModel> { }

    public partial class SettingsLayoutView : SettingsLayoutViewBase
    {
        public SettingsLayoutView(SettingsLayoutViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
        }
    }
}
