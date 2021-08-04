using System;

namespace TypingMachine.Entities
{
    /// <summary>
    /// Represents a `using UsedNamespace;` directive.
    /// </summary>
    public class UsingEntity
    {
        public NamespaceEntity UsedNamespace { get; }

        private UsingEntity(NamespaceEntity usedNamespace)
        {
            UsedNamespace = usedNamespace;
        }

        public static UsingEntity Create(NamespaceEntity usedNamespace)
        {
            if (usedNamespace == null)
                throw new ArgumentNullException(nameof(usedNamespace));
            if (usedNamespace == NamespaceEntity.NoNamespace)
                throw new ArgumentException("Using directive must involve a concrete namespace.", nameof(usedNamespace));

            return new(usedNamespace);
        }
    }
}
