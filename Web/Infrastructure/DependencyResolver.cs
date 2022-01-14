using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Web.Controllers;
using Web.Infrastructure;

namespace Web
{
    public class DependencyResolver : IDependencyResolver
    {
        private static Lazy<IServiceProvider> _lazyServiceProvider = new Lazy<IServiceProvider>(() => InitializeServiceProvider());

        private IServiceScope _serviceScope;
        private bool _disposed = false;

        private static IServiceProvider InitializeServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped(t => new OrderService());
            serviceCollection.AddScoped<OrderController>();

            return serviceCollection.BuildServiceProvider();
        }

        public DependencyResolver()
        {
        }

        private DependencyResolver(IServiceScope serviceScope)
        {
            _serviceScope = serviceScope ??
                throw new ArgumentNullException(nameof(serviceScope));
        }

        private void EnsureNotDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DependencyResolver));
        }

        public IDependencyScope BeginScope()
        {
            EnsureNotDisposed();

            return new DependencyResolver(_lazyServiceProvider.Value.CreateScope());
        }

        public object GetService(Type serviceType)
        {
            EnsureNotDisposed();

            return _serviceScope?.ServiceProvider?.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            EnsureNotDisposed();

            return _serviceScope?.ServiceProvider?.GetServices(serviceType) ?? new object[0];
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _serviceScope?.Dispose();

            _disposed = true;
        }
    }
}