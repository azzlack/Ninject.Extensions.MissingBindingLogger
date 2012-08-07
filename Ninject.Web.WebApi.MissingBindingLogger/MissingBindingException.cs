namespace Ninject.Extensions.MissingBindingLogger
{
    using System;

    /// <summary>
    /// Exception model for missing Ninject bindings.
    /// </summary>
    [Serializable]
    public class MissingBindingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBindingException" /> class.
        /// </summary>
        public MissingBindingException()
            : base("One or more dependencies does not have concrete implementations")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBindingException" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public MissingBindingException(Type service)
            : base(string.Format("{0} does not have a concrete implementation", service.FullName))
        {
            this.Service = service;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBindingException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="service">The service.</param>
        public MissingBindingException(string message, Type service) 
            : base(message)
        {
            this.Service = service;
        }

        /// <summary>
        /// Gets the service that is not bound.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public Type Service { get; private set; }
    }
}