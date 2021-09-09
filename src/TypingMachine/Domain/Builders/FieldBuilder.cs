namespace TypingMachine.Domain.Builders
{
    public class FieldBuilder
    {
        private AccessModifier _accessModifier = AccessModifier.Private;

        public FieldBuilder WithAccess(AccessModifier accessModifier)
        {
            _accessModifier = accessModifier;
            return this;
        }

        public FieldEntity Build(string name, Identifier type)
        {
            return FieldEntity.Create(name, type, _accessModifier);
        }
    }
}
