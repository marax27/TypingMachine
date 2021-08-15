using System.Collections.Generic;
using TypingMachine.Abstractions;

namespace TypingMachine.Entities
{
    public class InterfaceEntity : TypeEntity
    {
        public InterfaceEntity(TypeIdentifier identifier, NamespaceIdentifier namespaceId, IReadOnlyList<MethodEntity> methods, IReadOnlyList<TypeIdentifier> baseTypes)
            : base(identifier, namespaceId, methods, baseTypes)
        {
        }

        public override void Accept(ITypeVisitor visitor)
        {
            visitor.VisitInterface(this);
        }
    }
}
