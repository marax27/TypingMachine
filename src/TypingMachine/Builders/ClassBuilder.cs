using System.Collections.Generic;
using TypingMachine.Entities;

namespace TypingMachine.Builders
{
    public class ClassBuilder
    {
        private IReadOnlyList<MethodEntity> _methods = new List<MethodEntity>();
        private IReadOnlyList<TypeIdentifier> _baseTypes = new List<TypeIdentifier>();
        private IReadOnlyList<FieldEntity> _fields = new List<FieldEntity>();
        private NamespaceIdentifier _namespaceId = NamespaceIdentifier.NoNamespace;

        public ClassBuilder WithMethods(IReadOnlyList<MethodEntity> methods)
        {
            _methods = methods;
            return this;
        }

        public ClassBuilder WithBaseTypes(IReadOnlyList<TypeIdentifier> baseTypes)
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

        public ClassEntity Build(TypeIdentifier typeIdentifier)
            => new (typeIdentifier, _namespaceId, _methods, _baseTypes, _fields);
    }
}
