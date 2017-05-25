using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Splat;

namespace Arge.Bootstrapper
{
    //HACK DarthAffe 21.05.2017: I've no idea if this a good implementation - don't think so but it seems to work ...
    public class UnityDependencyResolver : IMutableDependencyResolver
    {
        #region Properties & Fields

        private readonly IUnityContainer _container;
        private int _defaultCounter = 0;

        #endregion

        #region Constructors

        public UnityDependencyResolver(IUnityContainer container)
        {
            this._container = container;
        }

        #endregion

        #region Methods

        public void Dispose() { }

        public object GetService(Type serviceType, string contract = null)
        {
            return _container.Resolve(serviceType, contract ?? "default");
        }

        public IEnumerable<object> GetServices(Type serviceType, string contract = null)
        {
            return _container.ResolveAll(serviceType);
        }

        public void Register(Func<object> factory, Type serviceType, string contract = null)
        {
            if (_container.IsRegistered(serviceType, contract ?? "default"))
                contract = $"{(contract ?? "default")}_{_defaultCounter++}";
            _container.RegisterType(serviceType, serviceType, contract ?? "default", new InjectionFactory(c => factory()));
        }

        public IDisposable ServiceRegistrationCallback(Type serviceType, string contract, Action<IDisposable> callback)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
