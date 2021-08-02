using System;
using System.Collections.Generic;
using TypingMachine.Abstractions;

namespace TypingMachine.Entities
{
    public class ClassEntity : TypeEntity
    {
        public IReadOnlyList<FieldEntity> Fields { get; }

        public ClassEntity(TypeIdentifier identifier, IReadOnlyList<MethodEntity> methods, IReadOnlyList<TypeIdentifier> baseTypes, IReadOnlyList<FieldEntity> fields) : base(identifier, methods, baseTypes)
        {
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }

        public override void Accept(ITypeVisitor visitor)
        {
            visitor.VisitClass(this);
        }
    }
}
