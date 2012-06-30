namespace Ninject.Web.WebApi.MissingBindingLogger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Ninject.Activation;
    using Ninject.Components;
    using Ninject.Extensions.Logging;
    using Ninject.Infrastructure;
    using Ninject.Planning.Bindings;
    using Ninject.Planning.Bindings.Resolvers;

    /// <summary>
    /// Component for logging missing Ninject bindings
    /// </summary>
    public class MissingBindingLogger : NinjectComponent, IMissingBindingResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBindingLogger" /> class.
        /// </summary>
        public MissingBindingLogger()
        {
            var factory = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILoggerFactory)) as ILoggerFactory;

            if (factory != null) 
            {
                this.Log = factory.GetCurrentClassLogger();
            }
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Log { get; private set; }

        /// <summary>
        /// Returns any bindings from the specified collection that match the specified request.
        /// </summary>
        /// <param name="bindings">The multimap of all registered bindings.</param><param name="request">The request in question.</param>
        /// <returns>
        /// The series of matching bindings.
        /// </returns>
        public IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, IRequest request)
        {
            var service = request.Service;

            if (!this.TypeIsSelfBindable(service) && !this.TypeIsSystemAssembly(service) && this.Log != null)
            {
                this.Log.Error("Missing binding for '{0}'", request.Service);
            }

            return Enumerable.Empty<IBinding>();
        }

        /// <summary>
        /// Returns a value indicating whether the specified service is a system assembly.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns><see langword="True"/> if the type is a system assembly; otherwise <see langword="false"/>.</returns>
        protected virtual bool TypeIsSystemAssembly(Type service)
        {
            if (service.Namespace != null)
            {
                return service.Namespace.StartsWith("System", StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        /// <summary>
        /// Returns a value indicating whether the specified service is self-bindable.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns><see langword="True"/> if the type is self-bindable; otherwise <see langword="false"/>.</returns>
        protected virtual bool TypeIsSelfBindable(Type service)
        {
            return !service.IsInterface
                   && !service.IsAbstract
                   && !service.IsValueType
                   && service != typeof(string)
                   && !service.ContainsGenericParameters;
        }
    }
}
