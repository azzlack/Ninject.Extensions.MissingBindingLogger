namespace Ninject.Web.WebApi.MissingBindingLogger
{
    using Ninject.Modules;
    using Ninject.Planning.Bindings.Resolvers;

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