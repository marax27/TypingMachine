using System;
using System.Collections.Generic;

namespace TypingMachine.Domain
{
    public class MethodEntity : BaseTypeMember
    {
        public Identifier Identifier { get; }
        public Identifier ReturnType { get; }
        public IReadOnlyList<Identifier> ArgumentTypes { get; }

        private MethodEntity(Identifier identifier, Identifier returnType, IReadOnlyList<Identifier> argumentTypes, AccessModifier accessModifier)
            : base(accessModifier)
        {
            Identifier = identifier;
            ReturnType = returnType;
            ArgumentTypes = argumentTypes;
        }

        public static MethodEntity Create(Identifier identifier, Identifier returnType,
            IReadOnlyList<Identifier> argumentTypes, AccessModifier accessModifier)
        {
            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier));
            if (returnType == null)
                throw new ArgumentNullException(nameof(returnType));
            if (argumentTypes == null)
                throw new ArgumentNullException(nameof(argumentTypes));

            return new (identifier, returnType, argumentTypes, accessModifier);
        }
    }
}
