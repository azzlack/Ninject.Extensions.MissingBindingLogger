namespace Ninject.Web.WebApi.MissingBindingLogger
{
    using Ninject.Modules;
    using Ninject.Planning.Bindings.Resolvers;

    /// <summary>
    /// Module for loading the missing binding logger
    /// </summary>
    public class MissingBindingLoggerModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            // Register missing binding logger component
            Kernel.Components.Add<IMissingBindingResolver, MissingBindingLogger>();
        }
    }
}