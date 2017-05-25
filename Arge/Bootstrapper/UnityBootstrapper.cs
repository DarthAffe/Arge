using System;
using System.Windows;
using Microsoft.Practices.Unity;
using ReactiveUI;
using Splat;

namespace Arge.Bootstrapper
{
    public abstract class UnityBootstrapper
    {
        #region Properties & Fields

        protected IUnityContainer Container;

        #endregion

        #region Methods

        public virtual void OnStartup(StartupEventArgs e)
        {
            Configure();
            RegisterTypes();
            InitializeHandlers();
            Start();
        }

        public virtual void OnExit(ExitEventArgs e)
        {
        }

        protected virtual void Configure()
        {
            Container = new UnityContainer();
            RegisterSingleton(Container);

            Locator.Current = new UnityDependencyResolver(Container);
            NavigationManager.Initialize(Container);
        }

        protected abstract void RegisterTypes();

        protected abstract void InitializeHandlers();

        protected abstract void Start();

        #region Container-Registration

        protected void RegisterService<TInterface, TImplementation>(bool registerTypeToo = true, bool registerAsSingleton = true, string uniqueName = null)
            where TImplementation : TInterface
        {
            RegisterTypeIfMissing(typeof(TInterface), typeof(TImplementation), registerAsSingleton, uniqueName);
            if (registerTypeToo)
                RegisterTypeIfMissing(typeof(TImplementation), typeof(TImplementation), registerAsSingleton, uniqueName);
        }

        protected void RegisterView<TView, TViewModel>()
            where TView : IViewFor<TViewModel>
            where TViewModel : class, IReactiveObject
        {
            RegisterTypeIfMissing(typeof(TView), typeof(TView), false);
            RegisterTypeIfMissing(typeof(TViewModel), typeof(TViewModel), false);

            NavigationManager.Instance.RegisterViewTypes(typeof(TView), typeof(TViewModel));
        }

        protected void RegisterSingleton<T>()
        {
            RegisterTypeIfMissing(typeof(T), typeof(T), true);
        }

        protected void RegisterSingleton<T>(T instance)
        {
            Container.RegisterInstance(typeof(T), instance, new ContainerControlledLifetimeManager());
        }

        protected void RegisterTypeIfMissing(Type fromType, Type toType, bool registerAsSingleton, string uniqueName = null)
        {
            if (fromType == null)
                throw new ArgumentNullException(nameof(fromType));

            if (toType == null)
                throw new ArgumentNullException(nameof(toType));

            if (!Container.IsRegistered(fromType))
            {
                if (registerAsSingleton)
                    Container.RegisterType(fromType, toType, uniqueName, new ContainerControlledLifetimeManager());
                else
                    Container.RegisterType(fromType, toType, uniqueName);
            }
        }

        #endregion

        #endregion
    }
}
