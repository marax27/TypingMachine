using System.Collections.Generic;

namespace TypingMachine.Entities
{
    public class InterfaceEntity : TypeEntity
    {
        public InterfaceEntity(TypeIdentifier identifier, IReadOnlyList<MethodEntity> methods, IReadOnlyList<TypeIdentifier> baseTypes) : base(identifier, methods, baseTypes)
        {
        }
    }
}
