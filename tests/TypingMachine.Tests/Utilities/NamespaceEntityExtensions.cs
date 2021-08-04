using TypingMachine.Entities;

namespace TypingMachine.Tests.Utilities
{
    static class NamespaceEntityExtensions
    {
        public static NamespaceEntity AsNamespace(this string namespaceName)
        {
            return NamespaceEntity.Create(namespaceName.Split('.'));
        }
    }
}
