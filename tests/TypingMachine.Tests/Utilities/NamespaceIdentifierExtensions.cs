using TypingMachine.Entities;

namespace TypingMachine.Tests.Utilities
{
    static class NamespaceIdentifierExtensions
    {
        public static NamespaceIdentifier AsNamespace(this string namespaceName)
        {
            return NamespaceIdentifier.Create(namespaceName.Split('.'));
        }
    }
}
