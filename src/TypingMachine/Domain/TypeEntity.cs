using System;
using System.Collections.Generic;
using System.Linq;
using TypingMachine.Abstractions;
using TypingMachine.Utilities;

namespace TypingMachine.Domain
{
    public abstract class TypeEntity
    {
        public TypeIdentifier Identifier { get; }
        public NamespaceIdentifier NamespaceId { get; }
        public IReadOnlyList<MethodEntity> Methods { get; }
        public IReadOnlyList<TypeIdentifier> BaseTypes { get; }
        public IReadOnlyCollection<UsingEntity> UsingDirectives { get; }
        public AccessModifier AccessModifier { get; }

        public abstract void Accept(ITypeVisitor visitor);

        public TypeEntity? FindReferencedType(ITypeIdentifier referencedTypeId, IReadOnlyCollection<TypeEntity> candidates)
        {
            var accessibleNamespaces = UsingDirectives
                .Select(u => u.UsedNamespace)
                .ConcatSingle(NamespaceId);

            return candidates
                .Where(type => accessibleNamespaces.Contains(type.NamespaceId))
                .SingleOrDefault(type => type.Identifier.Name == referencedTypeId.Name 
                                         && type.Identifier.Arity == referencedTypeId.Arity);
        }

        protected TypeEntity(TypeIdentifier identifier, NamespaceIdentifier namespaceId, IReadOnlyList<MethodEntity> methods, IReadOnlyList<TypeIdentifier> baseTypes, IReadOnlyCollection<UsingEntity> usingDirectives, AccessModifier accessModifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            NamespaceId = namespaceId ?? throw new ArgumentNullException(nameof(namespaceId));
            Methods = methods ?? throw new ArgumentNullException(nameof(methods));
            BaseTypes = baseTypes ?? throw new ArgumentNullException(nameof(baseTypes));
            UsingDirectives = usingDirectives ?? throw new ArgumentNullException(nameof(usingDirectives));
            AccessModifier = accessModifier;
        }
    }
}
