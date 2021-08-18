using System.Collections.Generic;
using TypingMachine.Entities;

namespace TypingMachine.Builders
{
    public class MethodBuilder
    {
        private IReadOnlyList<TypeIdentifier> _argumentTypes = new List<TypeIdentifier>();

        public MethodBuilder WithArgumentTypes(IReadOnlyList<TypeIdentifier> argumentTypes)
        {
            _argumentTypes = argumentTypes;
            return this;
        }

        public MethodEntity Build(string name, TypeIdentifier returnType)
        {
            return MethodEntity.Create(name, returnType, _argumentTypes);
        }
    }
}
