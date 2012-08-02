namespace Ninject.Web.WebApi.MissingBindingLogger
{
    using Ninject.Activation;
    using Ninject.Activation.Strategies;
    using Ninject.Extensions.Logging;

    /// <summary>
    /// Component for logging Ninject bindings
    /// </summary>
    public class ActivationLogger : ActivationStrategy
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Log { get; private set; }

        /// <summary>
        /// Contributes to the activation of the instance in the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="reference">A reference to the instance being activated.</param>
        public override void Activate(IContext context, InstanceReference reference)
        {
            if (reference.Instance is ILogger && this.Log == null)
            {
                this.Log = (ILogger)reference.Instance;
            }

            base.Activate(context, reference);

            if (this.Log != null)
            {
                this.Log.Info("Ninject binding activated: " + context.Binding.Service + " => " + reference.Instance.GetType());
            }
        }

        /// <summary>
        /// Contributes to the deactivation of the instance in the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="reference">A reference to the instance being deactivated.</param>
        public override void Deactivate(IContext context, InstanceReference reference)
        {
            base.Deactivate(context, reference);

            if (this.Log != null)
            {
                this.Log.Info("Ninject binding deactivated: " + context.Binding.Service + " => " + reference.Instance.GetType());
            }
        }
    }
}
