using System.Collections.Generic;
using TypingMachine.Abstractions;

namespace TypingMachine.Domain
{
    public class InterfaceEntity : TypeEntity
    {
        public InterfaceEntity(Identifier identifier, NamespaceIdentifier namespaceId, IReadOnlyList<MethodEntity> methods, IReadOnlyList<Identifier> baseTypes, IReadOnlyCollection<UsingEntity> usingDirectives, AccessModifier accessModifier)
            : base(identifier, namespaceId, methods, baseTypes, usingDirectives, accessModifier)
        {
        }

        public override void Accept(ITypeVisitor visitor)
        {
            visitor.VisitInterface(this);
        }
    }
}
