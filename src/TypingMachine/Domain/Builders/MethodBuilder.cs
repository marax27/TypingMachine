using System.Collections.Generic;

namespace TypingMachine.Domain.Builders
{
    public class MethodBuilder
    {
        private IReadOnlyList<Identifier> _argumentTypes = new List<Identifier>();
        private AccessModifier _accessModifier = AccessModifier.Private;

        public MethodBuilder WithArgumentTypes(IReadOnlyList<Identifier> argumentTypes)
        {
            _argumentTypes = argumentTypes;
            return this;
        }

        public MethodBuilder WithAccess(AccessModifier accessModifier)
        {
            _accessModifier = accessModifier;
            return this;
        }

        public MethodEntity Build(Identifier methodIdentifier, Identifier returnType)
        {
            return MethodEntity.Create(methodIdentifier, returnType, _argumentTypes, _accessModifier);
        }
    }
}
