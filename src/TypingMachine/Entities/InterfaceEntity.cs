using System.Collections.Generic;
using TypingMachine.Abstractions;

namespace TypingMachine.Entities
{
    public class InterfaceEntity : TypeEntity
    {
        public InterfaceEntity(TypeIdentifier identifier, IReadOnlyList<MethodEntity> methods, IReadOnlyList<TypeIdentifier> baseTypes) : base(identifier, methods, baseTypes)
        {
        }

        public override void Accept(ITypeVisitor visitor)
        {
            visitor.VisitInterface(this);
        }
    }
}
