using System.Collections.Generic;

namespace TypingMachine.Domain.Builders
{
    public class InterfaceBuilder
    {
        private IReadOnlyList<MethodEntity> _methods = new List<MethodEntity>();
        private IReadOnlyList<Identifier> _baseTypes = new List<Identifier>();
        private NamespaceIdentifier _namespaceId = NamespaceIdentifier.NoNamespace;
        private IReadOnlyCollection<UsingEntity> _usingDirectives = new List<UsingEntity>();
        private AccessModifier _accessModifier = AccessModifier.Internal;

        public InterfaceBuilder WithMethods(IReadOnlyList<MethodEntity> methods)
        {
            _methods = methods;
            return this;
        }

        public InterfaceBuilder WithBaseTypes(IReadOnlyList<Identifier> baseTypes)
        {
            _baseTypes = baseTypes;
            return this;
        }

        public InterfaceBuilder WithNamespace(NamespaceIdentifier namespaceId)
        {
            _namespaceId = namespaceId;
            return this;
        }

        public InterfaceBuilder WithUsingDirectives(IReadOnlyCollection<UsingEntity> usingDirectives)
        {
            _usingDirectives = usingDirectives;
            return this;
        }

        public InterfaceBuilder WithAccess(AccessModifier accessModifier)
        {
            _accessModifier = accessModifier;
            return this;
        }

        public InterfaceEntity Build(Identifier typeIdentifier)
            => new(typeIdentifier, _namespaceId, _methods, _baseTypes, _usingDirectives, _accessModifier);
    }
}
