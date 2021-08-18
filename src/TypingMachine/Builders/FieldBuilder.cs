using TypingMachine.Entities;

namespace TypingMachine.Builders
{
    public class FieldBuilder
    {
        private AccessModifier _accessModifier = AccessModifier.Private;

        public FieldBuilder WithAccess(AccessModifier accessModifier)
        {
            _accessModifier = accessModifier;
            return this;
        }

        public FieldEntity Build(string name, TypeIdentifier type)
        {
            return FieldEntity.Create(name, type, _accessModifier);
        }
    }
}
