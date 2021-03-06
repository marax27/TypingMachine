using System;
using System.Collections.Generic;
using TypingMachine.Abstractions;

namespace TypingMachine.Domain
{
    public class ClassEntity : TypeEntity
    {
        public IReadOnlyList<FieldEntity> Fields { get; }

        public ClassEntity(Identifier identifier, NamespaceIdentifier namespaceId, IReadOnlyList<MethodEntity> methods, IReadOnlyList<Identifier> baseTypes, IReadOnlyList<FieldEntity> fields, IReadOnlyCollection<UsingEntity> usingDirectives, AccessModifier accessModifier)
            : base(identifier, namespaceId, methods, baseTypes, usingDirectives, accessModifier)
        {
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }

        public override void Accept(ITypeVisitor visitor)
        {
            visitor.VisitClass(this);
        }
    }
}
