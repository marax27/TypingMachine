using System;
using System.Collections.Generic;

namespace TypingMachine.Entities
{
    public class MethodEntity
    {
        public string Name { get; }
        public TypeIdentifier ReturnType { get; }
        public IReadOnlyList<TypeIdentifier> ArgumentTypes { get; }

        private MethodEntity(string name, TypeIdentifier returnType, IReadOnlyList<TypeIdentifier> argumentTypes)
        {
            Name = name;
            ReturnType = returnType;
            ArgumentTypes = argumentTypes;
        }

        public static MethodEntity Create(string name, TypeIdentifier returnType,
            IReadOnlyList<TypeIdentifier> argumentTypes)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException(nameof(name), "Method name is empty or whitespace-only.");
            if (returnType == null)
                throw new ArgumentNullException(nameof(returnType));
            if (argumentTypes == null)
                throw new ArgumentNullException(nameof(argumentTypes));

            return new (name, returnType, argumentTypes);
        }
    }
}
