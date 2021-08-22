using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace TypingMachine.Tests.Utilities
{
    /// <summary>
    /// Provides a data source for a xUnit Theory that consists of all implementations
    /// of a given interface (that define a parameterless constructor).
    /// </summary>
    internal class ContextDataAttribute : DataAttribute
    {
        private readonly Type _contextInterface;

        /// <param name="contextInterface">Interface whose implementations will serve as test arguments.
        /// Its implementations should have a parameterless constructor defined
        /// - otherwise they will be ignored.</param>
        public ContextDataAttribute(Type contextInterface)
        {
            _contextInterface = contextInterface;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var instances =
                from type in Assembly.GetExecutingAssembly().GetTypes()
                where type.GetInterfaces().Contains(_contextInterface)
                      && type.GetConstructor(Type.EmptyTypes) != null
                select Activator.CreateInstance(type);

            return instances.Select(instance => new[] {instance});
        }
    }
}
