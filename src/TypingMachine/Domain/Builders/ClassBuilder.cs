using System.Collections.Generic;

namespace TypingMachine.Domain.Builders
{
    public class ClassBuilder
    {
        private IReadOnlyList<MethodEntity> _methods = new List<MethodEntity>();
        private IReadOnlyList<Identifier> _baseTypes = new List<Identifier>();
        private IReadOnlyList<FieldEntity> _fields = new List<FieldEntity>();
        private NamespaceIdentifier _namespaceId = NamespaceIdentifier.NoNamespace;
        private IReadOnlyCollection<UsingEntity> _usingDirectives = new List<UsingEntity>();
        private AccessModifier _accessModifier = AccessModifier.Internal;

        public ClassBuilder WithMethods(IReadOnlyList<MethodEntity> methods)
        {
            _methods = methods;
            return this;
        }

        public ClassBuilder WithBaseTypes(IReadOnlyList<Identifier> baseTypes)
        {
            _baseTypes = baseTypes;
            return this;
        }

        public ClassBuilder WithFields(IReadOnlyList<FieldEntity> fields)
        {
            _fields = fields;
            return this;
        }

        public ClassBuilder WithNamespace(NamespaceIdentifier namespaceId)
        {
            _namespaceId = namespaceId;
            return this;
        }

        public ClassBuilder WithUsingDirectives(IReadOnlyCollection<UsingEntity> usingDirectives)
        {
            _usingDirectives = usingDirectives;
            return this;
        }

        public ClassBuilder WithAccess(AccessModifier accessModifier)
        {
            _accessModifier = accessModifier;
            return this;
        }

        public ClassEntity Build(Identifier typeIdentifier)
            => new (typeIdentifier, _namespaceId, _methods, _baseTypes, _fields, _usingDirectives, _accessModifier);
    }
}
