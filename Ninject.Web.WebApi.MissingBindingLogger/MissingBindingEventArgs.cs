namespace Ninject.Extensions.MissingBindingLogger
{
    using System;

    /// <summary>
    /// Event arguments for the missing binding event
    /// </summary>
    public class MissingBindingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBindingEventArgs" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="throwExceptionOnMissingBinding">if set to <c>true</c> [throw exception on missing binding].</param>
        public MissingBindingEventArgs(Type service, bool throwExceptionOnMissingBinding = false)
        {
            this.Service = service;
            this.ThrowExceptionOnMissingBinding = throwExceptionOnMissingBinding;
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public Type Service { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [throw exception on missing binding].
        /// </summary>
        /// <value>
        /// <c>true</c> if [throw exception on missing binding]; otherwise, <c>false</c>.
        /// </value>
        public bool ThrowExceptionOnMissingBinding { get; set; }
    }
}