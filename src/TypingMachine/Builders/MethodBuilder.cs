using System.Collections.Generic;
using TypingMachine.Entities;

namespace TypingMachine.Builders
{
    public class MethodBuilder
    {
        private IReadOnlyList<TypeIdentifier> _argumentTypes = new List<TypeIdentifier>();
        private AccessModifier _accessModifier = AccessModifier.Private;

        public MethodBuilder WithArgumentTypes(IReadOnlyList<TypeIdentifier> argumentTypes)
        {
            _argumentTypes = argumentTypes;
            return this;
        }

        public MethodBuilder WithAccess(AccessModifier accessModifier)
        {
            _accessModifier = accessModifier;
            return this;
        }

        public MethodEntity Build(string name, TypeIdentifier returnType)
        {
            return MethodEntity.Create(name, returnType, _argumentTypes, _accessModifier);
        }
    }
}
