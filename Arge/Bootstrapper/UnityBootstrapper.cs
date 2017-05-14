using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Microsoft.Practices.Unity;

namespace Arge.Bootstrapper
{
    public abstract class UnityBootstrapper : BootstrapperBase
    {
        #region Properties & Fields

        protected IUnityContainer Container;

        #endregion

        #region Constructors

        protected UnityBootstrapper()
        {
            Initialize();
        }

        #endregion

        #region Methods

        protected override void Configure()
        {
            Container = new UnityContainer();
            RegisterSingleton(Container);
            RegisterInterface<IEventAggregator, EventAggregator>();

            RegisterTypes();
        }

        protected abstract void RegisterTypes();

        protected override object GetInstance(Type service, string key)
        {
            return key != null ? Container.Resolve(service, key) : Container.Resolve(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.ResolveAll(service);
        }

        protected override void BuildUp(object instance)
        {
            instance = Container.BuildUp(instance);
            base.BuildUp(instance);
        }

        #region Container-Registration

        protected void RegisterInterface<TInterface, TImplementation>(bool registerTypeToo = true, bool registerAsSingleton = true, string uniqueName = null)
            where TImplementation : TInterface
        {
            RegisterTypeIfMissing(typeof(TInterface), typeof(TImplementation), registerAsSingleton, uniqueName);
            if (registerTypeToo)
                RegisterTypeIfMissing(typeof(TImplementation), typeof(TImplementation), registerAsSingleton, uniqueName);
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
