using System;
using System.Collections.Generic;
using System.Linq;
using TypingMachine.Abstractions;

namespace TypingMachine.Entities
{
    public abstract class TypeEntity
    {
        public TypeIdentifier Identifier { get; }
        public NamespaceIdentifier NamespaceId { get; }
        public IReadOnlyList<MethodEntity> Methods { get; }
        public IReadOnlyList<TypeIdentifier> BaseTypes { get; }
        public IReadOnlyCollection<UsingEntity> UsingDirectives { get; }

        public abstract void Accept(ITypeVisitor visitor);

        public TypeEntity? FindReferencedType(TypeIdentifier referencedTypeId, IEnumerable<TypeEntity> candidateTypes)
        {
            return candidateTypes
                .Where(type => type.NamespaceId == NamespaceId
                               || UsingDirectives.Any(u => u.UsedNamespace == type.NamespaceId))
                .SingleOrDefault(type => type.Identifier == referencedTypeId);
        }

        protected TypeEntity(TypeIdentifier identifier, NamespaceIdentifier namespaceId, IReadOnlyList<MethodEntity> methods, IReadOnlyList<TypeIdentifier> baseTypes, IReadOnlyCollection<UsingEntity> usingDirectives)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            NamespaceId = namespaceId ?? throw new ArgumentNullException(nameof(namespaceId));
            Methods = methods ?? throw new ArgumentNullException(nameof(methods));
            BaseTypes = baseTypes ?? throw new ArgumentNullException(nameof(baseTypes));
            UsingDirectives = usingDirectives ?? throw new ArgumentNullException(nameof(usingDirectives));
        }
    }
}
