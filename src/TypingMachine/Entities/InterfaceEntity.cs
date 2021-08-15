using System.Collections.Generic;
using TypingMachine.Abstractions;

namespace TypingMachine.Entities
{
    public class InterfaceEntity : TypeEntity
    {
        public InterfaceEntity(TypeIdentifier identifier, NamespaceIdentifier namespaceId, IReadOnlyList<MethodEntity> methods, IReadOnlyList<TypeIdentifier> baseTypes, IReadOnlyCollection<UsingEntity> usingDirectives)
            : base(identifier, namespaceId, methods, baseTypes, usingDirectives)
        {
        }

        public override void Accept(ITypeVisitor visitor)
        {
            visitor.VisitInterface(this);
        }
    }
}
