namespace Ninject.Extensions.MissingBindingLogger.Tests
{
    using System;

    using NUnit.Framework;

    using Ninject.Planning.Bindings.Resolvers;

    [TestFixture]
    public class MissingBindingLoggerTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var kernel = new StandardKernel();

            kernel.Components.Add<IMissingBindingResolver, MissingBindingLogger>();
        }

        [Test]
        public void Log_WhenReferencingMissingBinding_ShouldTriggerEvent()
        {
            var wasCalled = false;

            var kernel = new StandardKernel();

            MissingBindingLogger.BindingMissing += (sender, e) => wasCalled = true;

            try 
            {
                var test = kernel.Get<ITest>();
            }
            catch
            {
            }

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void Log_WhenReferencingMissingBinding_ShouldNotTriggerEvent()
        {
            var wasCalled = false;

            var kernel = new StandardKernel();

            kernel.Bind<ITest>().To<Test>();

            MissingBindingLogger.BindingMissing += (sender, e) => { wasCalled = true; };

            try
            {
                var test = kernel.Get<ITest>();
            }
            catch
            {
            }

            Assert.IsFalse(wasCalled);
        }
    }
}
