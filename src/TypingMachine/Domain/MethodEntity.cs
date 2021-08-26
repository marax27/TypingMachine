using System;
using System.Collections.Generic;

namespace TypingMachine.Domain
{
    public class MethodEntity : BaseTypeMember
    {
        public string Name { get; }
        public Identifier ReturnType { get; }
        public IReadOnlyList<Identifier> ArgumentTypes { get; }

        private MethodEntity(string name, Identifier returnType, IReadOnlyList<Identifier> argumentTypes, AccessModifier accessModifier)
            : base(accessModifier)
        {
            Name = name;
            ReturnType = returnType;
            ArgumentTypes = argumentTypes;
        }

        public static MethodEntity Create(string name, Identifier returnType,
            IReadOnlyList<Identifier> argumentTypes, AccessModifier accessModifier)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException(nameof(name), "Method name is empty or whitespace-only.");
            if (returnType == null)
                throw new ArgumentNullException(nameof(returnType));
            if (argumentTypes == null)
                throw new ArgumentNullException(nameof(argumentTypes));

            return new (name, returnType, argumentTypes, accessModifier);
        }
    }
}
