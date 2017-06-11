using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;

namespace Arge.ViewModels
{
    public class ShellViewModel : ReactiveObject, IScreen, ISupportsActivation
    {
        #region Properties & Fields

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public RoutingState Router { get; }

        public ReactiveList<IRoutableViewModel> NavigationTargets { get; private set; }

        private IRoutableViewModel _selectedNavigationTarget;
        public IRoutableViewModel SelectedNavigationTarget
        {
            get => _selectedNavigationTarget;
            set => this.RaiseAndSetIfChanged(ref _selectedNavigationTarget, value);
        }

        #endregion

        #region Commands

        public ReactiveCommand OpenHomepage { get; }
        public ReactiveCommand<IRoutableViewModel, Unit> Navigate { get; }

        #endregion

        #region Constructors

        public ShellViewModel()
        {
            Router = new RoutingState();
            OpenHomepage = ReactiveCommand.Create(() => Process.Start("http://arge.be"));
            Navigate = ReactiveCommand.Create<IRoutableViewModel>(x => Router.Navigate.Execute(x));

            this.WhenAnyValue(vm => vm.NavigationTargets).Where(x => x != null).Subscribe(x => SelectedNavigationTarget = x.FirstOrDefault());
            this.WhenAnyValue(vm => vm.SelectedNavigationTarget).Where(x => x != null).InvokeCommand(Navigate);

            //TODO DarthAffe 28.05.2017: I don't really like this ...
            this.WhenActivated(delegate (Action<IDisposable> action)
            {
                NavigationTargets = new ReactiveList<IRoutableViewModel>(new[]
                                                                         {
                                                                             (IRoutableViewModel)Locator.Current.GetService(typeof(LightingLayoutViewModel)),
                                                                             (IRoutableViewModel)Locator.Current.GetService(typeof(SettingsLayoutViewModel))
                                                                         });
            });

        }

        #endregion
    }
}
