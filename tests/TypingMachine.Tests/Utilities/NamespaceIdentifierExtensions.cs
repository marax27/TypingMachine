using TypingMachine.Domain;

namespace TypingMachine.Tests.Utilities
{
    internal static class NamespaceIdentifierExtensions
    {
        public static NamespaceIdentifier AsNamespace(this string namespaceName)
        {
            return NamespaceIdentifier.Create(namespaceName.Split('.'));
        }
    }
}
