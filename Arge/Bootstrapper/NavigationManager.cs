using System;
using System.Collections.Generic;
using Arge.Enums;
using Microsoft.Practices.Unity;

namespace Arge.Bootstrapper
{
    //TODO DarthAffe 21.05.2017: This allows some sort of fake VM-first - One day I should make this cleaner ...
    public class NavigationManager
    {
        #region Properties & Fields

        public static NavigationManager Instance { get; private set; }

        private readonly Dictionary<Type, Type> _viewTypeMapping = new Dictionary<Type, Type>();

        private readonly IUnityContainer _container;

        public object Navigation { get; private set; }
        public object Layers { get; private set; }
        public object Selection { get; private set; }
        public object Action { get; private set; }
        public object Surface { get; private set; }

        #endregion

        #region Constructors

        private NavigationManager(IUnityContainer container)
        {
            this._container = container;
        }

        #endregion

        #region Methods

        public static void Initialize(IUnityContainer container)
        {
            Instance = new NavigationManager(container);
        }

        public void RegisterViewTypes(Type viewType, Type viewModelType)
        {
            _viewTypeMapping[viewModelType] = viewType;
        }

        public void Navigate<TTarget>(NavigationTargets target)
        {
            Navigate(target, typeof(TTarget));
        }

        public void Navigate(NavigationTargets target, Type targetType)
        {
            Type viewType;
            if (!_viewTypeMapping.TryGetValue(targetType, out viewType))
                viewType = targetType;

            Navigate(target, _container.Resolve(viewType));
        }

        public void Navigate(NavigationTargets target, object view)
        {
            switch (target)
            {
                case NavigationTargets.Navigation:
                    Navigation = view;
                    break;

                case NavigationTargets.Layers:
                    Layers = view;
                    break;

                case NavigationTargets.Selection:
                    Selection = view;
                    break;

                case NavigationTargets.Action:
                    Action = view;
                    break;

                case NavigationTargets.Surface:
                    Surface = view;
                    break;
            }
        }

        #endregion
    }
}
