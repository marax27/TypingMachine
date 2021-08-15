using System;

namespace TypingMachine.Entities
{
    /// <summary>
    /// Represents a `using UsedNamespace;` directive.
    /// </summary>
    public class UsingEntity
    {
        public NamespaceIdentifier UsedNamespace { get; }

        private UsingEntity(NamespaceIdentifier usedNamespace)
        {
            UsedNamespace = usedNamespace;
        }

        public static UsingEntity Create(NamespaceIdentifier usedNamespace)
        {
            if (usedNamespace == null)
                throw new ArgumentNullException(nameof(usedNamespace));
            if (usedNamespace == NamespaceIdentifier.NoNamespace)
                throw new ArgumentException("Using directive must involve a concrete namespace.", nameof(usedNamespace));

            return new(usedNamespace);
        }
    }
}
